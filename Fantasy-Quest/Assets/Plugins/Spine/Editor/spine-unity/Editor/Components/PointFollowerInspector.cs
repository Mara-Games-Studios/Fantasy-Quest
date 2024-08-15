/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated January 1, 2020. Replaces all prior versions.
 *
 * Copyright (c) 2013-2020, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Spine.Unity.Editor
{
    [CustomEditor(typeof(PointFollower)), CanEditMultipleObjects]
    public class PointFollowerInspector : UnityEditor.Editor
    {
        private SerializedProperty slotName,
            pointAttachmentName,
            skeletonRenderer,
            followZPosition,
            followBoneRotation,
            followSkeletonFlip;
        private PointFollower targetPointFollower;
        private bool needsReset;

        #region Context Menu Item
        [MenuItem("CONTEXT/SkeletonRenderer/Add PointFollower GameObject")]
        private static void AddBoneFollowerGameObject(MenuCommand cmd)
        {
            SkeletonRenderer skeletonRenderer = cmd.context as SkeletonRenderer;
            GameObject go = EditorInstantiation.NewGameObject("PointFollower", true);
            Transform t = go.transform;
            t.SetParent(skeletonRenderer.transform);
            t.localPosition = Vector3.zero;

            PointFollower f = go.AddComponent<PointFollower>();
            f.skeletonRenderer = skeletonRenderer;

            EditorGUIUtility.PingObject(t);

            Undo.RegisterCreatedObjectUndo(go, "Add PointFollower");
        }

        // Validate
        [MenuItem("CONTEXT/SkeletonRenderer/Add PointFollower GameObject", true)]
        private static bool ValidateAddBoneFollowerGameObject(MenuCommand cmd)
        {
            SkeletonRenderer skeletonRenderer = cmd.context as SkeletonRenderer;
            return skeletonRenderer.valid;
        }
        #endregion

        private void OnEnable()
        {
            skeletonRenderer = serializedObject.FindProperty("skeletonRenderer");
            slotName = serializedObject.FindProperty("slotName");
            pointAttachmentName = serializedObject.FindProperty("pointAttachmentName");

            targetPointFollower = (PointFollower)target;
            if (targetPointFollower.skeletonRenderer != null)
            {
                targetPointFollower.skeletonRenderer.Initialize(false);
            }

            if (!targetPointFollower.IsValid || needsReset)
            {
                targetPointFollower.Initialize();
                targetPointFollower.LateUpdate();
                needsReset = false;
                SceneView.RepaintAll();
            }
        }

        public void OnSceneGUI()
        {
            PointFollower tbf = target as PointFollower;
            SkeletonRenderer skeletonRendererComponent = tbf.skeletonRenderer;
            if (skeletonRendererComponent == null)
            {
                return;
            }

            Skeleton skeleton = skeletonRendererComponent.skeleton;
            Transform skeletonTransform = skeletonRendererComponent.transform;

            if (string.IsNullOrEmpty(pointAttachmentName.stringValue))
            {
                // Draw all active PointAttachments in the current skin
                Skin currentSkin = skeleton.Skin;
                if (currentSkin != skeleton.Data.DefaultSkin)
                {
                    DrawPointsInSkin(skeleton.Data.DefaultSkin, skeleton, skeletonTransform);
                }

                if (currentSkin != null)
                {
                    DrawPointsInSkin(currentSkin, skeleton, skeletonTransform);
                }
            }
            else
            {
                int slotIndex = skeleton.FindSlotIndex(slotName.stringValue);
                if (slotIndex >= 0)
                {
                    Slot slot = skeleton.Slots.Items[slotIndex];
                    PointAttachment point =
                        skeleton.GetAttachment(slotIndex, pointAttachmentName.stringValue)
                        as PointAttachment;
                    if (point != null)
                    {
                        DrawPointAttachmentWithLabel(point, slot.Bone, skeletonTransform);
                    }
                }
            }
        }

        private static void DrawPointsInSkin(Skin skin, Skeleton skeleton, Transform transform)
        {
            foreach (
                System.Collections.Generic.KeyValuePair<
                    Skin.SkinEntry,
                    Attachment
                > skinEntry in skin.Attachments
            )
            {
                PointAttachment attachment = skinEntry.Value as PointAttachment;
                if (attachment != null)
                {
                    Skin.SkinEntry skinKey = skinEntry.Key;
                    Slot slot = skeleton.Slots.Items[skinKey.SlotIndex];
                    DrawPointAttachmentWithLabel(attachment, slot.Bone, transform);
                }
            }
        }

        private static void DrawPointAttachmentWithLabel(
            PointAttachment point,
            Bone bone,
            Transform transform
        )
        {
            Vector3 labelOffset = new(0f, -0.2f, 0f);
            SpineHandles.DrawPointAttachment(bone, point, transform);
            Handles.Label(
                labelOffset + point.GetWorldPosition(bone, transform),
                point.Name,
                SpineHandles.PointNameStyle
            );
        }

        public override void OnInspectorGUI()
        {
            if (serializedObject.isEditingMultipleObjects)
            {
                if (needsReset)
                {
                    needsReset = false;
                    foreach (Object o in targets)
                    {
                        BoneFollower bf = (BoneFollower)o;
                        bf.Initialize();
                        bf.LateUpdate();
                    }
                    SceneView.RepaintAll();
                }

                EditorGUI.BeginChangeCheck();
                _ = DrawDefaultInspector();
                needsReset |= EditorGUI.EndChangeCheck();
                return;
            }

            if (needsReset && UnityEngine.Event.current.type == EventType.Layout)
            {
                targetPointFollower.Initialize();
                targetPointFollower.LateUpdate();
                needsReset = false;
                SceneView.RepaintAll();
            }
            serializedObject.Update();

            _ = DrawDefaultInspector();

            // Find Renderer
            if (skeletonRenderer.objectReferenceValue == null)
            {
                SkeletonRenderer parentRenderer =
                    targetPointFollower.GetComponentInParent<SkeletonRenderer>();
                if (
                    parentRenderer != null
                    && parentRenderer.gameObject != targetPointFollower.gameObject
                )
                {
                    skeletonRenderer.objectReferenceValue = parentRenderer;
                    Debug.Log("Inspector automatically assigned PointFollower.SkeletonRenderer");
                }
            }

            SkeletonRenderer skeletonRendererReference =
                skeletonRenderer.objectReferenceValue as SkeletonRenderer;
            if (skeletonRendererReference != null)
            {
                if (skeletonRendererReference.gameObject == targetPointFollower.gameObject)
                {
                    skeletonRenderer.objectReferenceValue = null;
                    _ = EditorUtility.DisplayDialog(
                        "Invalid assignment.",
                        "PointFollower can only follow a skeleton on a separate GameObject.\n\nCreate a new GameObject for your PointFollower, or choose a SkeletonRenderer from a different GameObject.",
                        "Ok"
                    );
                }
            }

            if (!targetPointFollower.IsValid)
            {
                needsReset = true;
            }

            UnityEngine.Event current = UnityEngine.Event.current;
            bool wasUndo =
                current.type == EventType.ValidateCommand
                && current.commandName == "UndoRedoPerformed";
            if (wasUndo)
            {
                targetPointFollower.Initialize();
            }

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}

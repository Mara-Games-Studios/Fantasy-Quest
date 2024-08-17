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

using System.Collections.Generic;

namespace Spine.Unity.AttachmentTools
{
    public static class SkinUtilities
    {
        #region Skeleton Skin Extensions
        /// <summary>
        /// Convenience method for duplicating a skeleton's current active skin so changes to it will not affect other skeleton instances. .</summary>
        public static Skin UnshareSkin(
            this Skeleton skeleton,
            bool includeDefaultSkin,
            bool unshareAttachments,
            AnimationState state = null
        )
        {
            // 1. Copy the current skin and set the skeleton's skin to the new one.
            Skin newSkin = skeleton.GetClonedSkin(
                "cloned skin",
                includeDefaultSkin,
                unshareAttachments,
                true
            );
            skeleton.SetSkin(newSkin);

            // 2. Apply correct attachments: skeleton.SetToSetupPose + animationState.Apply
            if (state != null)
            {
                skeleton.SetToSetupPose();
                _ = state.Apply(skeleton);
            }

            // 3. Return unshared skin.
            return newSkin;
        }

        public static Skin GetClonedSkin(
            this Skeleton skeleton,
            string newSkinName,
            bool includeDefaultSkin = false,
            bool cloneAttachments = false,
            bool cloneMeshesAsLinked = true
        )
        {
            Skin newSkin = new(newSkinName); // may have null name. Harmless.
            Skin defaultSkin = skeleton.data.DefaultSkin;
            Skin activeSkin = skeleton.skin;

            if (includeDefaultSkin)
            {
                defaultSkin.CopyTo(newSkin, true, cloneAttachments, cloneMeshesAsLinked);
            }

            activeSkin?.CopyTo(newSkin, true, cloneAttachments, cloneMeshesAsLinked);

            return newSkin;
        }
        #endregion

        /// <summary>
        /// Gets a shallow copy of the skin. The cloned skin's attachments are shared with the original skin.</summary>
        public static Skin GetClone(this Skin original)
        {
            Skin newSkin = new(original.name + " clone");
            Collections.OrderedDictionary<Skin.SkinEntry, Attachment> newSkinAttachments =
                newSkin.Attachments;
            ExposedList<BoneData> newSkinBones = newSkin.Bones;
            ExposedList<ConstraintData> newSkinConstraints = newSkin.Constraints;

            foreach (KeyValuePair<Skin.SkinEntry, Attachment> a in original.Attachments)
            {
                newSkinAttachments[a.Key] = a.Value;
            }

            newSkinBones.AddRange(original.bones);
            newSkinConstraints.AddRange(original.constraints);
            return newSkin;
        }

        /// <summary>Adds an attachment to the skin for the specified slot index and name. If the name already exists for the slot, the previous value is replaced.</summary>
        public static void SetAttachment(
            this Skin skin,
            string slotName,
            string keyName,
            Attachment attachment,
            Skeleton skeleton
        )
        {
            int slotIndex = skeleton.FindSlotIndex(slotName);
            if (skeleton == null)
            {
                throw new System.ArgumentNullException("skeleton", "skeleton cannot be null.");
            }

            if (slotIndex == -1)
            {
                throw new System.ArgumentException(
                    string.Format("Slot '{0}' does not exist in skeleton.", slotName),
                    "slotName"
                );
            }

            skin.SetAttachment(slotIndex, keyName, attachment);
        }

        /// <summary>Adds skin items from another skin. For items that already exist, the previous values are replaced.</summary>
        public static void AddAttachments(this Skin skin, Skin otherSkin)
        {
            if (otherSkin == null)
            {
                return;
            }

            otherSkin.CopyTo(skin, true, false);
        }

        /// <summary>Gets an attachment from the skin for the specified slot index and name.</summary>
        public static Attachment GetAttachment(
            this Skin skin,
            string slotName,
            string keyName,
            Skeleton skeleton
        )
        {
            int slotIndex = skeleton.FindSlotIndex(slotName);
            if (skeleton == null)
            {
                throw new System.ArgumentNullException("skeleton", "skeleton cannot be null.");
            }

            if (slotIndex == -1)
            {
                throw new System.ArgumentException(
                    string.Format("Slot '{0}' does not exist in skeleton.", slotName),
                    "slotName"
                );
            }

            return skin.GetAttachment(slotIndex, keyName);
        }

        /// <summary>Adds an attachment to the skin for the specified slot index and name. If the name already exists for the slot, the previous value is replaced.</summary>
        public static void SetAttachment(
            this Skin skin,
            int slotIndex,
            string keyName,
            Attachment attachment
        )
        {
            skin.SetAttachment(slotIndex, keyName, attachment);
        }

        public static void RemoveAttachment(
            this Skin skin,
            string slotName,
            string keyName,
            SkeletonData skeletonData
        )
        {
            int slotIndex = skeletonData.FindSlotIndex(slotName);
            if (skeletonData == null)
            {
                throw new System.ArgumentNullException(
                    "skeletonData",
                    "skeletonData cannot be null."
                );
            }

            if (slotIndex == -1)
            {
                throw new System.ArgumentException(
                    string.Format("Slot '{0}' does not exist in skeleton.", slotName),
                    "slotName"
                );
            }

            skin.RemoveAttachment(slotIndex, keyName);
        }

        public static void Clear(this Skin skin)
        {
            skin.Attachments.Clear();
        }

        //[System.Obsolete]
        public static void Append(this Skin destination, Skin source)
        {
            source.CopyTo(destination, true, false);
        }

        public static void CopyTo(
            this Skin source,
            Skin destination,
            bool overwrite,
            bool cloneAttachments,
            bool cloneMeshesAsLinked = true
        )
        {
            Collections.OrderedDictionary<Skin.SkinEntry, Attachment> sourceAttachments =
                source.Attachments;
            Collections.OrderedDictionary<Skin.SkinEntry, Attachment> destinationAttachments =
                destination.Attachments;
            ExposedList<BoneData> destinationBones = destination.Bones;
            ExposedList<ConstraintData> destinationConstraints = destination.Constraints;

            if (cloneAttachments)
            {
                if (overwrite)
                {
                    foreach (KeyValuePair<Skin.SkinEntry, Attachment> e in sourceAttachments)
                    {
                        Attachment clonedAttachment = e.Value.GetCopy(cloneMeshesAsLinked);
                        destinationAttachments[
                            new Skin.SkinEntry(e.Key.SlotIndex, e.Key.Name, clonedAttachment)
                        ] = clonedAttachment;
                    }
                }
                else
                {
                    foreach (KeyValuePair<Skin.SkinEntry, Attachment> e in sourceAttachments)
                    {
                        if (destinationAttachments.ContainsKey(e.Key))
                        {
                            continue;
                        }

                        Attachment clonedAttachment = e.Value.GetCopy(cloneMeshesAsLinked);
                        destinationAttachments.Add(
                            new Skin.SkinEntry(e.Key.SlotIndex, e.Key.Name, clonedAttachment),
                            clonedAttachment
                        );
                    }
                }
            }
            else
            {
                if (overwrite)
                {
                    foreach (KeyValuePair<Skin.SkinEntry, Attachment> e in sourceAttachments)
                    {
                        destinationAttachments[e.Key] = e.Value;
                    }
                }
                else
                {
                    foreach (KeyValuePair<Skin.SkinEntry, Attachment> e in sourceAttachments)
                    {
                        if (destinationAttachments.ContainsKey(e.Key))
                        {
                            continue;
                        }

                        destinationAttachments.Add(e.Key, e.Value);
                    }
                }
            }

            foreach (BoneData data in source.bones)
            {
                if (!destinationBones.Contains(data))
                {
                    destinationBones.Add(data);
                }
            }

            foreach (ConstraintData data in source.constraints)
            {
                if (!destinationConstraints.Contains(data))
                {
                    destinationConstraints.Add(data);
                }
            }
        }
    }
}

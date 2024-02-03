#if UNITY_EDITOR
using Cinemachine;
using Cinemachine.PostFX;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utils
{
    internal class CinemashinePostProcessingSceneAdderWindow : EditorWindow
    {
        private VolumeProfile volumeProfiler;

        [MenuItem(
            "Tools/CinemashinePostProcessingSceneAdder/Add Cinemashine VolumeSettings extension window"
        )]
        public static void ShowWindow()
        {
            _ = EditorWindow.GetWindow(typeof(CinemashinePostProcessingSceneAdderWindow));
        }

        private void OnGUI()
        {
            volumeProfiler = (VolumeProfile)
                EditorGUILayout.ObjectField(
                    "Volume Profile",
                    volumeProfiler,
                    typeof(VolumeProfile),
                    false
                );

            _ = EditorGUILayout.BeginHorizontal();
            if (
                GUILayout.Button("Add Cinemashine VolumeSettings extensions")
                && volumeProfiler != null
            )
            {
                CinemachineVirtualCamera[] virtualCameras =
                    GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
                foreach (CinemachineVirtualCamera camera in virtualCameras)
                {
                    if (camera.TryGetComponent(out CinemachineVolumeSettings volumeSettings))
                    {
                        GameObject.DestroyImmediate(volumeSettings);
                    }
                    CinemachineVolumeSettings newCinemachineVolumeSettings =
                        camera.gameObject.AddComponent<CinemachineVolumeSettings>();
                    newCinemachineVolumeSettings.m_Profile = volumeProfiler;
                }
                Debug.Log("Cinemashine VolumeSettings extension added");
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif

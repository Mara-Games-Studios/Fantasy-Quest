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
                EditorGUILayout.ObjectField("My Texture", null, typeof(VolumeProfile), false);

            _ = EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Cinemashine VolumeSettings extensions"))
            {
                CinemachineVirtualCamera[] virtualCameras =
                    GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
                foreach (CinemachineVirtualCamera camera in virtualCameras)
                {
                    if (camera.TryGetComponent(out CinemachineVolumeSettings volumeSettings))
                    {
                        GameObject.Destroy(volumeSettings);
                    }
                    CinemachineVolumeSettings newVolumeSettings =
                        new() { m_Profile = volumeProfiler };
                    camera.AddExtension(newVolumeSettings);
                }
                Debug.Log("Cinemashine VolumeSettings extension added");
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

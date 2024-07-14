#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endif

namespace Common
{
    internal static class AssetsToolsCommands
    {
#if UNITY_EDITOR
        [MenuItem("Asset Tools/Force Reserialize ALL Assets")]
        public static void ForceReserializeAssets()
        {
            string[] guids = AssetDatabase.FindAssets("", null);
            Debug.Log("Start Reserialize " + guids.Length + " assets.");
            foreach (string guid in guids)
            {
                Reserialize(guid);
            }
        }

        private static void Reserialize(string guid)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            try
            {
                AssetDatabase.ForceReserializeAssets(
                    new List<string>() { path },
                    ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata
                );
            }
            catch (System.Exception)
            {
                Debug.Log("Error in " + path);
            }
        }
#endif
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Common
{
    internal static class AssetsToolsCommands
    {
        [MenuItem("Tools/Force Reserialize ALL Assets")]
        public static void ForceReserializeAssets()
        {
            string[] guids = AssetDatabase.FindAssets("", null);
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
    }
}

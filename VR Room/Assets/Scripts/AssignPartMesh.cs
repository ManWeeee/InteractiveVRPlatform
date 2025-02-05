using UnityEditor;
using UnityEngine;

public class AssignPartMesh : MonoBehaviour
{
    [MenuItem("Tools/Assign Part Mesh")]
    public static void GenerateAndAssignPartInfo()
    {
        Object[] selectedPrefabs = Selection.objects;

        foreach (Object obj in selectedPrefabs)
        {
            if (obj is GameObject prefab)
            {
                var part = prefab.GetComponent<Part>();
                var partMeshFilter = prefab.GetComponentInChildren<MeshFilter>();

                if (part.PartInfo.PartMesh == null)
                {
                    Debug.LogWarning($"Skipping {prefab.name}: No valid PartInfo reference found.");
                    continue;
                }

                if (partMeshFilter == null)
                {
                    Debug.LogWarning($"Skipping {prefab.name}: No MeshFilter component found.");
                    continue;
                }

                if (part.PartInfo.PartMesh == null)
                {
                    Debug.LogWarning($"Skipping {prefab.name}: PartInfo does not contain a valid mesh.");
                    continue;
                }

                // Modify prefab correctly
                string path = AssetDatabase.GetAssetPath(prefab);
                GameObject prefabInstance = PrefabUtility.LoadPrefabContents(path);

                if (prefabInstance != null)
                {
                    MeshFilter instanceMeshFilter = prefabInstance.GetComponentInChildren<MeshFilter>();
                    if (instanceMeshFilter != null)
                    {
                        instanceMeshFilter.sharedMesh = part.PartInfo.PartMesh;
                        PrefabUtility.SaveAsPrefabAsset(prefabInstance, path);
                        Debug.Log($"Assigned mesh to {prefab.name} and saved changes.");
                    }
                    PrefabUtility.UnloadPrefabContents(prefabInstance);
                }
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Finished assigning meshes to selected objects.");
    }
}

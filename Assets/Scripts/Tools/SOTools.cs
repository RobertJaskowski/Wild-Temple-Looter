
using UnityEditor;
using UnityEngine;

public class SOTools
{
#if UNITY_EDITOR
    public static ScriptableObject GetExistingSO<T>(string name) where T: ScriptableObject
    {
        T returnValue = default(T);

        string[] ass = AssetDatabase.FindAssets(name, new string[] { "Assets" });
        returnValue = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(ass[0]));

        return returnValue;
    }

    public static void ChangeSOName(ScriptableObject myAsset, string newName)
    {
        string assetPath = AssetDatabase.GetAssetPath(myAsset.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, newName);
        AssetDatabase.SaveAssets();
    }
#endif
}

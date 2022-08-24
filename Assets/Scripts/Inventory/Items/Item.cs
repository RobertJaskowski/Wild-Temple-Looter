using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{
    [ReadOnly]
    public string ID;
    public string ItemName;
    public Sprite Icon;

#if UNITY_EDITOR
    public void OnValidate()  
    {
        if (ID == null || ID == "")
        {
            string path = AssetDatabase.GetAssetPath(this);
            ID = AssetDatabase.AssetPathToGUID(path);

        }
        if (ItemName != name)  
            ItemName = name;
    }
#endif

    
}

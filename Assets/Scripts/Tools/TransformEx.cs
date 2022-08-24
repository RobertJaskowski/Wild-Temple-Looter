
using UnityEngine;

public static class TransformEx
{

    public static void DestroyAllChildGameObjects(this Transform transform)
    {
        int childs = transform.childCount;
        for (int i= 0; i < childs; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}

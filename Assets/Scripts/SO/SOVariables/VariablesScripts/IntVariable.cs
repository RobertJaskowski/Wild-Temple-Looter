using System;
using UnityEngine;

[CreateAssetMenu (menuName ="Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int InitialValue;

    [NonSerialized]
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}

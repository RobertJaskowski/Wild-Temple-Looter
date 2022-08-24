using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/BoolVariable")]
public class BoolVariable : ScriptableObject
{
    public bool InitialValue;

    [NonSerialized]
    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Custom/DropTableVariable")]
public class DropTableVariable : ScriptableObject
{
    public DropTable InitialValue;

    [NonSerialized]
    public DropTable RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}

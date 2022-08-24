using System;
using System.Runtime.Serialization;

[Serializable]
public class EquipmentSaveInfo
{
    public string WeaponInstanceID;
    public EquipabbleItemData WeaponData;
    public string ChestInstanceID;
    public EquipabbleItemData ChestData;

    
}

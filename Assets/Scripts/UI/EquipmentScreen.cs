using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentScreen : MonoBehaviour
{
    public Equipment equipment;

    public EquipmentSlot[] equipmentSlots;

    public void RefreshUI()
    {
        foreach (EquipmentSlot slot in equipmentSlots)
        {
            switch (slot.EquipmentType)
            {
                case EquipmentType.Weapon:
                    slot.Item = equipment.Weapon.ItemName != null ? equipment.Weapon : null;
                    break;
                case EquipmentType.Chest:
                    slot.Item = equipment.Chest.ItemName != null ? equipment.Chest : null;
                    break;

            }
        }
    }
}

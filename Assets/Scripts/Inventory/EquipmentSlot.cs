using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType EquipmentType;

    public override void ItemSlotClicked()
    {
        //base.ItemSlotClicked();
        if (Item is Equipableitem)
            ItemViewScreen.instance.ShowScreenWithItem((Equipableitem)Item, true);
    }
}

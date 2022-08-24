using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region instance
    public static InventoryManager instance;


    private void Instantiate()
    {
        if (instance != null)
            Debug.Log("InventoryManager is already instantianated");

        else
            instance = this;
    }

    #endregion


    public Inventory inventory;
    public Equipment equipmentPanel;


    private void Awake()
    {
        Instantiate();
    }

    public bool HasItem(string itemID)
    {
        if (inventory.HasItem(itemID) && equipmentPanel.HasItem(itemID))
            return true;
        else
            return false;
    }

    public void Equip(Equipableitem item)
    {
        if (inventory.RemoveItem(item))
        {
            Equipableitem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }

        AudioManager.instance.Play("ItemEquip");
    }

    public void UnEquip(Equipableitem item)
    {
        if (equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
        AudioManager.instance.Play("ItemEquip");

    }
}

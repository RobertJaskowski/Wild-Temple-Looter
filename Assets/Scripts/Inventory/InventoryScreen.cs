using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour
{

    public Inventory inventory;

    public Transform ItemsParent;
    public GameObject itemSlotPrefab;

    public List<ItemSlot> ItemSlots = new List<ItemSlot>();

    public void RefreshUI()
    {
        if (ItemSlots.Count > 0)
        {
            foreach (ItemSlot item in ItemSlots)
            {
                Destroy(item.obj.gameObject);
            }
            ItemSlots.Clear();

        }

        
        for (int i = 0; i < inventory.Items.Count; i++)
        {

            GameObject slotObject = Instantiate(itemSlotPrefab, ItemsParent);
            ItemSlots.Add(slotObject.GetComponent<ItemSlot>());
            ItemSlots[i].obj = slotObject;
            ItemSlots[i].Item = inventory.Items[i];

        }
        
    }
}

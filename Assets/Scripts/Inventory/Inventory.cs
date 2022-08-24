using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Inventory")]
public class Inventory : ScriptableObject
{

    public ItemDatabase itemDatabase;

    public List<Item> Items;

    public GameEvent refreshInventoryUI;

    private void OnEnable()
    {
        Items = new List<Item>();
        SaveLoad.SaveInitiated += Save;
        SaveLoad.LoadInitiated += Load;
        
    }

    public bool HasItem(string itemID)
    {
        if (Items.Count > 0)
            foreach (Item item in Items)
            {
                if (item.ID == itemID)
                    return true;
            }
        return false;
    }

    public void AddItems(List<Item> itemList, bool create = false)
    {
        foreach (Item e in itemList)
        {
            if (e != null)
            {
                if (!create)
                    Items.Add(e);
                else
                    Items.Add(Instantiate(e));
            }
        }

        Save();
        refreshInventoryUI.Raise();

    }

    public bool AddItem(Item item, bool create = false)
    {
        if (!create)
        {
            Items.Add(item);
        }
        else
        {
            Items.Add(Instantiate(item));
        }
        Save();
        refreshInventoryUI.Raise();
        return true;

    }

    public bool RemoveItem(Item item)
    {
        if (Items.Remove(item))
        {
            Save();
            refreshInventoryUI.Raise();
            return true;
        }

        return false;
    }




    public void Save()
    {
        InventorySaveInfo info = new InventorySaveInfo();
        foreach (Item e in Items)
        {
            InventorySaveItemData d = new InventorySaveItemData(
                e.ID,
                ((Equipableitem)e).Data);

            info.itemDatas.Add(d);
        }

        SaveLoad.Save(info, SaveLoad.INVENTORY);
    }




    //public void Save()
    //{
    //    invSaveInfo = new InventorySaveInfo();
    //    invSaveInfo.EquipableItemsData = new List<EquipabbleItemData>();
    //    invSaveInfo.EquipableItemsInstances = new List<string>();
    //    foreach (Item e in Items)
    //    {
    //        if (e is Equipableitem)
    //        {
    //            invSaveInfo.EquipableItemsInstances.Add(e.ItemName);
    //            invSaveInfo.EquipableItemsData.Add(((Equipableitem)e).Data);
    //        }

    //    }
    //    //if (Items[0] != null)
    //    //    L.og("save " + invSaveInfo.EquipableItemsData[0].ItemName);
    //    SaveLoad.Save(invSaveInfo, "Inventory");
    //    //SaveLoad.GAMEFILE.test = 5;
    //    //SaveLoad.GAMEFILE.InventoryItems = Items;
    //}


    public void Load()
    {
        if (SaveLoad.SaveExists(SaveLoad.INVENTORY))
        {
            InventorySaveInfo info = SaveLoad.Load<InventorySaveInfo>(SaveLoad.INVENTORY);
            if (info.itemDatas != null)
            {
                Items.Clear();
                List<Item> listOfItemsToAdd = new List<Item>();
                foreach (InventorySaveItemData e in info.itemDatas)
                {



                    if (e.itemData != null)
                    {
                        Equipableitem item = Instantiate((Equipableitem)itemDatabase.GetItemSOByID(e.instanceID));
                        item.Data = e.itemData;
                        listOfItemsToAdd.Add(item);
                    }
                    else
                    {
                        Item item = Instantiate((Item)itemDatabase.GetItemSOByID(e.instanceID));
                        listOfItemsToAdd.Add(item);
                    }


                }

                AddItems(listOfItemsToAdd);
            }
        }
    }

    //public void Load()
    //{
    //    //if (SaveLoad.SaveExists("GameFile"))
    //    //{
    //    //    L.og(SaveLoad.GAMEFILE.test.ToString());
    //    //    L.og(SaveLoad.GAMEFILE.InventoryItems.Count.ToString());
    //    //    L.og(SaveLoad.GAMEFILE.InventoryItems[0].ItemName);
    //    //}
    //    if (SaveLoad.SaveExists("Inventory"))
    //    {
    //        invSaveInfo = new InventorySaveInfo();
    //        invSaveInfo.EquipableItemsData = new List<EquipabbleItemData>();
    //        invSaveInfo = SaveLoad.Load<InventorySaveInfo>("Inventory");


    //        if (invSaveInfo.EquipableItemsData != null)//todo this is only going through equipable item not just items
    //        {
    //            Items.Clear();
    //            List<Item> listOfItemsToAdd = new List<Item>();
    //            for (int i = 0; i < invSaveInfo.EquipableItemsInstances.Count; i++)
    //            {
    //                Equipableitem equipableitem = Instantiate((Equipableitem)ItemDatabase.GetItemSOByName(invSaveInfo.EquipableItemsInstances[i]));
    //                equipableitem.Data = invSaveInfo.EquipableItemsData[i];
    //                listOfItemsToAdd.Add(equipableitem);

    //            }
    //            AddItems(listOfItemsToAdd);
    //        }
    //    }
    //    else
    //    {
    //        invSaveInfo = new InventorySaveInfo
    //        {
    //            EquipableItemsData = new List<EquipabbleItemData>()
    //        };
    //    }
    //}
}

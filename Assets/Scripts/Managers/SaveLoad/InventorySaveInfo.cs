using System;
using System.Collections.Generic;


[Serializable]
public class InventorySaveInfo
{
    public List<InventorySaveItemData> itemDatas;

    public InventorySaveInfo()
    {
        itemDatas = new List<InventorySaveItemData>();
    }
}


[Serializable]
public class InventorySaveItemData
{
    public string instanceID;
    public EquipabbleItemData itemData;

    public InventorySaveItemData(string instanceID, EquipabbleItemData itemData)
    {
        this.instanceID = instanceID;
        this.itemData = itemData;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Equipment")]
public class Equipment : ScriptableObject
{


    public ItemDatabase itemDatabase;
    public GameEvent refreshEquipmentScreenUI;

    private Equipableitem _Weapon;
    public Equipableitem Weapon
    {
        get
        {
            if (_Weapon == null)
            {
                Equipableitem newWep = ScriptableObject.CreateInstance<Equipableitem>();
                newWep.Data = new EquipabbleItemData();
                newWep.Data.Attack = 50;
                newWep.Data.Level = 1;
                newWep.ElementalType = ElementalManager.ElementalType.Rock;
                return newWep;

            }
            else
                return _Weapon;
        }
        private set
        {
            _Weapon = value;
            refreshEquipmentScreenUI.Raise();
        }
    }


    private Equipableitem _Chest;
    public Equipableitem Chest
    {
        get
        {
            if (_Chest == null)
            {
                Equipableitem newChest = ScriptableObject.CreateInstance<Equipableitem>();
                newChest.Data = new EquipabbleItemData();
                newChest.Data.Level = 1;
                newChest.ElementalType = ElementalManager.ElementalType.Rock;
                return newChest;

            }
            else
                return _Chest;
        }
        private set
        {
            _Chest = value;
            refreshEquipmentScreenUI.Raise();
        }
    }


    private void OnEnable()
    {

        SaveLoad.SaveInitiated += Save;
        SaveLoad.LoadInitiated += Load;
    }

    public bool HasItem(string itemID)
    {
        if (Weapon.ID != null)
            if (Weapon.ID == itemID)
                return true;

        if (Chest.ID != null)
            if (Chest.ID == itemID)
                return true;

        return false;
    }

    public void AddItems(List<Equipableitem> items, out List<Equipableitem> previousItems)
    {
        List<Equipableitem> previous = new List<Equipableitem>();

        foreach (Equipableitem item in items)
        {
            switch (item.EquipmentType)
            {
                case EquipmentType.Weapon:
                    previous.Add(AssignPreviousIfNotNull(Weapon));
                    Weapon = item != null ? (Equipableitem)item : null;
                    break;
                case EquipmentType.Chest:
                    previous.Add(AssignPreviousIfNotNull(Chest));
                    Chest = item != null ? (Equipableitem)item : null;
                    break;
            }
        }
        previousItems = previous;
        Save();

    }

    public bool AddItem(Equipableitem item, out Equipableitem previousItem)
    {

        switch (item.EquipmentType)
        {
            case EquipmentType.Weapon:
                previousItem = AssignPreviousIfNotNull(Weapon);
                Weapon = item != null ? (Equipableitem)item : null;
                Save();
                return true;
            case EquipmentType.Chest:
                previousItem = AssignPreviousIfNotNull(Chest);
                Chest = item != null ? (Equipableitem)item : null;
                Save();
                return true;
        }


        previousItem = null;
        return false;
    }

    private Equipableitem AssignPreviousIfNotNull(Equipableitem currentToPrev)
    {
        return currentToPrev.ItemName != null ? currentToPrev : null;
    }

    public bool RemoveItem(Equipableitem item)
    {

        switch (item.EquipmentType)
        {
            case EquipmentType.Weapon:
                Weapon = null;
                Save();
                return true;
            case EquipmentType.Chest:
                Chest = null;
                Save();
                return true;
        }
        return false;
    }

    //public void AddItems(List<Equipableitem> items, out List<Equipableitem> previousItems)
    //{
    //    List<Equipableitem> previous = new List<Equipableitem>();

    //    foreach (Equipableitem item in items)
    //    {
    //        foreach (EquipmentSlot slot in equipmentSlots)
    //        {
    //            if (item.EquipmentType == slot.EquipmentType)
    //            {
    //                if (slot.Item != null)
    //                {
    //                    previous.Add((Equipableitem)slot.Item);
    //                }
    //                slot.Item = item;
    //            }
    //        }
    //    }
    //    RefreshCurrentEq();
    //    previousItems = previous;
    //    Save();

    //}


    //public bool AddItem(Equipableitem item, out Equipableitem previousItem)
    //{
    //    for (int i = 0; i < equipmentSlots.Length; i++)
    //    {
    //        if (equipmentSlots[i].EquipmentType == item.EquipmentType)
    //        {
    //            previousItem = (Equipableitem)equipmentSlots[i].Item;
    //            equipmentSlots[i].Item = item;
    //            RefreshCurrentEq();
    //            Save();
    //            return true;
    //        }

    //    }

    //    previousItem = null;
    //    return false;
    //}

    //private void RefreshCurrentEq()
    //{

    //    foreach (EquipmentSlot slot in equipmentSlots)
    //    {

    //        if (slot.EquipmentType == EquipmentType.Weapon)
    //            Weapon = slot.Item != null ? (Equipableitem)slot.Item : null;
    //        if (slot.EquipmentType == EquipmentType.Chest)
    //            Chest = slot.Item != null ? (Equipableitem)slot.Item : null;

    //    }

    //switch (item.EquipmentType)
    //{
    //    case EquipmentType.Weapon:
    //        Weapon = item;
    //        break;
    //    case EquipmentType.Chest:
    //        Chest = item;
    //        break;
    //}
    //}



    //public bool RemoveItem(Equipableitem item)
    //{
    //    for (int i = 0; i < equipmentSlots.Length; i++)
    //    {
    //        if (equipmentSlots[i].Item == item)
    //        {
    //            equipmentSlots[i].Item = null;
    //            RefreshCurrentEq();
    //            Save();
    //            return true;
    //        }

    //    }
    //    return false;
    //}



    public void Save()
    {
        EquipmentSaveInfo eqSaveInfo = new EquipmentSaveInfo();
        if (Weapon.ID != "")
        {
            eqSaveInfo.WeaponInstanceID = Weapon.ID;
            eqSaveInfo.WeaponData = Weapon.Data;
        }
        else
        {
            eqSaveInfo.WeaponInstanceID = null;
        }


        if (Chest.ID != "")
        {
            eqSaveInfo.ChestInstanceID = Chest.ID;
            eqSaveInfo.ChestData = Chest.Data;
        }
        else
        {
            eqSaveInfo.ChestInstanceID = null;
        }
        SaveLoad.Save<EquipmentSaveInfo>(eqSaveInfo, SaveLoad.EQUIPEMENT);
    }


    public void Load()
    {

        if (SaveLoad.SaveExists(SaveLoad.EQUIPEMENT))
        {
            EquipmentSaveInfo eqSaveInfo = SaveLoad.Load<EquipmentSaveInfo>(SaveLoad.EQUIPEMENT);
            List<Equipableitem> itemsToEquip = new List<Equipableitem>();

            if (eqSaveInfo.WeaponInstanceID != null)
            {
                Equipableitem equipableitem = Instantiate((Equipableitem)itemDatabase.GetItemSOByID(eqSaveInfo.WeaponInstanceID));
                equipableitem.Data = eqSaveInfo.WeaponData;
                itemsToEquip.Add(equipableitem);
                //AddItem(equipableitem, out Equipableitem prevWeapon);

            }
            if (eqSaveInfo.ChestInstanceID != null)
            {

                Equipableitem equipableitem = Instantiate((Equipableitem)itemDatabase.GetItemSOByID(eqSaveInfo.ChestInstanceID));
                equipableitem.Data = eqSaveInfo.ChestData;
                itemsToEquip.Add(equipableitem);
                //AddItem(equipableitem, out Equipableitem prevWeapon);

            }

            if (itemsToEquip.Count > 0)
                AddItems(itemsToEquip, out List<Equipableitem> prevItems);
        }
    }
}

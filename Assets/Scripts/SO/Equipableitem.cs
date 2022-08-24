using System;
using UnityEngine;
using static ElementalManager;

[CreateAssetMenu(menuName = "Items/EquipableItem")]
[System.Serializable]
public class Equipableitem : Item
{
    public EquipmentType EquipmentType;

    public ElementalType ElementalType;

    [Header("Optional")]
    public BlacksmithUpgradeTable upgradeTable;

    public EquipabbleItemData Data;

    


    public void CheckIfMaxLevel()
    {
        if (upgradeTable != null)
        {

            if (Data.Level >= upgradeTable.upgradeLevels.Capacity)
            {
                SetElementalImage();
            }
        }
        else
        {
            if(Data.Level >= 20)
            {
                SetElementalImage();
            }
        }


    }

    private void SetElementalImage()
    {
        string startName = Icon.texture.name;
        string cutPath = startName.Substring(0, startName.Length - 1);

        string fullPath = "Images/Items/" + cutPath + "/" + startName + "WE";
        Sprite newIcon = Resources.Load<Sprite>(fullPath);
        if (newIcon != null)
        {
            Icon = newIcon;
            L.og(startName + " " + fullPath);
        }

    }

}


[System.Serializable]
public class EquipabbleItemData
{
    public int Level;
    public int LevelProgress;
    public int Attack;
    public int Defense;
}


public enum EquipmentType
{
    Weapon,
    Chest
}



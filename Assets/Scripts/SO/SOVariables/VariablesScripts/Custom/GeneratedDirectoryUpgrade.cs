using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Variables/Custom/GeneratedDirectoryUpgrade")]
public class GeneratedDirectoryUpgrade : ScriptableObject
{

    public ItemDatabase itemDatabase;

    public Dictionary<string, UpgradeLevel> existingUpgradeLevels = new Dictionary<string, UpgradeLevel>();

    public void OnEnable()
    {
        existingUpgradeLevels = new Dictionary<string, UpgradeLevel>();
    }

    public bool Exists(string itemID)
    {
        if (existingUpgradeLevels.ContainsKey(itemID))
        {
            return true;
        }
        else { return false; }
    }

    public bool RemoveIfExists(string itemID)
    {
        if (Exists(itemID))
        {
            existingUpgradeLevels.Remove(itemID);
            return true;
        }
        else
            return false;
    }

    public void SetMaterialGeneratedInItemUpgrade(string itemID, MaterialUpgrade materialUsed)
    {
        if (existingUpgradeLevels.ContainsKey(itemID))
        {
            existingUpgradeLevels[itemID].materialUpgrades.Add(materialUsed);
        }
        else
        {
            UpgradeLevel upgradeLevel = new UpgradeLevel();
            upgradeLevel.materialUpgrades.Add(materialUsed);
            existingUpgradeLevels.Add(itemID, upgradeLevel);
        }
    }

    public CraftingMaterial GetMaterialNotUsedInItemUpgrade(string itemID)
    {
        bool materialFound = false;


        if (existingUpgradeLevels.ContainsKey(itemID))
        {

            CraftingMaterial randMat = null;
            while (!materialFound)
            {

                randMat = itemDatabase.GetRandomMaterial();
                for (int i = 0; i < existingUpgradeLevels[itemID].materialUpgrades.Count; i++) 
                {
                    if (existingUpgradeLevels[itemID].materialUpgrades[i].material.ID == randMat.ID)
                    {

                        break;
                    }
                    if (existingUpgradeLevels[itemID].materialUpgrades.Count-1 == i)
                    {
                        materialFound = true;
                    }
                }
            }
            return randMat;
        }
        else
        {
            return itemDatabase.GetRandomMaterial();
        }


    }
}

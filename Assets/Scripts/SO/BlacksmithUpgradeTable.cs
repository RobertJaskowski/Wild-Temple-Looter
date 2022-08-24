using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Blacksmith/UpgradeTable")]
public class BlacksmithUpgradeTable : ScriptableObject
{
    public List<UpgradeLevel> upgradeLevels;

    /*
#if UNITY_EDITOR
    public BlacksmithUpgradeTable pub;


    public void OnValidate()
    {
        if (pub != null)
            for (int i = 0; i < pub.upgradeLevels.Capacity-1; i++)
            {
                for (int j = 0; j < pub.upgradeLevels[i].materialUpgrades.Capacity-1; j++)
                {

                    if (upgradeLevels.Capacity < 20)
                        addsize();

                    if (pub.upgradeLevels[i].materialUpgrades.Capacity > upgradeLevels[i].materialUpgrades.Capacity) 
                        addsizetomatupg(i);

                    //if (pub.upgradeLevels[i].materialUpgrades[j].material != upgradeLevels[i].materialUpgrades[j].material)
                        upgradeLevels[i].materialUpgrades[j].material = (Material)SOTools.GetExistingSO<Material>( pub.upgradeLevels[i].materialUpgrades[j].material.name);


                    //if (pub.upgradeLevels[i].materialUpgrades[j].amount != upgradeLevels[i].materialUpgrades[j].amount)
                        upgradeLevels[i].materialUpgrades[j].amount = pub.upgradeLevels[i].materialUpgrades[j].amount;
                }
            }
    }

    private void addsizetomatupg(int i)
    {
        upgradeLevels[i].materialUpgrades.Add(null);
    }

    private void addsize()
    {
        upgradeLevels.Add(null);


        if ( upgradeLevels.Capacity < 20)
            addsize();
    }

#endif

    */
}


[System.Serializable]
public class UpgradeLevel
{
    public List<MaterialUpgrade> materialUpgrades;

    public UpgradeLevel()
    {
        materialUpgrades = new List<MaterialUpgrade>();
    }
}
[System.Serializable]
public class MaterialUpgrade
{
    public CraftingMaterial material;
    public int amount;

    public MaterialUpgrade(CraftingMaterial material, int amount)
    {
        this.material = material;
        this.amount = amount;
    }
}
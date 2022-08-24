using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Enemy/DropTable")]
public class DropTable : ScriptableObject
{
    public float legendaryItemChance;
    public List<Item> legendaryItemDrops;
    [Space(20)]
    public float rareItemChance;
    public List<Item> rareItemDrops;
    [Space(20)]
    public float normalItemChance;
    public List<Item> normalItemDrops;

    [Space(60)]

    public float rareMaterialChance;
    public List<CraftingMaterial> rareMaterialDrops;

    [Space(20)]

    public float normalMaterialChance;
    public List<CraftingMaterial> normalMaterialDrops;

    [Space(20)]

    public float moneyRate = 1;

    //generating and check if has in inv because of upgrade that has only id to instances
    public DropTable Generate()
    {
        legendaryItemChance = 3;
        legendaryItemDrops = new List<Item>();

        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Item random = ItemDatabase.instance.GetRandomItem();
            if (!InventoryManager.instance.HasItem(random.ID))
                legendaryItemDrops.Add(random);
        }

        //rareItemChance = 15;
        //rareItemDrops = new List<Item>();

        //for (int i = 0; i < RandomEx.MaxRange(0); i++)
        //{
        //    rareItemDrops.Add(ItemDatabase.instance.GetRandomItem());
        //}

        //normalItemChance = 30;
        //normalItemDrops = new List<Item>();

        //for (int i = 0; i < RandomEx.MaxRange(0); i++)
        //{
        //    normalItemDrops.Add(ItemDatabase.instance.GetRandomItem());
        //}

        //materials

        rareMaterialChance = 20;
        rareMaterialDrops = new List<CraftingMaterial>();

        for (int i = 0; i < RandomEx.MaxRange(2); i++)
        {
            rareMaterialDrops.Add(ItemDatabase.instance.GetRandomMaterial());
        }


        normalMaterialChance = 40;
        normalMaterialDrops = new List<CraftingMaterial>();

        for (int i = 0; i < RandomEx.MaxRange(2); i++)
        {
            normalMaterialDrops.Add(ItemDatabase.instance.GetRandomMaterial());
        }



        return this;

    }

}

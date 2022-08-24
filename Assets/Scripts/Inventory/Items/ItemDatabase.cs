using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    #region instance

    public static ItemDatabase instance;

    public void Instantinate()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    

    public void GetInstance()
    {

    }

    public List<Item> AllItems;
    public static Dictionary<string, Item> AllItemsReferencesIDs;
    [Space(20)]
    public List<CraftingMaterial> AllMaterials;
    public static Dictionary<string, CraftingMaterial> AllMaterialsReferencesIDs;

    void OnEnable()
    {
        Instantinate();

        AssignItemReferences();
        AssignMaterialReferences();
    }

    public int GetNumberOfAvailableItems()
    {
        return AllItems.Count;
    }

    public int GetNumberOfAvailableMaterials()
    {
        return AllMaterials.Count;
    }


    public Equipableitem GetRandomItem()
    {
        return (Equipableitem)AllItems[UnityEngine.Random.Range(0, GetNumberOfAvailableItems() - 1)];
    }


    public CraftingMaterial GetRandomMaterial()
    {

        return AllMaterials[UnityEngine.Random.Range(0, GetNumberOfAvailableMaterials() - 1)];
    }



    public static Item GetItemSOByName(string SOName)
    {
        foreach (Item e in AllItemsReferencesIDs.Values)
        {

            if (e.ItemName == SOName)
                return e;
        }

        return null;

    }

    public Item GetItemSOByID(string id)
    {
        return AllItemsReferencesIDs[id];
    }

    public static Item GetMaterialSOByName(string SOName)
    {
        foreach (Item e in AllMaterialsReferencesIDs.Values)
        {

            if (e.ItemName == SOName)
                return e;
        }

        return null;
    }

    public static Item GetMaterialSOByID(string id)
    {
        return AllMaterialsReferencesIDs[id];
    }

    


    private void AssignItemReferences()
    {
        if (AllItemsReferencesIDs == null)
        {
            AllItemsReferencesIDs = new Dictionary<string, Item>();
            foreach (Item item in AllItems)
            {
                AllItemsReferencesIDs.Add(item.ID, item);
            }
        }
    }
    private void AssignMaterialReferences()
    {
        if (AllMaterialsReferencesIDs == null)
        {
            AllMaterialsReferencesIDs = new Dictionary<string, CraftingMaterial>();
            foreach (CraftingMaterial material in AllMaterials)
            {
                AllMaterialsReferencesIDs.Add(material.ID, material);
            }
        }
    }
}

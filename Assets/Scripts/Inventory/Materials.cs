using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Materials")]
public class Materials : ScriptableObject
{

    public Dictionary<string, MaterialOwned> MaterialsList = new Dictionary<string, MaterialOwned>();



    public class MaterialOwned
    {
        public CraftingMaterial material;
        public int amountOwned;
    }

    private void OnEnable()
    {
        SaveLoad.SaveInitiated += Save;
        SaveLoad.LoadInitiated += Load;
    }

    public void RemoveMaterial(CraftingMaterial material)
    {
        MaterialOwned matOwn;
        MaterialsList.TryGetValue(material.ItemName, out matOwn);
        MaterialsList.Remove(material.ItemName);

        if (matOwn.amountOwned >= material.amount)
        {
            matOwn.amountOwned -= material.amount;
            if (matOwn.amountOwned > 0)
                MaterialsList.Add(material.ItemName, matOwn);
        }
    }


    public int GetMaterialAmount(CraftingMaterial material)
    {
        if (MaterialsList.ContainsKey(material.ItemName))
        {
            MaterialOwned owned = MaterialsList[material.ItemName];
            return owned.amountOwned;
        }
        else
        {
            return 0;
        }
    }

    public void AddMaterials(List<CraftingMaterial> materials)
    {
        foreach (CraftingMaterial material in materials)
        {
            if (MaterialsList.ContainsKey(material.ItemName))
            {
                MaterialOwned matOwn;
                MaterialsList.TryGetValue(material.ItemName, out matOwn);
                MaterialsList.Remove(material.ItemName);

                matOwn.amountOwned += material.amount;
                MaterialsList.Add(material.ItemName, matOwn);

            }
            else
            {
                MaterialOwned matOwn = new MaterialOwned();
                matOwn.material = material;
                matOwn.amountOwned = material.amount;
                MaterialsList.Add(material.ItemName, matOwn);
            }
        }


        Save();
    }

    public void AddMaterial(CraftingMaterial material)
    {
        if (MaterialsList.ContainsKey(material.ItemName))
        {
            MaterialOwned matOwn;
            MaterialsList.TryGetValue(material.ItemName, out matOwn);
            MaterialsList.Remove(material.ItemName);

            matOwn.amountOwned += material.amount;
            MaterialsList.Add(material.ItemName, matOwn);

        }
        else
        {
            MaterialOwned matOwn = new MaterialOwned();
            matOwn.material = material;
            matOwn.amountOwned = material.amount;
            MaterialsList.Add(material.ItemName, matOwn);
        }

        Save();
    }




    public void Save()
    {
        MaterialsSaveData materialsSaveData = new MaterialsSaveData();


        foreach (string material in MaterialsList.Keys)
        {
            MaterialOwned matOwn = MaterialsList[material];
            MaterialsDataToSave d = new MaterialsDataToSave(matOwn.material.ID, matOwn.amountOwned);
            materialsSaveData.materialsData.Add(d);
        }

        SaveLoad.Save<MaterialsSaveData>(materialsSaveData, SaveLoad.MATERIALS);

    }

    public void Load()
    {
        if (SaveLoad.SaveExists(SaveLoad.MATERIALS))
        {
            MaterialsList = new Dictionary<string, MaterialOwned>();
            MaterialsSaveData materialsSaveData = SaveLoad.Load<MaterialsSaveData>(SaveLoad.MATERIALS);
            List<CraftingMaterial> materialsToAdd = new List<CraftingMaterial>();

            foreach (MaterialsDataToSave item in materialsSaveData.materialsData)
            {
                CraftingMaterial material = Instantiate((CraftingMaterial)ItemDatabase.GetMaterialSOByID(item.instanceID));
                material.amount = item.amount;
                materialsToAdd.Add(material);
            }

            //for (int i = 0; i < materialsSaveData.MaterialsInstancesNames.Count; i++)
            //{
            //    Material material = Instantiate((Material)ItemDatabase.GetMaterialSOByID(materialsSaveData.MaterialsInstancesNames[i]));
            //    material.amount = materialsSaveData.MaterialsAmount[i];
            //    materialsToAdd.Add(material);
            //}
            AddMaterials(materialsToAdd);
        }
        else
        {
            MaterialsList = new Dictionary<string, MaterialOwned>();
        }
    }
}

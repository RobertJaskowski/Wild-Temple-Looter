using System;
using System.Collections.Generic;

[Serializable]
public class MaterialsSaveData 
{
    public List<MaterialsDataToSave> materialsData;

    public MaterialsSaveData()
    {
        materialsData = new List<MaterialsDataToSave>();
    }
}

[Serializable]
public class MaterialsDataToSave
{
    public string instanceID;
    public int amount;

    public MaterialsDataToSave(string instanceID, int amount)
    {
        this.instanceID = instanceID;
        this.amount = amount;
    }
}

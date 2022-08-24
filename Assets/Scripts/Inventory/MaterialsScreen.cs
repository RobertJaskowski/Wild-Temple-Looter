using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Materials;

public class MaterialsScreen : MonoBehaviour
{
    public Materials materials;

    public Transform materialsContentParent;
    public GameObject materialPrefab;

    public void RefreshUI()
    {
        foreach (Transform child in materialsContentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (string material in materials.MaterialsList.Keys)
        {
            GameObject materialSlotObject = Instantiate(materialPrefab, materialsContentParent);
            MaterialSlot slot = materialSlotObject.GetComponent<MaterialSlot>();
            MaterialOwned matOwn = materials.MaterialsList[material];
            

            slot.image.sprite = matOwn.material.Icon;
            slot.name.text = matOwn.material.ItemName;
            slot.amount.text = materials.MaterialsList[matOwn.material.ItemName].amountOwned.ToString();

        }

    }
}

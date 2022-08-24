using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithMaterialSlot : MonoBehaviour
{

    public Materials materials;

    [Space(20)]
    public Sprite validImage;
    public Sprite invalidImage;
    public Image isValidImage;
    public Image materialImage;
    public Image background;
    public List<Sprite> buttonsImagebackground;
    public TextMeshProUGUI materialsNeededText;


    public CraftingMaterial materialNeeded;
    private int materialAmount;

    private bool HaveEnoughMaterial = false;


    public void ConsumeMaterial()
    {
        materialNeeded.amount = materialAmount;
        materials.RemoveMaterial(materialNeeded);
    }

    public void SetMaterialsNeeded(CraftingMaterial material)
    {
        materialNeeded = material;
        materialAmount = material.amount;
        SetUpUI();
    }

    private void SetUpUI()
    {
        background.sprite = GetRandomBackgroundImage();
        materialImage.sprite = materialNeeded.Icon;

        int availableMaterials = materials.GetMaterialAmount(materialNeeded);
        if (availableMaterials >= materialNeeded.amount)
        {
            isValidImage.sprite = validImage;
            materialsNeededText.text = materialAmount + "/" + materialAmount;
            HaveEnoughMaterial = true;
        }
        else
        {
            isValidImage.sprite = invalidImage;
            materialsNeededText.text = availableMaterials + "/" + materialAmount;
            HaveEnoughMaterial = false;
        }

    }

    public bool HaveEnoughMaterials()
    {
        return HaveEnoughMaterial;
    }

    private Sprite GetRandomBackgroundImage()
    {
        return buttonsImagebackground[UnityEngine.Random.Range(0, buttonsImagebackground.Count - 1)];
    }
}

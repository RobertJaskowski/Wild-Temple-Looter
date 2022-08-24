using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithScreen : MonoBehaviour
{
    #region instance

    public static BlacksmithScreen instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("BlacksmithScreen" +
                " already instantiated");
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public GeneratedDirectoryUpgrade generatedUpgrades;


    public ItemDatabase itemDatabase;
    public Equipment equipment;
    public Inventory inventory;

    [Space(20)]
    public GameObject screen;
    public GameObject craftButton;
    public Image itemImage;
    public GameObject materialSlotPrefab;
    public GameObject materialsContentParent;
    public TextMeshProUGUI costOfUpgradeText;
    public TextMeshProUGUI feedbackText;

    [Space(20)]
    public TextMeshProUGUI currentItemLevelText;
    public TextMeshProUGUI currentItemLevelValueText;
    public TextMeshProUGUI currentItemLevelUpgradeValueText;
    public TextMeshProUGUI currentItemLevelAfterUpgradeValueText;
    [Space(20)]
    public TextMeshProUGUI currentItemStatText;
    public TextMeshProUGUI currentItemStatValueText;
    public TextMeshProUGUI currentItemStatUpgradeValueText;
    public TextMeshProUGUI currentItemStatAfterUpgradeValueText;


    [Space(20)]
    public IntReference playerLevel;
    public IntGameEvent playerGoldUpdate;
    public IntReference playerGold;
    public GameEvent refreshItemViewUI;
    public GameEvent refreshInventoryUI;

    [Space(20)]

    [ReadOnly]
    public List<BlacksmithMaterialSlot> materialsSlots;


    private Equipableitem ViewedItem;
    private UpgradeLevel CurrentUpgradeLevel;




    private void Awake()
    {
        Instantiate();
    }

    public void ShowBlacksmithWithItem(Equipableitem item)
    {
        ShowBlacksmith();
        ViewedItem = item;

        RefreshItem();



    }

    public void Craft()
    {


        foreach (BlacksmithMaterialSlot slot in materialsSlots)
        {
            slot.ConsumeMaterial();
        }
        playerGoldUpdate.UpdateValue(-GetCostOfUpgrade()).Raise();

        UpgradeItem();

        generatedUpgrades.RemoveIfExists(ViewedItem.ID);

        inventory.Save();
        equipment.Save();
        RefreshItem();
        refreshItemViewUI.Raise();
        refreshInventoryUI.Raise();
        RefreshFeedbackText("Crafted!");

        AudioManager.instance.Play("BlacksmithCraft");

    }

    private void UpgradeItem()
    {
        ViewedItem.Data.Level += 1;
        if (ViewedItem.Data.Attack > 0)
            ViewedItem.Data.Attack += GetMainStatUpgradeValue();
        else
            ViewedItem.Data.Defense += GetMainStatUpgradeValue();
    }


    private void RefreshItem()
    {

        //AssignCurrentUpgradeLevel();



        RefreshTextUI();
        ClearMaterialSlots();




        if (generatedUpgrades.Exists(ViewedItem.ID))
        {
            CurrentUpgradeLevel = generatedUpgrades.existingUpgradeLevels[ViewedItem.ID];
        }
        else
        {
            GenerateUpgradeLevel();

        }

        RefreshMaterialSlots();

        RefreshCraftingButtonVisibility();
        RefreshFeedbackText();
    }

    private void RefreshMaterialSlots()
    {
        foreach (MaterialUpgrade upgrade in CurrentUpgradeLevel.materialUpgrades)
        {
            BlacksmithMaterialSlot slot = Instantiate(materialSlotPrefab, materialsContentParent.transform).GetComponent<BlacksmithMaterialSlot>();
            upgrade.material.amount = upgrade.amount;
            slot.SetMaterialsNeeded(upgrade.material);
            materialsSlots.Add(slot);

        }
    }

    private void GenerateUpgradeLevel()
    {
        CurrentUpgradeLevel = new UpgradeLevel();

        int avbmat = itemDatabase.GetNumberOfAvailableMaterials();


        int maxMatNeeded = ViewedItem.Data.Level < avbmat ? ViewedItem.Data.Level : avbmat;


        for (int i = 0; i < maxMatNeeded; i++)
        {

            CraftingMaterial mat = generatedUpgrades.GetMaterialNotUsedInItemUpgrade(ViewedItem.ID);

            MaterialUpgrade materialUsed = new MaterialUpgrade(mat, GetAmountOfMaterial());
            generatedUpgrades.SetMaterialGeneratedInItemUpgrade(ViewedItem.ID, materialUsed);
            CurrentUpgradeLevel.materialUpgrades.Add(materialUsed);
        }
    }

    private void ClearMaterialSlots()
    {
        if (materialsSlots.Count > 0)
        {
            foreach (BlacksmithMaterialSlot slot in materialsSlots)
            {
                Destroy(slot.transform.gameObject);
            }
            materialsSlots.Clear();

        }
    }

    private int GetAmountOfMaterial()
    {
        int amount = 1;

        for (int i = 0; i < ViewedItem.Data.Level; i++)
        {
            amount *= 2;
        }
        return amount;
    }


    /*
   private void AssignCurrentUpgradeLevel()
   {
       if (ViewedItem.upgradeTable != null)
       {
           AssignCurrentUpgradeLevelFromItem();  
       }
       else
       {
           AssignCurrentUpgradeLevelFromDefaults();
       }
   }

   private void AssignCurrentUpgradeLevelFromItem()
   {

       if (ViewedItem.Data.Level >= ViewedItem.upgradeTable.upgradeLevels.Count - 1)
           CurrentUpgradeLevel = ViewedItem.upgradeTable.upgradeLevels[ViewedItem.upgradeTable.upgradeLevels.Count - 1];
       else
           CurrentUpgradeLevel = ViewedItem.upgradeTable.upgradeLevels[ViewedItem.Data.Level];

   }

   private void AssignCurrentUpgradeLevelFromDefaults()
   {
       int upgLevel = ViewedItem.Data.Level >= 20 ? 20 : ViewedItem.Data.Level;

       switch (ViewedItem.ElementalType)
       {
           case ElementalManager.ElementalType.Fire:
               CurrentUpgradeLevel = fireUpgradeTable.upgradeLevels[upgLevel];
               break;
           case ElementalManager.ElementalType.Nature:
               CurrentUpgradeLevel = natureUpgradeTable.upgradeLevels[upgLevel];
               break;
           case ElementalManager.ElementalType.Rock:
               CurrentUpgradeLevel = rockUpgradeTable.upgradeLevels[upgLevel];
               break;
           case ElementalManager.ElementalType.Electricity:
               CurrentUpgradeLevel = electricityUpgradeTable.upgradeLevels[upgLevel];
               break;
           case ElementalManager.ElementalType.Water:
               CurrentUpgradeLevel = waterUpgradeTable.upgradeLevels[upgLevel];
               break;
           case ElementalManager.ElementalType.Holy:
               CurrentUpgradeLevel = holyUpgradeTable.upgradeLevels[upgLevel];
               break;
           case ElementalManager.ElementalType.Dark:
               CurrentUpgradeLevel = darkUpgradeTable.upgradeLevels[upgLevel];
               break;
       }
   }
   */
    private void RefreshTextUI()
    {
        itemImage.sprite = ViewedItem.Icon;
        costOfUpgradeText.text = GetCostOfUpgrade().ToString();


        currentItemLevelText.text = "Level";
        currentItemLevelValueText.text = ViewedItem.Data.Level.ToString();
        currentItemLevelUpgradeValueText.text = "+1";
        currentItemLevelAfterUpgradeValueText.text = (ViewedItem.Data.Level + 1).ToString();


        if (ViewedItem.Data.Attack > 0)
        {
            currentItemStatText.text = "Attack";
            currentItemStatValueText.text = ViewedItem.Data.Attack.ToString();
            currentItemStatAfterUpgradeValueText.text = (ViewedItem.Data.Attack + GetMainStatUpgradeValue()).ToString();

        }
        else
        {
            currentItemStatText.text = "Defense";
            currentItemStatValueText.text = ViewedItem.Data.Defense.ToString();
            currentItemStatAfterUpgradeValueText.text = (ViewedItem.Data.Defense + GetMainStatUpgradeValue()).ToString();
        }

        currentItemStatUpgradeValueText.text = "+" + GetMainStatUpgradeValue().ToString();
    }

    private int GetMainStatUpgradeValue()
    {
        return ViewedItem.Data.Level + 1;
    }

    private void RefreshFeedbackText()
    {
        if (!CanCraft())
            RefreshFeedbackText("Not enough resources");
        else
            DisableFeedbackText();
    }
    

    private void RefreshFeedbackText(string textToDisplay)
    {
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = textToDisplay;

    }

    public void DisableFeedbackText()
    {
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(false);
    }

    private void RefreshCraftingButtonVisibility()
    {
        if (CanCraft())
        {
            craftButton.SetActive(true);
            L.og("can craft vi");
        }
        else
        {
            craftButton.SetActive(false);
        }
    }

    private bool CanCraft()
    {
        if (HaveEnoughResources() && HaveEnoughMoney())
            return true;
        else
            return false;
    }

    private bool HaveEnoughMoney()
    {
        if (playerGold.Value >= GetCostOfUpgrade())
            return true;
        else
            return false;
    }

    private int GetCostOfUpgrade()
    {
        return ViewedItem.Data.Level * 500 * playerLevel.Value;
    }

    private bool HaveEnoughResources()
    {
        foreach (BlacksmithMaterialSlot slot in materialsSlots)
        {
            if (slot.HaveEnoughMaterials())
                continue;
            else
                return false;
        }
        return true;
    }

    private void ShowBlacksmith()
    {
        screen.SetActive(true);
        AudioManager.instance.Play("UIOpenedClosed");
    }

    public void HideBlacksmith()
    {
        screen.SetActive(false);

    }
}

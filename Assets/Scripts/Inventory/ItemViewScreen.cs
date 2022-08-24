using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewScreen : MonoBehaviour
{
    #region instance

    public static ItemViewScreen instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("ItemViewScreen" +
                " already instantiated");
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public Equipment equipment;
    public Inventory inventory;
    public GameObject itemViewScreen;
    public GameObject confirmationBox;

    


    [Space(20)]

    public Image ItemImage;
    public TextMeshProUGUI ItemLevel;
    public Image ItemElemental;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemMainStatName;
    public TextMeshProUGUI ItemMainStatValue;

    [Space(20)]
    public TextMeshProUGUI EquipUnequipButtonText;

    [SerializeField][HideInInspector]
    private Equipableitem CurrentlyViewedItem;
    private bool CurrentlyItemEquiped = false;

    private void Awake()
    {
        Instantiate();
    }

    public void ShowScreen()
    {
        itemViewScreen.SetActive(true);
        AudioManager.instance.Play("UIOpenedClosed");
    }

    public void HideScreen()
    {
        itemViewScreen.SetActive(false);
    }

    public void ShowScreenWithItem(Equipableitem item, bool equipped = false)
    {
        ShowScreen();
        CurrentlyViewedItem = item;
        CurrentlyItemEquiped = equipped;


        RefreshItemData();
        RefreshEquipUnequipButton();
    }

    public void RefreshItemData()
    {
        ItemImage.sprite = CurrentlyViewedItem.Icon;
        ItemLevel.text = CurrentlyViewedItem.Data.Level.ToString();
        ItemElemental.sprite = ElementalManager.GetElementalImage(CurrentlyViewedItem.ElementalType);
        ItemName.text = CurrentlyViewedItem.ItemName;
        if (CurrentlyViewedItem.Data.Attack > 0)
        {
            ItemMainStatName.text = "Attack";
            ItemMainStatValue.text = CurrentlyViewedItem.Data.Attack.ToString();
        }
        else
        {
            ItemMainStatName.text = "Defense";
            ItemMainStatValue.text = CurrentlyViewedItem.Data.Defense.ToString();
        }
    }

    private void RefreshEquipUnequipButton()
    {
        if (CurrentlyItemEquiped)
        {
            EquipUnequipButtonText.text = "Unequip";
        }
        else
        {
            EquipUnequipButtonText.text = "Equip";
        }
    }

    public void EquipUnequipItem()
    {
        if (CurrentlyItemEquiped)
        {
            InventoryManager.instance.UnEquip(CurrentlyViewedItem);
            CurrentlyItemEquiped = false;
            RefreshEquipUnequipButton();
        }
        else
        {
            InventoryManager.instance.Equip(CurrentlyViewedItem);
            CurrentlyItemEquiped = true;
            RefreshEquipUnequipButton();

        }
    }

    public void ShowBlacksmithWithItem()
    {
        BlacksmithScreen.instance.ShowBlacksmithWithItem(CurrentlyViewedItem);
    }

    public void DestroyButton()
    {
        confirmationBox.SetActive(true);
    }

    public void CancelDestroy()
    {
        confirmationBox.SetActive(false);
    }

    public void ConfirmDestroy()
    {
        DestroyCurrentItem();
    }

    public void DestroyCurrentItem()
    {
        if (CurrentlyItemEquiped)
        {
            equipment.RemoveItem(CurrentlyViewedItem);
        }
        else
        {
            inventory.RemoveItem(CurrentlyViewedItem);
        }
        HideScreen();
        CancelDestroy();
    }
}

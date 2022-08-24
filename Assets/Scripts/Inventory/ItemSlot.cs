using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour
{
    [ReadOnly]
    public GameObject obj;

    public Image Image;
    public Image starImage;

    [SerializeField]
    [ReadOnly]
    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                Image.enabled = false;
            }
            else
            {
                Image.sprite = _item.Icon;
                Image.enabled = true;
                if (value is Equipableitem)
                {
                    ((Equipableitem)value).CheckIfMaxLevel();
                    EnableStarIfUpgraded();
                }
            }
        }
    }

    private void EnableStarIfUpgraded()
    {

        if (starImage != null)
            if (((Equipableitem)Item).Data.Level < 1)
                starImage.enabled = false;
    }

    public virtual void ItemSlotClicked()
    {
        if (Item is Equipableitem)
        {
            ItemViewScreen.instance.ShowScreenWithItem((Equipableitem)Item);
        }

        //InventoryManager.instance.Equip((Equipableitem)Item);
    }
}

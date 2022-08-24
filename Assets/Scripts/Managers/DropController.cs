
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class DropController : MonoBehaviour
{
    #region instance
    public static DropController instance;



    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("Drop controller already instanciated");

        }
        else
        {
            instance = this;
        }
    }

    #endregion


    private GameObject dropRoot;
    public GameObject dropPrefab;

    public Sprite goldIcon;

    public IntGameEvent goldValueEvent;

    public IntReference goldLooted;

    public Materials materials;


    public FloatVariable dungeonDifficulty;


    private DropTable currentTable;

    private void Awake()
    {
        Instantiate();
    }

    private void InitializeDropRoot()
    {
        if (dropRoot == null)
            dropRoot = GameObject.Find("DropRoot");
    }


    public void Initiate(DropTable dropTableToUse, bool modifyValuesToBoss = false)
    {

        currentTable = modifyValuesToBoss == true ? ModifyDropTableValues(dropTableToUse) : dropTableToUse;


        RollChanceForItem(currentTable);

        RollChanceForMaterial(currentTable);

        DropMoney(currentTable);

        L.og(currentTable.legendaryItemChance.ToString());

        if (modifyValuesToBoss)
            ModifyDropTableValuesBackToNormal(currentTable);

        AudioManager.instance.Play("Dropped");

    }

    public DropTable ModifyDropTableValues(DropTable dropTable)
    {
        dropTable.legendaryItemChance *= 2;
        dropTable.legendaryItemChance *= dungeonDifficulty.RuntimeValue;
        dropTable.moneyRate *= 2;
        dropTable.moneyRate *= dungeonDifficulty.RuntimeValue;

        return dropTable;
    }

    public DropTable ModifyDropTableValuesBackToNormal(DropTable dropTable)
    {
        dropTable.legendaryItemChance /= 2;
        dropTable.legendaryItemChance /= dungeonDifficulty.RuntimeValue;
        dropTable.moneyRate /= 5;
        dropTable.moneyRate /= dungeonDifficulty.RuntimeValue;

        return dropTable;
    }



    public void RollChanceForItem(DropTable dropTable)
    {
        float rolled = Random.Range(0, 100);
        if (rolled < dropTable.legendaryItemChance && dropTable.legendaryItemDrops.Count > 0)
        {
            RollItemFromListOfDrops(dropTable.legendaryItemDrops);
        }
        else if (rolled < dropTable.rareItemChance && dropTable.rareItemDrops.Count > 0)
        {
            RollItemFromListOfDrops(dropTable.rareItemDrops);
        }
        else if (rolled < dropTable.normalItemChance && dropTable.normalItemDrops.Count > 0)
        {
            RollItemFromListOfDrops(dropTable.normalItemDrops);
        }

    }


    public void RollItemFromListOfDrops(List<Item> drops)
    {
        int itemRolled = Random.Range(0, drops.Count);
        ShowDropOnScreen(drops[itemRolled]);
        Item itemCopy = Instantiate(drops[itemRolled]);

        FightManager.instance.AddItemToLooted(itemCopy);

        AddDroppedItemToInv(itemCopy);

        RemoveItemFromDropTable(itemCopy);

    }

    private void RemoveItemFromDropTable(Item itemCopy)
    {
        if (currentTable.legendaryItemDrops.Count > 0)
            foreach (Item item in currentTable.legendaryItemDrops)
            {
                if(item.ID == itemCopy.ID)
                {
                    currentTable.legendaryItemDrops.Remove(item);
                }
            }
    }

    private void RollChanceForMaterial(DropTable dropTable)
    {
        int rolled = Random.Range(0, 100);

        if (rolled < dropTable.rareMaterialChance && dropTable.rareMaterialDrops.Count > 0)
        {
            RollMaterialFromListOFDrops(dropTable.rareMaterialDrops);
        }
        else if (rolled < dropTable.normalMaterialChance && dropTable.normalMaterialDrops.Count > 0)
        {
            RollMaterialFromListOFDrops(dropTable.normalMaterialDrops);
        }
    }

    private void RollMaterialFromListOFDrops(List<CraftingMaterial> drops)
    {
        int itemRolled = Random.Range(0, drops.Count);
        ShowDropOnScreen(drops[itemRolled]);

        CraftingMaterial droppedMat = drops[itemRolled];
        droppedMat.amount = Random.Range(1, 10);

        FightManager.instance.AddMaterialToLooted(droppedMat);

        AddDroppedMaterialToInv(droppedMat);

    }

    private void DropMoney(DropTable dropTable)
    {//TODO add drop money level balance


        int droppedValue = Mathf.RoundToInt(100 * dropTable.moneyRate);
        //PlayerManager.AddGold(droppedValue);
        goldValueEvent.UpdateValue(droppedValue).Raise();


        goldLooted.Value += droppedValue;
        //FightManager.instance.AddMoneyToLooted(droppedValue);
        //     UIManager.instance.UpdateGoldValue();

        int min = dropTable.moneyRate > 1 ? 10 : 0;
        int max = dropTable.moneyRate > 1 ? 15 : 5;

        int randomNumberOfCoins = Random.Range(min, max);
        for (int i = 0; i < randomNumberOfCoins; i++)
        {
            ShowDroppedMoneyOnScreen();
        }


    }


    public void ShowDroppedMoneyOnScreen()
    {
        InitializeDropRoot();
        GameObject drop = Instantiate(dropPrefab, dropRoot.transform);
        drop.GetComponent<Image>().sprite = goldIcon;
    }

    public void ShowDropOnScreen(Item item)
    {
        InitializeDropRoot();
        GameObject drop = Instantiate(dropPrefab, dropRoot.transform);
        drop.GetComponent<Image>().sprite = item.Icon;
    }

    public void AddDroppedItemToInv(Item item)
    {

        InventoryManager.instance.inventory.AddItem(item, true);
    }


    public void AddDroppedMaterialToInv(CraftingMaterial material)
    {
        materials.AddMaterial(material);
    }


}

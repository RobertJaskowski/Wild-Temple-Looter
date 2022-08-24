using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    #region instance
    public static FightManager instance;


    private void Instantiate()
    {
        if (instance != null)
            Debug.Log("FightManager is already instantianated");

        else
            instance = this;
    }

    #endregion


    public Equipment equipment;

    public Image background;
    public GameObject enemiesParent;
    public Image enemyImage;

    [ReadOnly]
    public Equipableitem currentWeapon;
    [ReadOnly]
    public Enemy currentEnemy;
    [ReadOnly]
    public Level currentLevel;

    private bool continueFighting = false;

    [Space(20)]
    public IntGameEvent updateLevelProgressEvent;
    public FloatGameEvent playerRuntimeCurrentHpUpdateEvent;
    public GameEvent hideDeathScreen;


    [Space(20)]
    public LevelDatabase levelDatabase;
    public GameObject lootedItemsParent;
    public Scrollbar LootedItemsScrollbar;
    public GameObject lootedItemSlotPrefab;
    public IntReference GoldLooted;
    public FloatReference difficulty;
    public List<Sprite> ItemsLooted { get; set; }
    public Dictionary<CraftingMaterial, int> MaterialsLooted { get; set; }

    [Space(20)]
    public GameObject endingScreenParent;
    public GameObject deathScreenObj;
    public TextMeshProUGUI goldLootedText;
    public GameObject returnToTownButton;


    [Space(20)]
    public bool InCombat = false;
    private int continued = 0;

    private void Awake()
    {
        Instantiate();

    }


    void Start()
    {
        AudioManager.instance.StopPlaying("TownBackgroundChill");

        continued = 0;
        InitiateFight();

        ClearAndInstantinateLootedList();
    }


    public void InitiateFight()
    {


        ShowReturnToTownButton();
        hideDeathScreen.Raise();
        InCombat = true;
        playerRuntimeCurrentHpUpdateEvent.UpdateValue((float)PlayerHpBarUpdateValue.Init);
        ManageContinued();

        instance.currentLevel = MapGeneratedLevelButton.ChosenLevel;
        currentWeapon = equipment.Weapon;

        instance.currentLevel.SetupLevel();
        instance.currentLevel.SpawnEnemy();
    }

    private void ManageContinued()
    {
        if (continued != 0)
        {
            IncreaseDifficulty();
        }
        else
        {
            ResetDifficulty();
        }
        continued++;
    }

    private void IncreaseDifficulty()
    {
        difficulty.Value *= 1.2f;
    }

    private void ResetDifficulty()
    {
        difficulty.Value = 1f;
    }

    private void ClearAndInstantinateLootedList()
    {
        GoldLooted.Value = 0;
        ItemsLooted = new List<Sprite>();
        MaterialsLooted = new Dictionary<CraftingMaterial, int>();
    }

    public void EnemyDied()
    {

        updateLevelProgressEvent.UpdateValueAndRaise(1);
        if (instance.currentLevel.HasAnotherEnemy())
        {
            //L.og("yes has another enemy");
            instance.currentLevel.SpawnEnemy();
        }
        else
        {
            // L.og("no  another enemy");

            //updateLevelProgressEvent.SetValue(0).Raise();

            if (continueFighting)
            {
                InitiateFight();
            }
            else
            {
                ShowEndingClearedScreen();

            }

        }


        //SaveLoad.OnSaveInitiated();
    }

    public void AddGoldRewardIfContractCompleted()
    {
        if (difficulty == 0)
        {
            GoldLooted.Value += currentLevel.levelReward;
        }
    }

    public void RemoveCompletedContract()
    {
        levelDatabase.RemoveLevel(currentLevel);
    }

    public void InitiateDropTableWithCurrentLevelTable()
    {
        DropController.instance.Initiate(currentLevel.dropTable);
    }

    //public void ModifyCurrentLevelDropTableToBossValuesOnBossSpawn()
    //{

    //}

    //public void ModifyCurrentLevelDropTAbleToNormalValuesOnBossDied()
    //{

    //}

    public void InitiateDropTableWithCurrentLevelAndBossValues()
    {
        DropController.instance.Initiate(currentLevel.dropTable, true);
    }

    public void ContinueFightingChanged(bool willContinue)
    {
        continueFighting = willContinue;
    }

    private void ShowEndingClearedScreen()
    {
        endingScreenParent.SetActive(true);
        goldLootedText.text = "Gold looted: " + GoldLooted.Value;
    }

    public void ContinueDungeon()
    {
        endingScreenParent.SetActive(false);
        InitiateFight();
    }

    public void ReturnToTown()
    {
        GameManager.instance.LoadTown();
    }

    public void ClickField()//todo add check if can attack because of death screen and end screen
    {
        if (InCombat)
        {
            currentEnemy.TakeDamage(currentWeapon);
            AudioManager.instance.Play("PlayerHit");

        }

    }

    public void AddItemToLooted(Item item)
    {
        ItemsLooted.Add(item.Icon);
        GameObject obj = Instantiate(lootedItemSlotPrefab, lootedItemsParent.transform);
        LootedItemSlot slot = obj.GetComponent<LootedItemSlot>();
        slot.image.sprite = item.Icon;
        MoveLootedScrollbarToNewest();
    }

    public void AddMaterialToLooted(CraftingMaterial material)
    {
        if (MaterialsLooted.ContainsKey(material))
        {
            int numberOfMaterial;
            MaterialsLooted.TryGetValue(material, out numberOfMaterial);
            MaterialsLooted.Remove(material);
            MaterialsLooted.Add(material, numberOfMaterial + material.amount);

            Transform obj = lootedItemsParent.transform.Find(material.name);
            LootedItemSlot slot = obj.GetComponent<LootedItemSlot>();
            int oldAmount = int.Parse(slot.AmountText.text);
            slot.AmountText.text = (oldAmount + material.amount).ToString();

        }
        else
        {
            MaterialsLooted.Add(material, material.amount);
            GameObject obj = Instantiate(lootedItemSlotPrefab, lootedItemsParent.transform);
            obj.name = material.name;
            LootedItemSlot slot = obj.GetComponent<LootedItemSlot>();
            slot.image.sprite = material.Icon;
            slot.AmountText.enabled = true;
            slot.AmountText.text = material.amount.ToString();
        }
        MoveLootedScrollbarToNewest();
    }

    public void MoveLootedScrollbarToNewest()
    {
        LootedItemsScrollbar.value = 1;
    }

    public void HideReturnToTownButton()
    {
        returnToTownButton.SetActive(false);
    }


    public void ShowReturnToTownButton()
    {
        returnToTownButton.SetActive(true);
    }


    //public void RefreshLootedUI()
    //{

    //    foreach (Item item in ItemsLooted)
    //    {
    //        if ()
    //    }


    //    foreach (Transform child in materialsContentParent)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    foreach (Material material in MaterialsLooted.Keys)
    //    {
    //        GameObject materialSlotObject = Instantiate(materialPrefab, materialsContentParent);
    //        MaterialSlot slot = materialSlotObject.GetComponent<MaterialSlot>();
    //        slot.image.sprite = material.Icon;
    //        slot.name.text = material.ItemName;
    //        slot.amount.text = MaterialsLooted[material].ToString();

    //    }

    //}
}

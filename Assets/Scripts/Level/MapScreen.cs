using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{

    public LevelDatabase levelDatabase;

    [Space(20)]
    public GameObject contractInfo;
    public GameObject levelNotChosenText;
    public GameObject playButton;
    public GameObject levelsRoot;

    [Space(20)]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI goldText;
    public GameObject materialsRoot;
    public GameObject elementsRoot;

    [Space(20)]
    public GameObject generatedLevelPrefab;
    public GameObject iconPlaceholderPrefab;
    public GameObject iconElementalPlaceholderPrefab;

    [SerializeField]
    [ReadOnly]
    private Level visibleLevel;
    private List<ElementalManager.ElementalType> elementsAdded;
    private List<Sprite> itemsAndMaterialsAdded;

    private void OnEnable()
    {
        RefreshBanners();
    }

    public void PopulateMapWithChosenLevel()
    {
        AudioManager.instance.Play("ContractSwitch");


        contractInfo.SetActive(true);
        levelNotChosenText.SetActive(false);
        playButton.SetActive(true);

        visibleLevel = MapGeneratedLevelButton.ChosenLevel;

        titleText.text = visibleLevel.Name;
        goldText.text = visibleLevel.levelReward.ToString();


        DestroyPossibleElementsAndMaterials();
        SetPossibleEnemyElementTypes();
        SetPossibleItemsAndMaterials();
    }

    private void DestroyPossibleElementsAndMaterials()
    {
        elementsRoot.transform.DestroyAllChildGameObjects();

        materialsRoot.transform.DestroyAllChildGameObjects();
    }

    public void PlayChosenLevel()
    {
        GameManager.instance.LoadStory(visibleLevel);

        AudioManager.instance.Play("ContractSign");
    }

    public void RefreshBanners()
    {



        levelDatabase.CheckAndFillIfMissingLevels();



        levelsRoot.transform.DestroyAllChildGameObjects();
        for (int i = 0; i < levelDatabase.listOfGeneratedLevels.Count; i++)
        {
            MapGeneratedLevelButton banner = Instantiate(generatedLevelPrefab, levelsRoot.transform).GetComponent<MapGeneratedLevelButton>();
            banner.level = levelDatabase.listOfGeneratedLevels[i];
            banner.bannerText.text = (i + 1).ToString();


        }
    }

    private void SetPossibleEnemyElementTypes()
    {
        elementsAdded = new List<ElementalManager.ElementalType>();
        foreach (Enemy enemy in visibleLevel.possibleEnemies)
        {
            if (!elementsAdded.Contains(enemy.ElementalType))
            {
                elementsAdded.Add(enemy.ElementalType);
                Image image = Instantiate(iconElementalPlaceholderPrefab, elementsRoot.transform).GetComponent<Image>();
                image.sprite = ElementalManager.GetElementalImage(enemy.ElementalType);
            }

        }
    }

    private void SetPossibleItemsAndMaterials()
    {
        itemsAndMaterialsAdded = new List<Sprite>();

        foreach (Enemy enemy in visibleLevel.possibleEnemies)
        {

            FillWithItems(visibleLevel.dropTable.legendaryItemDrops);
            FillWithItems(visibleLevel.dropTable.rareItemDrops);
            FillWithItems(visibleLevel.dropTable.normalItemDrops);

            FillWithMaterials(visibleLevel.dropTable.rareMaterialDrops);
            FillWithMaterials(visibleLevel.dropTable.normalMaterialDrops);

        }
    }

    private void FillWithMaterials(List<CraftingMaterial> materialsToMoveThrough)
    {
        foreach (CraftingMaterial material in materialsToMoveThrough)
        {
            if (!itemsAndMaterialsAdded.Contains(material.Icon))
            {
                itemsAndMaterialsAdded.Add(material.Icon);
                InitImagePrefab(material.Icon);
            }

        }
    }

    private void FillWithItems(List<Item> itemsToMoveThrough)
    {
        if (itemsToMoveThrough != null)
            if (itemsToMoveThrough.Count > 0)
                foreach (Item item in itemsToMoveThrough)
                {
                    if (item != null)
                        if (!itemsAndMaterialsAdded.Contains(item.Icon))
                        {
                            itemsAndMaterialsAdded.Add(item.Icon);
                            InitImagePrefab(item.Icon);
                        }

                }
    }


    private void InitImagePrefab(Sprite spriteToFillImage)
    {
        Image image = Instantiate(iconPlaceholderPrefab, materialsRoot.transform).GetComponent<Image>();
        image.sprite = spriteToFillImage;
    }

}

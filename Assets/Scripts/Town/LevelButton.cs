using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{

    public Level buttonLevel;
    public Image background;
    public TextMeshProUGUI title;



    [Space(20)]
    public GameObject imagePrefab;
    public GameObject elementsRoot;
    public GameObject materialsParent;

    public static Level chosenLevel;

    private List<ElementalManager.ElementalType> elementsAdded;
    private List<Sprite> itemsAndMaterialsAdded;

    public void InitiateOnScreen(Level level)
    {
        buttonLevel = level;

        SetBackground(level);
        SetTitle(level);
        SetPossibleEnemyElementTypes(level);
        SetPossibleItemsAndMaterials(level);

    }

    private void SetPossibleItemsAndMaterials(Level level)
    {
        itemsAndMaterialsAdded = new List<Sprite>();

        foreach (Enemy enemy in level.possibleEnemies)
        {
            DropTable table = enemy.dropTable;


            FillWithItems(table.legendaryItemDrops);
            FillWithItems(table.rareItemDrops);
            FillWithItems(table.normalItemDrops);

            FillWithMaterials(table.rareMaterialDrops);
            FillWithMaterials(table.normalMaterialDrops);

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
        Image image = Instantiate(imagePrefab, materialsParent.transform).GetComponent<Image>();
        image.sprite = spriteToFillImage;
    }

    private void SetPossibleEnemyElementTypes(Level level)
    {
        elementsAdded = new List<ElementalManager.ElementalType>();
        foreach (Enemy enemy in level.possibleEnemies)
        {
            if (!elementsAdded.Contains(enemy.ElementalType))
            {
                elementsAdded.Add(enemy.ElementalType);
                Image image = Instantiate(imagePrefab, elementsRoot.transform).GetComponent<Image>();
                image.sprite = ElementalManager.GetElementalImage(enemy.ElementalType);
            }

        }
    }

    private void SetTitle(Level level)
    {
        title.text = level.Name;
        gameObject.name = level.Name;
    }

    private void SetBackground(Level level)
    {
        background.sprite = level.background;
    }

    public void LoadLevel()
    {
        chosenLevel = buttonLevel;
        //FightManager.currentLevel = chosenLevel;
        GameManager.instance.LoadStory(buttonLevel);


    }
}

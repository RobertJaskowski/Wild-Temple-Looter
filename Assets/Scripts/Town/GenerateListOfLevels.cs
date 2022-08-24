using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateListOfLevels : MonoBehaviour
{

    public GameObject contentParent;
    public GameObject LevelButtonPrefab;
    public List<Level> levelsToGenerate;

    private void Awake()
    {
        InitiateLevels();
    }

    public void InitiateLevels()
    {
        foreach (Level level in levelsToGenerate)
        {
            LevelButton lb = Instantiate(LevelButtonPrefab, contentParent.transform).GetComponent<LevelButton>();
            lb.InitiateOnScreen(level);


        }
    }
}

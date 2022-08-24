using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Managers/LevelDatabase"))]
public class LevelDatabase : ScriptableObject
{
    [Space(20)]
    public List<Level> listOfGeneratedLevels;
    [Space(20)]
    public List<string> levelNamePossible;
    [Space(20)]
    public List<Sprite> levelBackgroundPossible;
    [Space(20)]
    public int MaxLevelsPossible;



    [Space(20)]
    public IntReference playerLevel;
    public IntGameEvent updateLevelProgress;
    public IntReference LevelProgress;
    public IntVariable MaxLevelProgress;
    public EnemyController enemyController;

    private void OnEnable()
    {
        listOfGeneratedLevels.Clear();
    }

    public void GenerateListOfLevels(bool clear = false)
    {
        if (clear)
        {
            listOfGeneratedLevels = new List<Level>();
            listOfGeneratedLevels.Clear();
        }

        int startingInt = listOfGeneratedLevels.Count > 0 ? listOfGeneratedLevels.Count  : 0;

        for (int i = startingInt; i < MaxLevelsPossible; i++)
        {
            Level level = GenerateLevel();
            listOfGeneratedLevels.Add(level);
        }
    }

    public Level GenerateLevel()
    {
        Level level = ScriptableObject.CreateInstance<Level>();
        level.InitVariables(updateLevelProgress, LevelProgress, MaxLevelProgress);
        level.Name = GetRandomLevelName();
        level.background = GetRandomLevelBackground();
        level.possibleEnemies = GeneratePossibleEnemies();
        level.levelReward = GenerateLevelReward();
        level.dropTable = ScriptableObject.CreateInstance<DropTable>().Generate();


        return level;
    }

    private int GenerateLevelReward() => playerLevel.Value * UnityEngine.Random.Range(300, 2500) * UnityEngine.Random.Range(1, 2);

    private List<Enemy> GeneratePossibleEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();

        for (int i = 0; i < 3; i++)
        {
            Enemy enemy = ScriptableObject.CreateInstance<Enemy>();
            enemy.GenerateEnemy(enemyController);
            enemies.Add(enemy);

        }

        return enemies;
    }

    private Sprite GetRandomLevelBackground() => levelBackgroundPossible[UnityEngine.Random.Range(0, levelBackgroundPossible.Count - 1)];

    internal void CheckAndFillIfMissingLevels()
    {
        if (listOfGeneratedLevels.Count < MaxLevelsPossible)
            GenerateListOfLevels();

        FillNullLevels();
    }

    private void FillNullLevels()
    {
        for (int i = 0; i < listOfGeneratedLevels.Count - 1; i++)
        {

            if (listOfGeneratedLevels[i] == null)
            {
                listOfGeneratedLevels[i] = GenerateLevel();
            }
        }
    }

    private string GetRandomLevelName() => levelNamePossible[UnityEngine.Random.Range(0, levelNamePossible.Count - 1)];

    public void RemoveLevel(Level levelToRemove)
    {

        if (listOfGeneratedLevels.Contains(levelToRemove))
        {
            listOfGeneratedLevels.Remove(levelToRemove);
        }
    }
}

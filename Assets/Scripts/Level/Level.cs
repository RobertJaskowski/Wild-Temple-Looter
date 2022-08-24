using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(menuName = "Level")]
public class Level : ScriptableObject
{
    public string Name;
    public int NumberOfEnemies { get; set; }
    public List<Enemy> possibleEnemies;
    public List<Enemy> Enemies { get; set; }
    public int levelReward;
    public DropTable dropTable;

    [Space(20)]

    public IntGameEvent updateLevelProgress;
    public IntReference LevelProgress;
    public IntVariable MaxLevelProgress;

    [Space(20)]
    public Sprite background;

#if UNITY_EDITOR
    public void OnValidate()
    {
        MaxLevelProgress = (IntVariable)SOTools.GetExistingSO<IntVariable>("MaxProgress");
        updateLevelProgress = (IntGameEvent)SOTools.GetExistingSO<IntGameEvent>("UpdateLevelProgress");
        if (Name != null && name != Name)
        {
            Name = name;
        }
    }
#endif

    public void InitVariables(IntGameEvent updateLevelProgress, IntReference LevelProgress, IntVariable MaxLevelProgress)
    {
        this.updateLevelProgress = updateLevelProgress;
        this.LevelProgress = LevelProgress;
        this.MaxLevelProgress = MaxLevelProgress;
    }

    public void SetupLevel()
    {
        FightManager.instance.background.sprite = background;
        LevelProgress = updateLevelProgress.Value;

        updateLevelProgress.SetValue(0);
        MaxLevelProgress.RuntimeValue = 5;
        updateLevelProgress.Raise();

    }

    //public void FillLevel(int numberOfEnemies)
    //{



    //    MaxLevelProgress.RuntimeValue = numberOfEnemies;
    //   // UIManager.instance.levelProgressBar.maxValue = MaxProgress;
    //   // UIManager.instance.levelProgressBar.value = StoryManager.instance.StoryInfo.LevelProgress;

    //    NumberOfEnemies = numberOfEnemies;
    //    AddEnemies();
    //}

    public void SpawnEnemy()
    {
        if (IsLastEnemy())
        {
            Enemy e = GetEnemyFromPossible();
            e.isBoss = true;
            e.Spawn();
        }
        else
        {

            GetEnemyFromPossible().Spawn();

            // Enemies[StoryManager.instance.StoryInfo.LevelProgress].Spawn();


        }

    }



    public bool IsLastEnemy()
    {
        //L.og(LevelProgress.Value + " progress and max  " + (MaxLevelProgress.RuntimeValue - 1));
        if (LevelProgress.Value == MaxLevelProgress.RuntimeValue - 1)
        {
            L.og("last enemy");
            return true;
        }
        return false;

    }

    public bool HasAnotherEnemy()
    {
        //L.og("Has another ?" + StoryManager.instance.StoryInfo.LevelProgress + " " + Enemies.Count);
        if (LevelProgress.Value < MaxLevelProgress.RuntimeValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Enemy GetEnemyFromPossible()
    {
        int randomInt = Random.Range(0, possibleEnemies.Count);

        Enemy e = possibleEnemies[randomInt];

        Enemy enemy = Instantiate(e);
        if (enemy.dropTable != null)
            enemy.dropTable = Instantiate(e.dropTable);
        return enemy;
    }

    //private void AddEnemies()
    //{
    //    Enemies = new List<Enemy>();

    //    for (int i = 0; i < NumberOfEnemies; i++)
    //    {
    //        int randomInt = Random.Range(0, possibleEnemies.Count);


    //        Enemy e = possibleEnemies[randomInt];

    //        Enemy enemy = ScriptableObject.CreateInstance<Enemy>();
    //        enemy.Image = e.Image;
    //        enemy.Name = e.Name;
    //        enemy.Hp = e.Hp;
    //        enemy.dropTable = Instantiate(e.dropTable);
    //        Enemies.Add(enemy);

    //    }
    //}

}

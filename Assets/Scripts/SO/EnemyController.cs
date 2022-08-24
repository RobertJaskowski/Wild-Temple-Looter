using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Controllers/EnemyController")]
public class EnemyController : ScriptableObject
{
    [Space(20)]
    [Range(0, 5)]
    public float attackTimeMin;
    [Range(0, 25)]
    public float attackTimeMax;
    [Range(0, 100)]
    public float attackTimeMultiplier;


    [Space(20)]
    [Range(5,50)]
    public float strenghtMin;
    [Range(0, 100)]
    public float strenghtMultiplier;

    [Space(20)]
    [Range(5, 1000)]
    public float enemyHpMin;
    [Range(0, 100)]
    public float enemyHpMultiplier;

    [Space(100)]
    public FloatVariable MaxEnemyHp;
    public FloatVariable difficulty;
    public Equipment equipment;
    public EnemyDatabase enemyDatabase;
    public FloatVariable playerHp;
    public IntVariable playerLevel;

    [Space(20)]
    public FloatGameEvent updateEnemyHpBarEvent;
    public FloatGameEvent playerRuntimeHpCurrentUpdateEvent;
    public DropTableGameEvent normalEnemyDied;
    public GameEvent hidePlayerHpBarEvent;
    public GameEvent showPlayerHpBarEvent;
    public GameEvent enemyHit;
    public GameEvent bossSpawned;
    public GameEvent bossDied;
    public GameEvent enemyAttack;
    public GameEvent hideDungeonReturnToTownButton;

#if UNITY_EDITOR
    public void OnValidate()
    {
        updateEnemyHpBarEvent = (FloatGameEvent)SOTools.GetExistingSO<FloatGameEvent>("UpdateEnemyHpBar");
        MaxEnemyHp = (FloatVariable)SOTools.GetExistingSO<FloatVariable>("MaxEnemyHp");
        difficulty = (FloatVariable)SOTools.GetExistingSO<FloatVariable>("EnemyDifficulty");
        showPlayerHpBarEvent = (GameEvent)SOTools.GetExistingSO<GameEvent>("ShowPlayerHpBar");
        hidePlayerHpBarEvent = (GameEvent)SOTools.GetExistingSO<GameEvent>("HidePlayerHpBar");
        playerRuntimeHpCurrentUpdateEvent = (FloatGameEvent)SOTools.GetExistingSO<FloatGameEvent>("PlayerRuntimeCurrentHpUpdate");
        //equipment = (Equipment)SOTools.GetExistingSO<Equipment>("Equipment");
        //enemyDatabase = (EnemyDatabase)SOTools.GetExistingSO<EnemyDatabase>("EnemyDatabase");
        //playerHp = (FloatVariable)SOTools.GetExistingSO<FloatVariable>("PlayerRuntimeMaxHp");
        //playerLevel = (FloatVariable)SOTools.GetExistingSO<FloatVariable>("PlayerLevel");
    }
#endif
    public float GetEnemyGeneratedHp()
    {
        return playerLevel.RuntimeValue * enemyHpMin * (enemyHpMultiplier / 100);
    }

    public float GetEnemyGeneratedStrenght()
    {
        float str = strenghtMin * playerLevel.RuntimeValue * (strenghtMultiplier / 100);
        return str > strenghtMin ? str : strenghtMin;
    }

    public float GetEnemyGeneratedAttackTime()
    {
        float att = attackTimeMax - (attackTimeMax*(playerLevel.RuntimeValue / 100));
        float attMultiplied = att - (att * (attackTimeMultiplier / 100));

        return attMultiplied > attackTimeMin ? attMultiplied : attackTimeMin;
    }

    public ElementalManager.ElementalType GetEnemyGeneratedElementalType()
    {
        return ElementalManager.GetRandomElementalType();
    }



}

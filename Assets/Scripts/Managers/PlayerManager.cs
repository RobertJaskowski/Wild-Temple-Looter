using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Managers/PlayerManager")]
public class PlayerManager : ScriptableObject
{

    [Space(20)]
    public IntReference Level;
    public IntGameEvent updatePlayerLevel;

    [Space(20)]
    public IntReference LevelProgress;
    public IntGameEvent updatePlayerLevelProgress;

    [Space(20)]
    public FloatReference PlayerHp;
    public FloatGameEvent updatePlayerHp;

    [Space(20)]
    public IntReference Gold;
    public IntGameEvent updateGoldEvent;

    [Space(20)]
    public IntReference Gems;
    public IntGameEvent updatePlayerGems;

    private void OnEnable()
    {

        SaveLoad.SaveInitiated += Save;
        SaveLoad.LoadInitiated += Load;
    }



    public void Save()
    {

        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData.Level = Level.Value;
        playerSaveData.LevelProgress = LevelProgress.Value;
        playerSaveData.Hp = PlayerHp.Value;
        playerSaveData.Gold = Gold.Value;
        playerSaveData.Gems = Gems.Value;
        SaveLoad.Save(playerSaveData, "Player");

    }

    public void Load()
    {
        if (SaveLoad.SaveExists("Player"))
        {
            PlayerSaveData playerSaveData = SaveLoad.Load<PlayerSaveData>("Player");
            updatePlayerLevel.SetValue(playerSaveData.Level).Raise();
            updatePlayerLevelProgress.SetValue(playerSaveData.LevelProgress).Raise();
            updatePlayerHp.SetValue(playerSaveData.Hp).Raise();
            updateGoldEvent.SetValue(playerSaveData.Gold).Raise();
            updatePlayerGems.SetValue(playerSaveData.Gems).Raise();



            //Level.Value = playerSaveData.Level;
            //PlayerHp.Value = playerSaveData.Hp;
            //Gold.Value = playerSaveData.Gold;
            //Gems.Value = playerSaveData.Gems;

        }
        else
        {
            updatePlayerLevel.SetValue(1).Raise();
            updatePlayerLevelProgress.SetValue(10).Raise();
            updatePlayerHp.SetValue(100).Raise();
            updateGoldEvent.SetValue(100).Raise();
            updatePlayerGems.SetValue(10).Raise();
            //Level.Value = 1;
            //PlayerHp.Value = 100;
            //Gold.Value = 100;
            //Gems.Value = 10; 
        }
    }

}
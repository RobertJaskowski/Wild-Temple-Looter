using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreen : MonoBehaviour
{
    public TextMeshProUGUI level;
    public TextMeshProUGUI levelProgressToUpgradeText;
    public Image levelProgressBar;

    [Space(20)]
    public IntGameEvent playerUpdateGoldEvent;
    public IntGameEvent playerUpdateLevelEvent;

    [Space(20)]
    public IntReference PlayerGold;
    public IntReference PlayerLevel;
    public IntReference PlayerLevelProgress;

    private bool isUpdatingProgressBar = false;

    

    public void RefreshAllState()
    {
        RefreshLevel();
        RefreshProgressBar();
        RefreshValueCostOfUpgradeText();
    }

    private void OnEnable()
    {
        RefreshAllState();
    }


    private void Update()
    {
        if (isUpdatingProgressBar)
        {
            UpgradeLevelTick();
            NextLevelCheck();
            RefreshAllState();
        }
    }

    private void NextLevelCheck()
    {
        if (PlayerLevelProgress.Value >= GetMaxLevelProgress())
        {
            
            playerUpdateLevelEvent.UpdateValueAndRaise(1);
            PlayerLevelProgress.Value = 0;

            AudioManager.instance.Play("PlayerLevelUp");
        }
    }

    private void UpgradeLevelTick()
    {
        if (HaveEnoughGold())
        {
            PlayerLevelProgress.Value += GetUpgradeTick();
            playerUpdateGoldEvent.UpdateValueAndRaise(-GetUpgradeTick());
        }//todo not enough gold feedback
    }

    private bool HaveEnoughGold()
    {
        if (PlayerGold.Value > GetUpgradeTick())
            return true;
        else
            return false;
    }

    public void UpgradeButtonPressed()
    {
        isUpdatingProgressBar = true;
    }

    public void UpgradeButtonReleased()
    {
        isUpdatingProgressBar = false;
    }

    private void RefreshValueCostOfUpgradeText()
    {
        levelProgressToUpgradeText.text = (GetMaxLevelProgress() - PlayerLevelProgress.Value).ToString();
    }

    private int GetUpgradeTick()
    {
        return GetMaxLevelProgress() / 25;
    }

    private int GetMaxLevelProgress()
    {
        return PlayerLevel.Value * 500;
    }

    private void RefreshProgressBar()
    {
        if (PlayerLevelProgress.Value != 0)
        {
            levelProgressBar.fillAmount = (float)PlayerLevelProgress.Value / (float)GetMaxLevelProgress();
        }
        else
            levelProgressBar.fillAmount = 0;
    }

    public void RefreshLevel()
    {
        level.text = PlayerLevel.Value.ToString();

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Monetization;

public class DeathScreen : MonoBehaviour
{

    public GameObject deathScreenObj;
    public TextMeshProUGUI goldLootedText;

    public IntReference goldLooted;
    public IntReference playerGold;
    public IntGameEvent playerGoldUpdateEvent;
    public FloatGameEvent playerRuntimeCurrentHpUpdate;

    [Space(20)]
    public GameEvent showDungeonReturnToTownButton;
    

    

    public void Init()
    {
        goldLootedText.text = "Gold looted: " + goldLooted.Value;

    }


    public void ShowAd(string button)
    {
        StartCoroutine(WaitForAd(button));
    }

    IEnumerator WaitForAd(string button)
    {
        while (!Monetization.IsReady(AdController.instance.rewardedId))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(AdController.instance.rewardedId) as ShowAdPlacementContent;

        if (ad != null)
        {
            switch (button)
            {
                case "100":
                    ad.Show(AdFinished100);
                    break;
                case "continue":
                    ad.Show(AdFinishedContinue);
                    break;
            }
            
        }
    }

    void AdFinished100(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {

            BackToTownAndAd();

        }
    }

    void AdFinishedContinue(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            ContinueAndAd();
        }
    }

    public void BackToTownAndAd()
    {
        showDungeonReturnToTownButton.Raise();

        GameManager.instance.LoadTown();
    }

    public void BackToTown50()
    {
        showDungeonReturnToTownButton.Raise();

        playerGoldUpdateEvent.UpdateValue(-(goldLooted.Value / 2)).Raise();
        GameManager.instance.LoadTown();
    }

    public void ContinueAndAd()
    {
        
        playerRuntimeCurrentHpUpdate.SetValue((float)PlayerHpBarUpdateValue.Init).Raise();
        FightManager.instance.InCombat = true;

    }

    

    public void HideScreen()
    {
        deathScreenObj.SetActive(false);
    }
}

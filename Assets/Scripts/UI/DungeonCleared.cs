using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class DungeonCleared : MonoBehaviour
{

    public IntReference playerGold;
    public IntReference goldLooted;


    public void TriggerEndingChest()
    {
        StartCoroutine(WaitForAd());
    }

    IEnumerator WaitForAd()
    {
        while (!Monetization.IsReady(AdController.instance.rewardedEndingCleared))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(AdController.instance.rewardedEndingCleared) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinishedChestEndingCleared);

        }
    }


    void AdFinishedChestEndingCleared(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            playerGold.Value += goldLooted.Value;
            GameManager.instance.LoadTown();
        }
    }
}

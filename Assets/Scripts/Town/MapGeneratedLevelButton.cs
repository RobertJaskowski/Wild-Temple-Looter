using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGeneratedLevelButton : MonoBehaviour
{

    public TextMeshProUGUI bannerText;
    [ReadOnly]
    public Level level;
    public GameEvent mapBannerLevelWasChosen;

    public static Level ChosenLevel;

    public void PopulateMapAsCurrent()
    {
        ChosenLevel = level;
        mapBannerLevelWasChosen.Raise();
    }
}

using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    #region instance

    public static AdController instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("AdController already exists");
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

    }

    #endregion


    public string rewardedId = "rewardedVideo";
    public string rewardedEndingCleared = "rewardedEndingCleared";
    private string gameId = "3282680";
    public bool testMode = true;

    private void Awake()
    {
        Instantiate();
    }

    void Start()
    {
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, testMode);
        }
    }

    public string GetGameId()
    {
        return gameId;
    }

    public bool GetTestMode()
    {
        return testMode;
    }
}

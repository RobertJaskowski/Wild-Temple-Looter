using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerManager playerManager;

    public void SavePlayerData()
    {
        playerManager.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    #region instance

    public static TownManager instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("TownManager already exists");
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

    }

    #endregion

    public GameObject map;


    private void Awake()
    {
        Instantiate();
    }
    
    public void ShowMap()
    {
        map.SetActive(true);
    }

    public void CloseMap()
    {
        map.SetActive(false);
    }


}

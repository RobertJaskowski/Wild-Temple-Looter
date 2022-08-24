using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region instance

    public static GameManager instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("GameManager already exists");
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

    }

    #endregion



    private void Awake()
    {
        Instantiate();
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        L.og("LoadInnitiated gamemanger");
        SaveLoad.OnLoadInitiated();
        AudioManager.instance.Play("TownBackgroundChill");

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ShowLoadingScreen()
    {
        SceneManager.LoadScene("Loading");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // L.og("onsceneloaded");
        //SceneManager.UnloadSceneAsync("Loading");
        //if (scene.name == "Dungeon")
        //{
        //    FightManager.instance.InitiateFight();
        //}
    }

    public void LoadTown()
    {
        SceneManager.LoadSceneAsync("Town", LoadSceneMode.Single).allowSceneActivation = true;
        AudioManager.instance.Play("TownBackgroundChill");

    }


    public void LoadStory(Level level)
    {
        ShowLoadingScreen();
        SceneManager.LoadSceneAsync("Dungeon", LoadSceneMode.Single).allowSceneActivation = true;
        //SceneManager.LoadSceneAsync("Dungeon", LoadSceneMode.Additive).allowSceneActivation = true;


    }

    public void Save()
    {
        //redundant in my game
        SaveLoad.OnSaveInitiated();
    }

}

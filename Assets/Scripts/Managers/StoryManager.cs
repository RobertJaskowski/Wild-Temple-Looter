using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoryManager : MonoBehaviour
{
    #region instance

    public static StoryManager instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("StoryManager already exists");
        }
        else
        {
            instance = this;

        }

    }

    #endregion


    public List<Level> story;

    public StoryInfo StoryInfo { get; set; }

    private void Awake()
    {

        Instantiate();
        //SaveLoad.SaveInitiated += Save;
        SaveLoad.LoadInitiated += Load;
    }

    public void IncrementProgress()
    {

    }


    void Save()
    {
        
        //SaveLoad.Save<StoryInfo>(StoryInfo, "StoryInfo");
    }


    public void Load()
    {

        if (SaveLoad.SaveExists("StoryInfo"))
        {
            //Debug.Log("StoryInfo exist");
            //StoryInfo = SaveLoad.Load<StoryInfo>("StoryInfo");
        }
        else
        {
            StoryInfo = new StoryInfo
            {
                LevelProgress = 0,
                StoryProgress = 0
            };
            //L.og("StoryInfo doesnt exist " + StoryInfo.LevelProgress);
        }
    }


}


public class StoryInfo
{
    public int StoryProgress;
    public int LevelProgress;
}

using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class SaveLoad : MonoBehaviour
{
    public static string INVENTORY = "Inventory" ;
    public static string EQUIPEMENT = "Equipement" ;
    public static string PLAYER = "Player" ;
    public static string MATERIALS = "Materials" ;

    public static GameFile GAMEFILE = new GameFile();

    public static double SAVE_VERSION = 1.0;

    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";


    public static System.Action SaveInitiated;
    public static System.Action LoadInitiated;


    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke();
        // Save("GameFile");
    }

    public static void OnLoadInitiated()
    {
        //if (SaveExists("GameFile"))
        //    Load("GameFile");
        LoadInitiated?.Invoke();
    }


    //public static void Save(string gameFileName)
    //{
    //    Directory.CreateDirectory(SAVE_FOLDER);

    //    GameFile gameFile = new GameFile();

    //    string json = JsonUtility.ToJson(GAMEFILE);
    //    File.WriteAllText(SAVE_FOLDER + gameFileName + ".txt", json);

    //    L.og("saved " + json);

    //    //BinaryFormatter formatter = new BinaryFormatter();
    //    //using (FileStream fileStream = new FileStream(SAVE_FOLDER + key + ".txt", FileMode.Create))
    //    //{
    //    //    formatter.Serialize(fileStream, objectToSave);
    //    //}

    //}

    //public static void Save<T>(T objectToSave, string key)
    //{
    //    Directory.CreateDirectory(SAVE_FOLDER);
    //    //string json = JsonUtility.ToJson(objectToSave);
    //    string json = JsonConvert.SerializeObject(objectToSave);
    //    File.WriteAllText(SAVE_FOLDER + key + ".txt", json);

    //    L.og("saved " + json);

    //    //BinaryFormatter formatter = new BinaryFormatter();
    //    //using (FileStream fileStream = new FileStream(SAVE_FOLDER + key + ".txt", FileMode.Create))
    //    //{
    //    //    formatter.Serialize(fileStream, objectToSave);
    //    //}

    //}

    public static void Save<T>(T objectToSave, string key)
    {


        Directory.CreateDirectory(SAVE_FOLDER);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SAVE_FOLDER + key + ".txt");
        bf.Serialize(file, objectToSave);
        file.Close();

    }

    public static T Load<T>(string key)
    {

        T returnValue = default(T);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(SAVE_FOLDER + key + ".txt", FileMode.Open);
        returnValue = (T)bf.Deserialize(file);
        file.Close();

        return returnValue;
    }
    //current
    /*
    public static void Save<T>(T objectToSave, string key)
    {


        Directory.CreateDirectory(SAVE_FOLDER);
        string json = JsonUtility.ToJson(objectToSave);
        File.WriteAllText(SAVE_FOLDER + key + ".txt", json);

        L.og("saved " + json);


    }

    public static T Load<T>(string key)
    {

        T returnValue = default(T);

        string textRead = File.ReadAllText(SAVE_FOLDER + key + ".txt");
        L.og(textRead);
        returnValue = JsonUtility.FromJson<T>(textRead);
        return returnValue;
    }

    */
    //    public static void Load(string key)
    //{
    //    //BinaryFormatter formatter = new BinaryFormatter();
    //    //T returnValue = default(T);
    //    //using (FileStream fileStream = new FileStream(SAVE_FOLDER + key + ".txt", FileMode.Open))
    //    //{
    //    //    returnValue = (T)formatter.Deserialize(fileStream);
    //    //}
    //    string textRead = File.ReadAllText(SAVE_FOLDER + key + ".txt");
    //    L.og(textRead);
    //    //GAMEFILE = JsonConvert.DeserializeObject<GameFile>(textRead);
    //    GAMEFILE = JsonUtility.FromJson<GameFile>(textRead);
    //    L.og("loaded " + GAMEFILE);

    //}

    //public static T Load<T>(string key)
    //{
    //    //BinaryFormatter formatter = new BinaryFormatter();
    //    //T returnValue = default(T);
    //    T returnValue = new T();
    //    //using (FileStream fileStream = new FileStream(SAVE_FOLDER + key + ".txt", FileMode.Open))
    //    //{
    //    //    returnValue = (T)formatter.Deserialize(fileStream);
    //    //}

    //    string textRead = File.ReadAllText(SAVE_FOLDER + key + ".txt");
    //    L.og(textRead);
    //    //returnValue; 
    //    JsonConvert.PopulateObject(textRead, returnValue);
    //    L.og("loaded " + returnValue);

    //    return returnValue;

    //}

    public static bool SaveExists(string key)
    {
        string path = SAVE_FOLDER + key + ".txt";
        return File.Exists(path);
    }

    public static void DeleteAllSaveFiles()
    {
        DirectoryInfo directory = new DirectoryInfo(SAVE_FOLDER);
        directory.Delete();
        Directory.CreateDirectory(SAVE_FOLDER);
    }



}

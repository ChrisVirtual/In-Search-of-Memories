using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

// This class is a Singleton class meaning we only want one of them in the scene. 
// It's like saving default coins or powerups for any 'New Game' created as a Singleton class and it ensures that all new users have same initial amount of coins and powerups.
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance {  get; private set; } // get instance publicly, but modify instance privately

    private void Awake()
    {
        //if (instance == null)
        //{
            //Debug.LogError("Found more than one Data Persistence Manager in the scene."); // as there should only be one singleton class in the scene
        //}
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load saved data from file using data handler
        this.gameData = dataHandler.Load();

        // if no data to load, then create new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }
        //TODO - push loaded data to all other scripts where necessary.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // Save that data to file using data handler.
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

}

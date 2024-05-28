using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = Application.persistentDataPath;
    private string dataFileName = "";

    public  FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName); //Path.Combine is used so its suitable for any OS
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // Load serialized data from the file.
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserializing data from JSON file back to C# object.
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            } 
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName); //Path.Combine is used so its suitable for any OS
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); // Creating directory if the directory doesn't exist already.

            // Serializing the game data object into a JSON format.
            string DataToStore = JsonUtility.ToJson(data, true);

            // Writing serialized data to the JSON file.
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(DataToStore);
                }
            }
        } 
        catch (Exception e) 
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // Loops over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dir_Info = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dir_Info)
        {
            string profileId = dirInfo.Name;

            //check if the data file actually exists. If it doesn't then this folder isnt a profile and should be skipped.
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it doesn't contain data: " + profileId);
                continue;
            }

            // Load the game data for this profile and put it in directory
            GameData profileData = Load();

            //ensuring the profile data isnt actually null
            if(profileData == null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }

        }

        return profileDictionary;
    }
}

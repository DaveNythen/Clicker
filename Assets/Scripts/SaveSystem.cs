using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

    private static string path = Application.persistentDataPath + "/gameData.buh";

    public static void SaveData(PlayerStats playerStats, InventorySaveData inventorySave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = File.Create(path);

        GameData data = new GameData(playerStats, inventorySave);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        if (SaveFileExists())
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = File.Open(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }

    public static void DeleteSaveFile()
    {
        if (SaveFileExists())
        {
            File.Delete(path);
        }
    }

    public static bool SaveFileExists()
    {
        return File.Exists(path);
    }
}

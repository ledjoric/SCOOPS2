using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void savePlayer(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.scps";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gameData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.scps";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

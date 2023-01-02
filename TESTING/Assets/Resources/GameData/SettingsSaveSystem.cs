using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingsSaveSystem : MonoBehaviour
{
    public static void saveSettings(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/settings.scps";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsSaveData data = new SettingsSaveData(gameData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsSaveData loadSettings()
    {
        string path = Application.persistentDataPath + "/settings.scps";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsSaveData data = formatter.Deserialize(stream) as SettingsSaveData;
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

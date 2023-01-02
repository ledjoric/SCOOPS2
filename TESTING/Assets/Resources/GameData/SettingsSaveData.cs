[System.Serializable]
public class SettingsSaveData
{
    // SETTINGS
    public float music;
    public float sfx;
    public int quality;

    public SettingsSaveData(GameData gameData)
    {
        music = gameData.music;
        sfx = gameData.sfx;
        quality = gameData.quality;
    }
}

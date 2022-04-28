using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance { get; private set; }

    private const float defaultVolume = 0.5f;
    public float sfxVolume;
    public float musicVolume;
    public float ambiVolume;
    public float levelsCompleted;

    public static bool isLoaded;

    [System.Serializable]
    class Settings
    {
        public float sfxVolume;
        public float musicVolume;
        public float ambiVolume;
    }

    [System.Serializable]
    class SaveData
    {
        public int levelsCompleted;
    }

    void Awake()
    {
        isLoaded = false;
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        LoadProgress();
        LoadSettings();
        isLoaded = true;
    }

    static public IEnumerator WaitForData()
    {
        while (!DataSaver.isLoaded)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void SaveSettings()
    {        
        // load slider values into Settings structure
        UIManager.instance.SaveVolumes();

        Settings settings = new Settings
        {
            ambiVolume = this.ambiVolume,
            sfxVolume = this.sfxVolume,
            musicVolume = this.musicVolume
        };

        string json = JsonUtility.ToJson(settings);
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);

    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Settings settings = JsonUtility.FromJson<Settings>(json);

            ambiVolume = settings.ambiVolume;
            musicVolume = settings.musicVolume;
            sfxVolume = settings.sfxVolume;
        }
        else
        {
            LoadDefault();
        }
    }

    private void LoadDefault()
    {
        ambiVolume = musicVolume = sfxVolume = defaultVolume;
    }

    public void SaveProgress()
    {
        // todo: save progress data

    }

    public void LoadProgress()
    {
        // todo: load progress data

        /* string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
        else
        {
            // prog = 0
         }*/
    }
}

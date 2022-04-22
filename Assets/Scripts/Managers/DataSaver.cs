using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        LoadData();
    }

    public void SaveSettings()
    {
        // todo: save settings
        /*string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); */

    }

    public void LoadSettings()
    {
        // todo: load settings

    }

    public void LoadData()
    {
        // todo: load progress data

        /* string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        } */

        LoadSettings();
    }
}

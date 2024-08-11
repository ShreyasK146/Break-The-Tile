
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class SaveSettings : MonoBehaviour
{
    public static SaveSettings instance;

    //public float tiltSens;
    //public float buttonSens;
    public float gameVolume;
    public float musicVolume;
    //public bool buttonSensEnabled;
    public int[] progress = new int[30];
    public int[] highscores = new int[30];
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
        SaveData data = new SaveData();
        data.LoadValues();
    }

    [System.Serializable]
    public class SaveData
    {
        //public float tiltSens;
        //public float buttonSens;
        public float gameVolume;
        public float musicVolume;
        public bool enabledButtonSens = true;
        public int[] progress = new int[30];
        public int[] highscores = new int[30];
        public void SaveValues()
        {
            
            //this.tiltSens = instance.tiltSens;
            //this.buttonSens = instance.buttonSens;
            this.gameVolume = instance.gameVolume;
            this.musicVolume = instance.musicVolume;
            //this.enabledButtonSens = instance.buttonSensEnabled;
            this.progress = instance.progress;
            this.highscores = instance.highscores;
            string json = JsonUtility.ToJson(this);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        public void LoadValues()    
        {
            string path = Application.persistentDataPath + "/savefile.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                //instance.tiltSens = data.tiltSens;
                //instance.buttonSens = data.buttonSens;
                instance.gameVolume = data.gameVolume;
                instance.musicVolume = data.musicVolume;
                //instance.buttonSensEnabled = data.enabledButtonSens;
                instance.progress = data.progress;
                instance.highscores = data.highscores;  
            }
        }
    }
}
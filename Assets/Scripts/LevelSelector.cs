
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector Instance { get; private set; }

    public bool randomLevel = false;

    public int RandomBallColor;

    public LevelData levelData { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }// to keep one levelmanager accross scenes

    }


    public void SetLevelData(LevelData levelData)
    {
        SaveSettings.SaveData s = new SaveSettings.SaveData();
        s.SaveValues();//making sure settings are saved.
        this.levelData = levelData;
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

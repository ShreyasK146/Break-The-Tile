
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
   
    public void LoadLevel()
    {
        SaveSettings.SaveData s = new SaveSettings.SaveData();
        s.SaveValues();
        LevelSelector.Instance.randomLevel = false;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
            
    }


    public void SetRandomLevelTrue() 
    {
        LevelSelector.Instance.randomLevel = true;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        SaveSettings.SaveData s = new SaveSettings.SaveData();
        s.SaveValues();
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        
#endif
    }
}

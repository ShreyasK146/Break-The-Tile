
using UnityEngine;
using UnityEngine.UI;

public class LevelScores : MonoBehaviour
{
    [SerializeField] GridLayoutGroup grid;
    string hexColor = "#FFE300";//yellow
    Color color;
    
    private void Start()
    {

        for (int i = 0; i < grid.transform.childCount; i++) 
        {
                
                for (int j = 0; j < SaveSettings.instance.progress[i]; j++)
                {
                    
                    if (ColorUtility.TryParseHtmlString(hexColor, out color))
                        grid.transform.GetChild(i).gameObject.GetComponent<ButtonScript>().images[j].GetComponent<Image>().color = color;
                }
        }

    }

    

}

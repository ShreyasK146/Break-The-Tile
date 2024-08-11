
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] public GameObject[] images = new GameObject[3];

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnButtonPress);    
    }

    public void OnButtonPress()
    {
        LevelSelector.Instance.SetLevelData(levelData);
    }



}

using UnityEngine.UI;
using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    [SerializeField] GameObject launchButton;

    private void Start()
    {
        launchButton.GetComponent<Button>().onClick.AddListener(SetBallToLaunch);
    }
    private void SetBallToLaunch()
    {
        GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunchable = true;
        launchButton.gameObject.SetActive(false);
    }
}

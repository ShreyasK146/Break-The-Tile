using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   

public class Game_Manager : MonoBehaviour
{
    [HideInInspector] public bool ballLaunched = false;
    [HideInInspector] public float horizontalInput = 0;
    LevelData levelData;
    [SerializeField] List<GameObject> tiles;
    [SerializeField] List<GameObject> ballImages;
    [SerializeField] GameObject ball;
    [SerializeField] PlayerPadController player;
    [SerializeField] GameObject scorePage;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] List<GameObject> stars;
    [SerializeField] List<Sprite> tileColors;
    [SerializeField] Sprite brickSprite;
    [SerializeField] Sprite metalSprite;
    [SerializeField] public List<Sprite> ballColors;
    [SerializeField] public List<GameObject> lifeballs;
    [SerializeField] GameObject launchButton;
    [HideInInspector] public bool ballLaunchable = false;

    string hexColor = "#FFE300";
    Color color;
    public int score;
    public int lives = 3;
    int count = 0;
    int random = -1;
    public bool randomMap = false;


    private void Awake()
    {
        LevelSelector.Instance.RandomBallColor = Random.Range(0, 6);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        GameObject ballGameObject = Instantiate(ball, player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        ballGameObject.GetComponent<SpriteRenderer>().sprite = ballColors[LevelSelector.Instance.RandomBallColor];
        levelData = LevelSelector.Instance.levelData;


        if(levelData != null && !LevelSelector.Instance.randomLevel)
        {
            int onesCount = levelData.tileData.Count(c => c == '1');
            int tileColorDistributionCount = onesCount / Random.Range(1, 9);//to avoid generating to totally random colors 
            int currentColorIndex = Random.Range(0, tileColors.Count);
            int currentColorCount = 0;
            foreach (char c in levelData.tileData)
            {
                if(c == '3')
                {
                    tiles[count].gameObject.tag = "metaltile";
                    tiles[count].gameObject.SetActive(true);
                    tiles[count].gameObject.GetComponent<SpriteRenderer>().sprite = metalSprite;

                }
                else if (c == '2')
                {
                    tiles[count].gameObject.tag = "strongtile";
                    tiles[count].gameObject.SetActive(true);
                    tiles[count].gameObject.GetComponent<SpriteRenderer>().sprite = brickSprite;
                }
                else if (c == '0')
                {
                    tiles[count].gameObject.SetActive(false);
                }
                else
                {
                    
                    if(currentColorCount >= tileColorDistributionCount)
                    {
                        currentColorIndex = Random.Range(0, tileColors.Count);
                        currentColorCount = 0;
                    }

                    Debug.Log(tiles[count]);
                   
                    tiles[count].gameObject.GetComponent<Renderer>().GetComponent<SpriteRenderer>().sprite = tileColors[currentColorIndex];
                    
                    currentColorCount++;    
                   
                     
                }
                
                count++;
            }
        }
        else
        {
            while(count != 273)
            {
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    tiles[count].gameObject.SetActive(false);
                }
                
                else if (random == 2)
                {
                    tiles[count].gameObject.tag = "strongtile";
                    tiles[count].gameObject.SetActive(true);
                    tiles[count].gameObject.GetComponent<SpriteRenderer>().sprite = brickSprite;
                }
                else
                {

                    tiles[count].gameObject.GetComponent<Renderer>().GetComponent<SpriteRenderer>().sprite = tileColors[Random.Range(0,tileColors.Count)];

                }
                count++;
            }
        }

        foreach(GameObject g in lifeballs)
        {
            g.GetComponent<Image>().sprite = ballColors[LevelSelector.Instance.RandomBallColor];
        }
    
    }

    private void Update()
    {

        if (lives == 0 || (GameObject.FindGameObjectsWithTag("tile").Length == 0 && GameObject.FindGameObjectsWithTag("strongtile").Length == 0))
            StartCoroutine(CheckProgress());
       
        CheckForBall();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void CheckForBall()
    {
        if (GameObject.FindGameObjectWithTag("ball") == null && lives > 0)
        {
            ballImages[ballImages.Count - 1].gameObject.SetActive(false);
            ballImages.RemoveAt(ballImages.Count - 1);
            lives--;
            if (lives != 0)
            {
                GameObject ballGameObject1 = Instantiate(ball, player.transform.position, Quaternion.identity);
                ballGameObject1.GetComponent<SpriteRenderer>().sprite = ballColors[LevelSelector.Instance.RandomBallColor];
                ballLaunched = false;
                launchButton.gameObject.SetActive(true);
                player.ball = ball;
            }

        }
        
    }
    
    IEnumerator CheckProgress()
    {
        yield return null;

        if (levelData == null)//for random level
        {
            scorePage.gameObject.SetActive(true);
            for (int i = 0; i < lives; i++)
            {
                if (ColorUtility.TryParseHtmlString(hexColor, out color))
                {
                    stars[i].GetComponent<Image>().color = color;
                }
            }
            scoreText.text = score.ToString();
            resultText.text = lives == 0 ? "Game Over" : "Good Job";
            Time.timeScale = 0f;
        }
        else
        {

            if (SaveSettings.instance.progress[levelData.id] < lives)
            {
                Debug.Log(lives);
                SaveSettings.instance.progress[levelData.id] = lives;
            }

            SaveSettings.SaveData s = new SaveSettings.SaveData();
            s.SaveValues();

            scorePage.gameObject.SetActive(true);
            for (int i = 0; i < lives; i++)
            {
                if (ColorUtility.TryParseHtmlString(hexColor, out color))
                {
                    stars[i].GetComponent<Image>().color = color;
                }
            }
          
          
            scoreText.text = score.ToString();
            if (score > SaveSettings.instance.highscores[levelData.id])
            {
                highScoreText.text = score.ToString();
                SaveSettings.instance.highscores[levelData.id] = score;
            }
            else
            {
                highScoreText.text = SaveSettings.instance.highscores[levelData.id].ToString();
            }


            resultText.text = lives == 0 ? "Game Over" : "Good Job";
            SaveSettings.SaveData s1 = new SaveSettings.SaveData();
            s1.SaveValues();
            Time.timeScale = 0f;
        }

        
       
    }

    public void SetBallToLaunch()
    {

        launchButton.gameObject.SetActive(false);
        StartCoroutine(Wait1Sec());
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(0.24f);
        ballLaunchable = true;
    }
}

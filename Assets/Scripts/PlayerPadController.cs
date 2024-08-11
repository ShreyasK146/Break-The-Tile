using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class PlayerPadController : MonoBehaviour
{

    //[SerializeField] Button leftButton;
    //[SerializeField] Button rightButton;
    [SerializeField] float endX = 9.5f;
    [SerializeField] public GameObject ball;
    [SerializeField] AudioSource powerupCollect;
    [SerializeField] UISettings speedSettings;
    [SerializeField] GameObject rocketPrefab;
    private Vector2 touchStartPosition;
    private bool isDragging = false;
    private float trailRendererValue = 0.155f;

    void Update()
    {
        //ControlMovementByButtons();
        ControlMovementByKeys();
        MovementByTouch();
        /*if (SaveSettings.instance.buttonSensEnabled)
        {
            ControlMovementByButtons();
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
        }
        else if(!SaveSettings.instance.buttonSensEnabled) 
        {
            //UponDeviceTilt();
           
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
        }*/
      
        
        
        ResetPosition();

    }

    private void MovementByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0; 
            
            switch (touch.phase)
            {
                
                case TouchPhase.Began:
                    isDragging = true;
                    touchStartPosition = touchPosition;
                    /*  if (transform.gameObject.GetComponent<Collider2D>().bounds.Contains(touchPosition))
                      {
                          isDragging = true;
                          touchStartPosition = touchPosition;
                      }*/
                    break;
                case TouchPhase.Moved:
                  
                    if (isDragging) 
                    {
                        float moveX = touchPosition.x - touchStartPosition.x;
                        transform.Translate(Vector3.right * moveX);
                        touchStartPosition = touchPosition;
                    }
                    break;
                case TouchPhase.Ended:
                  
                    isDragging = false;
                    break;
                
            }
        }
    }

    private void ControlMovementByKeys()
    {
        float xInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speedSettings.keySpeed * Time.deltaTime * xInput);
    }

    private void UponDeviceTilt()
    {
        if (Input.acceleration.x < -0.1f)
            transform.Translate(Input.acceleration.x * Vector3.right * speedSettings.tiltSpeed * Time.deltaTime);

        else if (Input.acceleration.x > 0.1f)
            transform.Translate(Input.acceleration.x * Vector3.right * speedSettings.tiltSpeed * Time.deltaTime);
    }

    private void ResetPosition()
    {
        if(transform.position.x < -endX)
            transform.position = new Vector3(-endX,transform.position.y,transform.position.z);

        else if (transform.position.x > endX)
            transform.position = new Vector3(endX, transform.position.y, transform.position.z);
    }

   
    private void ControlMovementByButtons()
    {
        transform.Translate(Vector3.right * speedSettings.keySpeed * Time.deltaTime * GameObject.Find("GameManager").GetComponent<Game_Manager>().horizontalInput);      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        powerupCollect.Play();
        switch(collision.gameObject.tag) 
        {
            case "ball3x":
                if(ball.gameObject != null)
                {
                    float size = GameObject.FindGameObjectWithTag("ball").GetComponent<Transform>().localScale.x;
                    GameObject ballGameObject1 = Instantiate(ball, GameObject.FindGameObjectWithTag("ball").GetComponent<Transform>().position, Quaternion.identity);
                    ballGameObject1.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<Game_Manager>().ballColors[LevelSelector.Instance.RandomBallColor];
                    ballGameObject1.GetComponent<Transform>().localScale = new Vector3(size, size, size);
                    ballGameObject1.GetComponent<TrailRenderer>().enabled = true;
                    SetTrailRendererWidth(ballGameObject1);
                    GameObject ballGameObject2 = Instantiate(ball, GameObject.FindGameObjectWithTag("ball").GetComponent<Transform>().position, Quaternion.identity);
                    ballGameObject2.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<Game_Manager>().ballColors[LevelSelector.Instance.RandomBallColor];
                    ballGameObject2.GetComponent<Transform>().localScale = new Vector3(size, size, size);
                    ballGameObject2.GetComponent<TrailRenderer>().enabled = true;
                    SetTrailRendererWidth(ballGameObject2);
                }
         
                Destroy(collision.gameObject);
                break;
            case "ballexpand":
                GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
                if(balls.Length != 0)
                {
                    foreach(GameObject ball1 in balls)
                    {
                        if (ball1.GetComponent<Transform>().localScale.x <= 0.35)
                        {
                            
                            ball1.GetComponent<Transform>().localScale = new Vector2(ball1.GetComponent<Transform>().localScale.x + 0.05f, ball1.GetComponent<Transform>().localScale.y + 0.05f);
                            SetTrailRendererWidth(ball1);
                        }
                    }
                }
                Destroy(collision.gameObject);
                break;
            case "ballshrink":
                GameObject[] balls1 = GameObject.FindGameObjectsWithTag("ball");
                if (balls1.Length != 0)
                {
                    foreach (GameObject ball2 in balls1)
                    {
                        if (ball2.GetComponent<Transform>().localScale.x >= 0.1)
                        {
                            
                            ball2.GetComponent<Transform>().localScale = new Vector2(ball2.GetComponent<Transform>().localScale.x - 0.05f, ball2.GetComponent<Transform>().localScale.y - 0.05f);
                            SetTrailRendererWidth(ball2);
                        }
                           
                    }
                }
                Destroy(collision.gameObject);
                break;
            case "playerexpand":
                GameObject player1 = GameObject.FindGameObjectWithTag("Player");
                player1.GetComponent<Transform>().localScale = new Vector2(player1.GetComponent<Transform>().localScale.x + 0.05f, player1.GetComponent<Transform>().localScale.y + 0.01f);
                Destroy(collision.gameObject);
                break;
            case "playershrink":
                GameObject player11 = GameObject.FindGameObjectWithTag("Player");
                if (player11.GetComponent<Transform>().localScale.x >= 0.1)
                {
                    player11.GetComponent<Transform>().localScale = new Vector2(player11.GetComponent<Transform>().localScale.x - 0.05f, player11.GetComponent<Transform>().localScale.y - 0.05f);
                }
                Destroy(collision.gameObject);
                break;
            case "slowdown":
                GameObject[] balls2 = GameObject.FindGameObjectsWithTag("ball");
                if (balls2.Length != 0)
                {
                    foreach (GameObject ball3 in balls2)
                    {
                        Rigidbody2D rb = ball3.GetComponent<Rigidbody2D>();
                        rb.velocity = rb.velocity - new Vector2(rb.velocity.x + 10, rb.velocity.y + 10);
                    }
                }
                Destroy(collision.gameObject);
                break;
            case "speedup":
                GameObject[] balls3 = GameObject.FindGameObjectsWithTag("ball");
                if (balls3.Length != 0)
                {
                    foreach (GameObject ball4 in balls3)
                    {
                        Rigidbody2D rb = ball4.GetComponent<Rigidbody2D>();
                        rb.velocity = rb.velocity - new Vector2(rb.velocity.x - 10, rb.velocity.y - 10);
                    }
                }
                Destroy(collision.gameObject);
                break;
            case "rocket":
               
                StartCoroutine(WaitForNextLaunch());
                Destroy(collision.gameObject);

                break;
            default:
                    break;


        }
    }
    //how often rocket should launch or how much time it should launch
    IEnumerator  WaitForNextLaunch()
    {
        yield return new WaitForSeconds(1f);
        int count = 0;
        while (count < 10)
        {
            Vector2 leftLaunchPos = new Vector2((gameObject.transform.position.x - gameObject.transform.localScale.x / 2), gameObject.transform.position.y);
            Vector2 rightLaunchPos = new Vector2((gameObject.transform.position.x + gameObject.transform.localScale.x / 2), gameObject.transform.position.y);
            GameObject leftRocket = Instantiate(rocketPrefab, leftLaunchPos, Quaternion.identity);
            GameObject rightRocket = Instantiate(rocketPrefab, rightLaunchPos, Quaternion.identity);
            Rigidbody2D rb1 = leftRocket.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = rightRocket.GetComponent<Rigidbody2D>();
            rb1.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rb2.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            count++;
            yield return new WaitForSeconds(0.5f);
            
        }
    }

    public void SetTrailRendererWidth(GameObject ball5)
    {
        float updatedTrailRendererValue =
       
        ball5.GetComponent<TrailRenderer>().startWidth = (ball5.GetComponent<Transform>().localScale.x - trailRendererValue) + ball5.GetComponent<Transform>().localScale.x;
    }
   

}

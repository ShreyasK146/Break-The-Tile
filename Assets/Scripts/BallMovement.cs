using System.Collections;

using UnityEngine;


public class BallMovement : MonoBehaviour
{
    Vector3 launchDirection;
    [SerializeField] float launchForce = 1f;
    [SerializeField] GameObject playerPad;
    Vector3 touchPositionInWorld;
    private Rigidbody2D rb;
    int luckyNumber = 4;
    [SerializeField] PowerUpsSpawner powerUpsSpawner;
    [SerializeField] AudioSource ballCollideAudio;
    [SerializeField] ParticleSystem collideEffect;
    Game_Manager gamemanager;


    private void Start()
    {
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        playerPad = GameObject.Find("Player").gameObject;
        rb = GetComponent<Rigidbody2D>();
        ballCollideAudio = GameObject.Find("ballcollide").GetComponent<AudioSource>();
        gamemanager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }

    private void Update()
    {
       // PlayTrail();
        FollowPlayerBeforeLaunch();
        LaunchOnClick();
        KeepSameSpeed();

    }


    private void FollowPlayerBeforeLaunch()
    {
        if (!GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunched)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPad.transform.position.x, playerPad.transform.position.y + 0.1f, playerPad.transform.position.z), 100f * Time.deltaTime);
        else
            gameObject.GetComponent<TrailRenderer>().enabled = true;

    }

    private void LaunchOnClick()
    {
        
        if (!GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunched && GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunchable)
        {
            if (Input.touchCount > 0)
            {
                
                touchPositionInWorld = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                touchPositionInWorld.z = 0f;
                launchDirection = (touchPositionInWorld - transform.position).normalized;
                rb.velocity = Vector2.zero;
                rb.AddForce(launchDirection * launchForce ,ForceMode2D.Impulse);
                GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunched = true;
                GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunchable = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector3 MouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MouseClickPos.z = 0f;
                launchDirection = (MouseClickPos - transform.position).normalized;
                rb.velocity = Vector2.zero;
                rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
                GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunched = true;
                GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunchable = false;
            }
            
            
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         ballCollideAudio.Play();
         if (collision.gameObject.CompareTag("Player"))
         {
              //depending on the contact point between ball and playerpad change the launch direction 
              //if contactpoint is less than playerpad pos then relativehit pos will be negative which will bounce off the ball in negative direction/angle
              Vector2 contactPoint = collision.GetContact(0).point;
              Vector2 playerPadPos = collision.gameObject.transform.position;
              float relativeHitPoint = contactPoint.x - playerPadPos.x;
              Vector2 bounceDirection = rb.velocity.normalized + new Vector2(relativeHitPoint * 2.1f, 0);
              rb.velocity = bounceDirection.normalized * rb.velocity.magnitude;

         }
         else
         {
              Vector2 newVelocity = rb.velocity + new Vector2(0.5f, 0.1f);
              rb.velocity = newVelocity;

         }
        


        if (collision.gameObject.CompareTag("strongtile"))
        {
            gamemanager.score += 100;
            collision.gameObject.tag = "tile";
        }

        else if (collision.gameObject.CompareTag("tile"))
        {

            ParticleSystem vfx = Instantiate(collideEffect, collision.gameObject.transform.position, Quaternion.identity);

            gamemanager.score += 100;

            if (Random.Range(1, 11) == luckyNumber)
            {
                GameObject powerUp = powerUpsSpawner.powerUps[Random.Range(0, powerUpsSpawner.powerUps.Count)];
                Instantiate(powerUp, collision.gameObject.transform.position, Quaternion.identity);
            }

            StartCoroutine(Wait(collision.gameObject, vfx));
        }


    }

    private void KeepSameSpeed()
    {
        
        if (rb.velocity.magnitude != launchForce)
        {

            rb.velocity = rb.velocity.normalized * launchForce;
            
        }
        
            
    }
   
    IEnumerator  Wait(GameObject tile,ParticleSystem vfx)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(vfx.gameObject);
        Destroy(tile.gameObject);
        
    }
   
  /*  private void PlayTrail()
    {
        if(!electrictrail.isPlaying)
            electrictrail.Play();
    }*/

  
}

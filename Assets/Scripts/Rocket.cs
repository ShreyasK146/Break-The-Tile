
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] PowerUpsSpawner powerUpsSpawner;
    [SerializeField] AudioSource rocketCollideAudio;
    [SerializeField] ParticleSystem collideEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rocketCollideAudio.Play();
        if (collision.gameObject.CompareTag("strongtile"))
        {
            GameObject.Find("GameManager").GetComponent<Game_Manager>().score += 100;
            collision.gameObject.tag = "tile";
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("tile"))
        {

            ParticleSystem vfx = Instantiate(collideEffect, collision.gameObject.transform.position, Quaternion.identity);

            GameObject.Find("GameManager").GetComponent<Game_Manager>().score += 100;

            if (Random.Range(1, 11) == 4)
            {
                GameObject powerUp = powerUpsSpawner.powerUps[Random.Range(0, powerUpsSpawner.powerUps.Count)];
                Instantiate(powerUp, collision.gameObject.transform.position, Quaternion.identity);
            }

            Destroy(collision.gameObject);
            Destroy(vfx.gameObject);
            Destroy(gameObject);
        }
        
        
        
    }
   

}

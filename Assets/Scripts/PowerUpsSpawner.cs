
using System.Collections.Generic;

using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> powerUps;
    
    private void Start()
    {
        
        InvokeRepeating("SpawnRandomPowerups",7,7);


    }

    private void SpawnRandomPowerups()
    {
        if(GameObject.Find("GameManager").GetComponent<Game_Manager>().ballLaunched)
        {
            GameObject powerUp = powerUps[Random.Range(0, powerUps.Count)];

            Instantiate(powerUp, new Vector3(Random.Range(-7f, 7f), 5f, 0f), Quaternion.identity);
        }
        

    }


}

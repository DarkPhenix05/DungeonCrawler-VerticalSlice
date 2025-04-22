using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject projectile;

    public Transform spawnLocation;

    public Quaternion spawnRotation;

    public float spawnTime = 0.5f;

    private float timeSinceSpawned = 0.5f; 

    public DetectionZone detectionZone;

     void Start()
    {
        
    }

     void Update()
    {
        
        if (detectionZone.detectedObjs.Count > 0)
        {
            timeSinceSpawned += Time.deltaTime;

            if (timeSinceSpawned >= spawnTime)
            {
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timeSinceSpawned = 0;
            }    
           
        } else {

            timeSinceSpawned = 0.5f;
        }

       
    }
}

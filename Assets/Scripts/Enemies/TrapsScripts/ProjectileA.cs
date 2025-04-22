using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileA : MonoBehaviour
{
    
    public float moveSpeed = 10f;

    public float timeToLive = 5f; 

    private float timeSinceSpawned = 0f;


    public float knockbackForce = 200f;

    public int damage = 1;

    public Player dMono; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.position += moveSpeed * transform.up * Time.deltaTime; 

        timeSinceSpawned += Time.deltaTime;

        if(timeSinceSpawned > timeToLive)
        {
            Destroy(gameObject);    
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        string tag = collider.gameObject.tag;

        if(tag == "Player")
        {
            dMono = collider.transform.gameObject.GetComponent<Player>();
            if(dMono != null)
            {
                dMono.TakeDamage(damage);
                Destroy(gameObject);    
            }

        }

    }

}

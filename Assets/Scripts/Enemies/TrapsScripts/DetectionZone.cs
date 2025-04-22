using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public string tagTarget = "Player";

    public List<Collider> detectedObjs = new List<Collider> ();
  

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == tagTarget) 
        { 
           detectedObjs.Add(collider);
        
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collider);
        }
    }
}

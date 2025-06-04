using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EndDoor : MonoBehaviour
{
    public Text hint;
    public GameObject doorGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hint.gameObject.SetActive(true);
            if (Inventory.instance.HaveNeededKey(3))
            {
                hint.text = "Press interact to open door";
                other.gameObject.GetComponent<Player>().endDoor = true;
                //if (Input.GetKeyDown(KeyCode.E))
                //{
                //    doorGameObject.SetActive(false);
                //}
            }
            else
            {
                hint.text = "You need a big key to open this door...";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hint.gameObject.SetActive(false);
        }
    }
}

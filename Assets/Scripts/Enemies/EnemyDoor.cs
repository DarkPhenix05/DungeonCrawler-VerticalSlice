using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyDoor : MonoBehaviour
{
    public UnityEvent onKillEnemyEvent;
    public List<EnemyScript> enemies;
    public GameObject doorGameObject;
    private int counter = 0;

    public Text hint;
    public GameObject mediumKeyReward;
    public Transform keyPos;

    private void Start()
    {
        // A todos los enemigos se les asigna como su onKilledEnemyEvent la función de DoorDisableCounter. Los eventos añadidios por código no
        // Se reflejan en el inspector.
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].onKilledEnemyEvent.AddListener(DoorDisableCounter);
        }
    }

    //Esta función se llamará cuando un enemigo sea derrotado subiendo el contador que cuando sea igual o mayor al número de enemigos asignados en la
    //lista desaparecerá la puerta, aqui recomiendo reemplazar el set active por un tween para un efecto más estético.
    public void DoorDisableCounter()
    {
        counter++;
        if (counter >= enemies.Count)
        {
            //Drop Mid Key at position of last enemy killed
            //enemies[^1].transform
            Instantiate(mediumKeyReward, keyPos);
            Debug.Log("MediumKeyInstantiated");
        }
    }

    //Esta función es para activar la puerta, recomiendo usar un script por aparte con una función on triggerEnter que llame a esta función para activar
    //la puerta, una vez que pase desabilitar el trigger para que el jugador no vuelva a interactuar con el.
    public void DoorEnable()
    {
        doorGameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hint.gameObject.SetActive(true);
            if (Inventory.instance.HaveNeededKey(2))
            {
                hint.text = "Press interact to open door";
                other.gameObject.GetComponent<Player>().bossDoor = true;
                //if (Input.GetKeyDown(KeyCode.E))
                //{
                //    doorGameObject.SetActive(false);
                //}
            }
            else
            {
                hint.text = "You need a medium key to open this door...";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hint.gameObject.SetActive(false);
        }
    }
}
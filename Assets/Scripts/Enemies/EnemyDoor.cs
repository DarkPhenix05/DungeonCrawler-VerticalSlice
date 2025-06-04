using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyDoor : MonoBehaviour
{
    public List<EnemyScript> enemies;
    public GameObject doorGameObject;
    private int _counter = 0;

    public Text hint;
    public GameObject key;
    private bool _canOpen = false;

    [Header("BOSS")]
    public GameObject BOSS;

    private void Start()
    {
        key.SetActive(false);
        BOSS.SetActive(false);
    }

    //Esta funci�n se llamar� cuando un enemigo sea derrotado subiendo el contador que cuando sea igual o mayor al n�mero de enemigos asignados en la
    //lista desaparecer� la puerta, aqui recomiendo reemplazar el set active por un tween para un efecto m�s est�tico.
    public void DoorDisableCounter(Transform enemyTransform)
    {
        _counter++;
        if (_counter >= enemies.Count)
        {
            key.transform.position = enemyTransform.position; 
            key.SetActive(true);
            Debug.Log("MediumKeyInstantiated at:" + enemyTransform.position.ToString());
        }
    }

    //Esta funci�n es para activar la puerta, recomiendo usar un script por aparte con una funci�n on triggerEnter que llame a esta funci�n para activar
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
                other.gameObject.GetComponent<Player>().SetBossDoor(true, this.gameObject);
                Debug.Log("Player has required key for boss door");
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
        else
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hint.gameObject.SetActive(false);
            other.gameObject.GetComponent<Player>().SetBossDoor(false); 
        }
    }

    public void OpenBossDoor(GameObject other)
    {
        hint.gameObject.SetActive(false);
        BOSS.SetActive(true);
        other.gameObject.GetComponent<Player>().SetBossDoor(false); 
        doorGameObject.SetActive(false);
    }
}
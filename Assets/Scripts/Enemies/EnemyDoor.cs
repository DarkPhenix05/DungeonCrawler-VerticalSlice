using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDoor : MonoBehaviour
{
    public UnityEvent onKillEnemyEvent;
    public List<EnemyScript> enemies;
    public GameObject doorGameObject;
    private int counter = 0;

    private void Start()
    {
        // A todos los enemigos se les asigna como su onKilledEnemyEvent la funci�n de DoorDisableCounter. Los eventos a�adidios por c�digo no
        // Se reflejan en el inspector.
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].onKilledEnemyEvent.AddListener(DoorDisableCounter);
        }
    }

    //Esta funci�n se llamar� cuando un enemigo sea derrotado subiendo el contador que cuando sea igual o mayor al n�mero de enemigos asignados en la
    //lista desaparecer� la puerta, aqui recomiendo reemplazar el set active por un tween para un efecto m�s est�tico.
    public void DoorDisableCounter()
    {
        counter++;
        if (counter >= enemies.Count)
        {
            doorGameObject.SetActive(false);
        }
    }

    //Esta funci�n es para activar la puerta, recomiendo usar un script por aparte con una funci�n on triggerEnter que llame a esta funci�n para activar
    //la puerta, una vez que pase desabilitar el trigger para que el jugador no vuelva a interactuar con el.
    public void DoorEnable()
    {
        doorGameObject.SetActive(false);
    }
}
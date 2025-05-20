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
            doorGameObject.SetActive(false);
        }
    }

    //Esta función es para activar la puerta, recomiendo usar un script por aparte con una función on triggerEnter que llame a esta función para activar
    //la puerta, una vez que pase desabilitar el trigger para que el jugador no vuelva a interactuar con el.
    public void DoorEnable()
    {
        doorGameObject.SetActive(false);
    }
}
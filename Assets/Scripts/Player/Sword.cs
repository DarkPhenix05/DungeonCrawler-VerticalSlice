using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public Collider swordCollider;
    public List<Transform> positionList;
    public int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.transform.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
        }
    }

    public void SetPosition(int index)
    {
        transform.localPosition = positionList[index].localPosition;
        transform.localScale = positionList[index].localScale;
    }
}

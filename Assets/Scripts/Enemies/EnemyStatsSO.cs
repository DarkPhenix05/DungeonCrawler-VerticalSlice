using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatsSO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Stats")]
    public int maxHP;
    public int attackPower;
    public float attackFrequency;
    public float speed;
}
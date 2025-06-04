using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject goldKey;
    public Transform keyPos;

    private void OnDisable()
    {
        Instantiate(goldKey, keyPos);
    }
}

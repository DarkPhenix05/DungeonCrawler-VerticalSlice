using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject _goldKey;

    private void Start()
    {
        _goldKey.SetActive(false);
    }

    private void OnDisable()
    {
        _goldKey.transform.position = this.gameObject.transform.position;
        _goldKey.SetActive(true);
    }
}

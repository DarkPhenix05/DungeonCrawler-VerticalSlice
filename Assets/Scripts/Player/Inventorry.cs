using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Inventorry : MonoBehaviour
{
    [Header("UI")]
    public Text goldText;

    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private int _gold;
    [SerializeField] private int _smallKey;
    [SerializeField] private int _midKey;
    [SerializeField] private int _bigKey;
    
    [SerializeField] private int _selectedWeapon;
    [SerializeField] private Transform _transforms;

    void Start()
    {
        _transform = transform;
        UpdateUIGoldValue();
    }

    public void TakeGold(GameObject gold, int value)
    {
        gold.GetComponent<Gold>().PickUpEfect();
        gold.transform.position = _transform.position;

        _gold += value;
        UpdateUIGoldValue();
    }

    public void UseGold(int amout)
    {
        if(HaveGold(amout)) 
        { 
            _gold -= amout;
            UpdateUIGoldValue();
        }
        else
        {
            Debug.Log("CAN'T AFORD");
        }
    }

    public bool HaveGold(int cost)
    {
        return _gold >= cost;
    }

    public int GetGold() 
    {
        return _gold;
    }

    public void TakeKey(GameObject pickUp, int tipe)
    {
        Key tempkey = pickUp.GetComponent<Key>();
        tempkey.PickUpEfect();
        pickUp.GetTipe();
    }

    private void AddKey(int T)
    {
        if(T == 1)
        {
            _smallKey++
        }
        else if(T == 2)
        {
            _midKey++
        }
        else if(T == 3)
        {
            _bigKey++
        }
        else
        {
            Debug.Log("ERROR KEY TIPE OVER FLOW");
        }
    }
    public bool HaveNeededItem(int tipe)
    {
        switch(tipe)
        {
            case 1:
            
        }

        return false;
    }

    public void UseKey(int tipe)
    {
        
    }

    private void UpdateUIGoldValue()
    {
        goldText.text = _gold.ToString();
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Transform _transform;

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

    public void TakeKey(GameObject key, int tipe)
    {
        Key tempkey = key.GetComponent<Key>();
        tempkey.PickUpEfect();

        int T = tempkey.GetTipe();
        AddKey(T);
    }

    public bool HaveNeededKey(int tipe)
    {
        switch(tipe)
        {
            case 1:
            return (_smallKey > 0);
                
            case 2:
            return (_midKey > 0);

            case 3:
            return (_bigKey > 0);
        }

        return false;
    }

    public void UseKey(int tipe)
    {
        switch (tipe)
        {
            case 1:
                _smallKey--;
                return;

            case 2:
                _midKey--;
                return;

            case 3:
                _bigKey--;
                return;
        }
    }

    public void AddKey(int tipe)
    {
        switch (tipe)
        {
            case 1:
                _smallKey++;
                return;

            case 2:
                _midKey++;
                return;

            case 3:
                _bigKey++;
                return;
        }
    }

    private void UpdateUIGoldValue()
    {
        goldText.text = _gold.ToString();
    }
}
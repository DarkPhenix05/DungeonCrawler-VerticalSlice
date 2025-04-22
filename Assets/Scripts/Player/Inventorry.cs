using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Inventorry : MonoBehaviour
{
    [Header("UI")]
    public Text goldText;

    [SerializeField] private List<GameObject> _pickedObjects;
    [SerializeField] private List<int> _pickedObjectsUses;
    [SerializeField] private int _gold;

    [SerializeField] private GameObject _tempObj;
    [SerializeField] private Transform _transform;
    
    [SerializeField] private int _selectedObject;

    void Start()
    {
        _transform = transform;
        UpdateUIGoldValue();
    }

    public void TakeGold(GameObject gold, int value)
    {
        gold.GetComponent<Gold>().PickUpEfect();
        gold.transform.position = _transform.position;
        gold.transform.parent = _transform;

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

    public void TakePickUp(GameObject pickUp, int uses)
    {
        pickUp.GetComponent<Key>().PickUpEfect();
        pickUp.transform.position = _transform.position;
        pickUp.transform.parent = _transform;

        _pickedObjects.Add(pickUp);
        _pickedObjectsUses.Add(uses);
    }

    public bool HaveNeededItem(GameObject requieredItem)
    {
        for (int i = 0; _pickedObjects.Count > i; i++)
        {
            if (_pickedObjects[i] == requieredItem)
            {
               _selectedObject = i;
               Debug.Log(_selectedObject);
                return true;
            }
        }

        return false;
    }

    public void UseKey()
    {
        Debug.Log(_pickedObjectsUses[_selectedObject]);
        
        int temp = _pickedObjectsUses[_selectedObject];
        temp -= 1;
        
        _pickedObjectsUses[_selectedObject] = temp;
        
        Debug.Log(_pickedObjectsUses[_selectedObject]);
        
        if (_pickedObjectsUses[_selectedObject] <= 0.0f)
        {
                _pickedObjects.RemoveAt(_selectedObject);
                _pickedObjectsUses.RemoveAt(_selectedObject);
        }
    }

    private void UpdateUIGoldValue()
    {
        goldText.text = _gold.ToString();
    }
}
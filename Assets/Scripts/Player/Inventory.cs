using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    public Text goldText;

    public AudioSource _audioSource;
    public AudioClip _goldSFX;

    [SerializeField] private List<GameObject> _weapons;
    private int curWeapon = -1;
    private bool weaponEquiped = false;

    [SerializeField] private List<GameObject> _shields;
    private int curShield = -1;
    private bool shieldEquiped = false;

    [SerializeField] private int _gold = 0;
    [SerializeField] private int _tempGold = 0;
    public float animDuration;

    [SerializeField] private int _smallKey;
    [SerializeField] private int _midKey;
    [SerializeField] private int _bigKey;
    
    [SerializeField] private int _selectedWeapon;
    [SerializeField] private Transform _transform;

    private Transform _leftTransform;
    private Transform _rightTransform;

    void Start()
    {
        _leftTransform = GameObject.Find("weapon_l").transform;
        _rightTransform = GameObject.Find("weapon_r").transform;

        _audioSource = gameObject.GetComponent<AudioSource>();

        _transform = this.gameObject.transform;

        UpdateUIGoldValue();

        if(!weaponEquiped)
            EquipWeapon(0);
        if(!shieldEquiped)
            EquipShield(0);
    }

    public int GetCurWeapon()
    {
        return curWeapon;
    }
    public int GetCurShield()
    {
        return curShield;
    }

    public int GetWeaponVal(GameObject weapon)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            if (_weapons[i] == weapon)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddToWeapons(GameObject weapon)
    {
        _weapons.Add(weapon);
        weapon.transform.parent = _transform;
    }
    public void AddToShields(GameObject shield)
    {
        _shields.Add(shield);
        shield.transform.parent = _transform;
    }

    public void EquipWeapon(int weapon)
    {
        if (weapon > (_weapons.Count - 1))
        {
            Debug.LogWarning("DON'T HAVE THAT WEAPON NUMBER");
            return;
        }

        GameObject tempWeapon;

        if (weaponEquiped)
        {
            tempWeapon = _weapons[curWeapon];
            tempWeapon.SetActive(false);
            tempWeapon.transform.parent = _transform;
        }
        

        curWeapon = weapon;
        weaponEquiped = true;
        tempWeapon = _weapons[weapon];
        tempWeapon.transform.parent = _rightTransform;
        tempWeapon.transform.position = _rightTransform.position;
        tempWeapon.SetActive(true);
    }
    public void EquipShield(int shield)
    {
        if (shield > (_shields.Count - 1))
        {
            Debug.LogWarning("DON'T HAVE THAT SHIELD NUMBER");
            return;
        }

        GameObject tempShield;

        if (shieldEquiped)
        {
            tempShield = _shields[curShield];
            tempShield.SetActive(false);
            tempShield.transform.parent = _transform;
        }
        

        curShield = shield;
        shieldEquiped = true;
        tempShield = _shields[shield];
        tempShield.transform.parent = _leftTransform;
        tempShield.transform.position = _leftTransform.position;
        tempShield.SetActive(true);
    }

    public void TakeGold(GameObject gold, int value)
    {
        gold.GetComponent<Gold>().PickUpEfect();
        gold.transform.position = _transform.position;

        _tempGold = _gold;
        _gold += value;
        UpdateUIGoldValue();
    }

    public void UseGold(int amout)
    {
        if(HaveGold(amout)) 
        {
            _tempGold = _gold;
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
        StartCoroutine(GoldAnimation());
    }

    IEnumerator GoldAnimation()
    {
        float dtGold = _gold - _tempGold;
        float wait;
        if (animDuration > 0)
        {
            wait = animDuration/ dtGold;
        }
        else
        {
            wait = 0;
        }

        yield return new WaitForSeconds(0.25f);

        if (dtGold > 0)
        {
            for (int i = 0; i < Mathf.Abs(dtGold); i++)
            {
                _tempGold++;
                GoldCounterSFX();
                goldText.text = _tempGold.ToString();
                yield return new WaitForSeconds(wait);
            }
        }
        else
        {
            for (int i = 0; i < Mathf.Abs(dtGold); i++)
            {
                _tempGold--;
                GoldCounterSFX();
                goldText.text = _tempGold.ToString();
                yield return new WaitForSeconds(wait);
            }
        }

        goldText.text = _gold.ToString();
    }

    private void GoldCounterSFX()
    {
        if (!_goldSFX || !_audioSource) return;

        _audioSource.clip = _goldSFX;
        //_audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        _audioSource.volume = 1.0f;
        _audioSource.Play();
        _audioSource.DORestart();
    }
}
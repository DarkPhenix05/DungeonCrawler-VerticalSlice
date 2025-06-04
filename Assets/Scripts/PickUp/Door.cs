using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject bossDoor;
    public GameObject endDoor;

    public bool boosDoor = false;

    private GameObject _player;
    private Player _playerScript;
    private bool opened = false;

    public AudioSource _audioSource;
    public AudioClip _openClip;
    public AudioClip _rewardClip;
    public AudioClip _closeClip;

    private void Awake()
    {
        _playerScript = FindObjectOfType<Player>();
        _player = _playerScript.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            if (!opened)
            {
                if (!boosDoor)
                {
                    _playerScript.bossDoor = true;
                }
                else
                {
                    _playerScript.endDoor = true;
                }
                _playerScript.inRange = true;
                _playerScript._interactionObject = this.gameObject;

                _playerScript.canCollect = _playerScript.HaveKey(1);
                Debug.Log(_playerScript.canCollect);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerScript.bossDoor = false;
            _playerScript.endDoor= false;
            _playerScript.inRange = false;
            _playerScript.canCollect = false;
            _playerScript._interactionObject = null;
        }
    }

    public void OpenBossDoor()
    {
        bossDoor.gameObject.SetActive(false);
    }

    public void OpenEndDoor()
    {
        endDoor.gameObject.SetActive(false);
    }

    public void Locked()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    [SerializeField] private Collider _cameraTrigger;
    [SerializeField] private int _cameraToSwitch;
    [SerializeField] private string _staticName;
    [SerializeField] private GameObject _staticCamera;
    [SerializeField] private Transform _cameraPos;

    public Player _player;

    private CameraManager _cameraManager;

    public bool _entrance = false;
    public bool _exit = true;

    void Start()
    {
        while (_staticCamera ==  null)
        {
            _staticCamera = GameObject.Find(_staticName);
        }

        while (_cameraTrigger == null) 
        { 
            _cameraTrigger = GetComponent<Collider>();
        }

        while (_cameraManager == null)
        {
            _cameraManager = CameraManager.instance;
        }

        _cameraTrigger.isTrigger = true;    
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!_entrance)
        {
            return;
        }

        Debug.Log(collision);
        _player = collision.GetComponent<Player>();

        if (_player != null) 
        { 
            CameraManager.instance.CameraSwitch(_cameraToSwitch);

            Debug.Log(CameraManager.instance.name);
            if(_cameraManager.GetCurVC(_cameraToSwitch).name ==  _staticName)
            {
                _staticCamera.transform.position = _cameraPos.position;
                _staticCamera.transform.rotation = _cameraPos.rotation;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(!_exit)
        {
            return;
        }

        //Debug.Log(collision);
        _player = collision.GetComponent<Player>();

        if (_player != null)
        {
            CameraManager.instance.CameraSwitch(_cameraToSwitch);

            Debug.Log(CameraManager.instance.name);
            if (_cameraManager.GetCurVC(_cameraToSwitch).name == _staticName)
            {
                _staticCamera.transform.position = _cameraPos.position;
                _staticCamera.transform.rotation = _cameraPos.rotation;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private int _initCam;
    [SerializeField] private List<CinemachineVirtualCamera> _camList;
    public int _currentCamera; 

    [SerializeField] private KeyCode _nextCamera;
    [SerializeField] private KeyCode _preCamera;

    public static CameraManager instance;

    /// <summary>
    /// This code manages the camera trancitions and sistemas. 
    /// This will be made using "Unity Cinemachine", and virtual cameras to move to locations.
    /// </summary>

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        if (_camList != null)
        {
            AssignCameraPriority();
        }

        _currentCamera = _initCam;
        CameraSwitch(_currentCamera);
    }

    /// <summary>
    /// We set a prioritiy automaticly with a for, in the order of the list.
    /// And using the function "CameraSwitch", we can move between them.
    /// </summary>

    private void AssignCameraPriority()
    {
        for(int i = 0; i < _camList.Count; i++)
        {
            _camList[i].Priority = i;
            if(i != _initCam)
            {
                _camList[i].gameObject.SetActive(false);
            }
        }
    }

    public void CameraSwitch(int cameraIndex)
    {
        _currentCamera = Mathf.Clamp(cameraIndex, 0, _camList.Count - 1);

        for(int i = 0; i < _camList.Count; i++)
        {
            if(_currentCamera == _camList[i].Priority)
            {
                _camList[i].gameObject.SetActive(true);
            }
            else
            {
                _camList[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Only used for DEBUG
    /// </summary>
    void Update()
    {
        if(Input.GetKeyUp(_nextCamera))
        {
            _currentCamera++;
            CameraSwitch(_currentCamera);
        }
        else if(Input.GetKeyDown(_preCamera))
        {
            _currentCamera--;
            CameraSwitch(_currentCamera);
        }
    }

    public GameObject GetCurVC(int cameraIndex)
    {
        return _camList[cameraIndex].gameObject;
    }
}

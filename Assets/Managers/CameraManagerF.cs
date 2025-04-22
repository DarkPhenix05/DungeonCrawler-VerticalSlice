using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerF : MonoBehaviour
{
    public static CameraManagerF instance;

    public int initialCameraIndex;
    public List <CinemachineVirtualCamera> cameraList;

    public KeyCode debugCameraSwitchKey;
    public int cameraSwitchDebugIndex;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        AssignCamerasPriority();
        CameraSwitch(initialCameraIndex);
    }

    private void Update()
    {
        if(Input.GetKeyDown(debugCameraSwitchKey))
        {
            CameraSwitch(cameraSwitchDebugIndex);
        }
    }

    /// <summary>
    /// Asigna la prioridad de las cámaras usando su orden en la lista
    /// </summary>
    private void AssignCamerasPriority()
    {
        for(int i = 0; i < cameraList.Count; i++)
            cameraList[i].Priority = i;
    }

    /// <summary>
    /// Cambia la cámara cuya prioridad sea la especificada
    /// </summary>
    /// <param name="cameraIndex">prioridad de la cámara deseada</param>
    public void CameraSwitch(int cameraIndex)
    {
        int clampedIndex = Mathf.Clamp(cameraIndex, 0, cameraList.Count - 1);

        for(int i = 0;i < cameraList.Count;i++)
        {
            if(clampedIndex == cameraList[i].Priority)
                cameraList[i].gameObject.SetActive(true);
            else
                cameraList[i].gameObject.SetActive(false);
        }
    }
}

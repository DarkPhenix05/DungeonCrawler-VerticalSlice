using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTriggerF : MonoBehaviour
{
    public int cameraToSwitch;
    public Collider cameraTrigger;

    private void Start()
    {
        cameraTrigger.isTrigger = true;
    }

    /// <summary>
    /// Si el jugador entra al trigger se cambiará a la cámara especificada por la variable
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter(Collider other)
    {
        Player player = GetComponent<Player>();

        if (player != null)
            CameraManagerF.instance.CameraSwitch(cameraToSwitch);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sobrecarga de CameraSwitch, tiene el mismo comportamiento base pero después de un tiempo especificado
/// regresará a la cámara especificada y desactivará el collider/trigger ppensado para mostrar la pista
/// </summary>
public class CameraSwitchHintTriggerF : CameraSwitchTriggerF
{
    public int cameraToReturn;
    public float hintTime;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        StartCoroutine(ReturnToCameraRoutine());
        cameraTrigger.enabled = false;
    }

    IEnumerator ReturnToCameraRoutine()
    {
        yield return new WaitForSeconds(hintTime);
        CameraManagerF.instance.CameraSwitch(cameraToReturn);
    }
}

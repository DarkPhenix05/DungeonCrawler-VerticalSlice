using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sobrecarga de CameraSwitch, tiene el mismo comportamiento base pero despu�s de un tiempo especificado
/// regresar� a la c�mara especificada y desactivar� el collider/trigger ppensado para mostrar la pista
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCamRotate : MonoBehaviour
{
    void Update()
    {
        Vector3 incomingVector = GameInstance.PlayerController.FpsCameraTf.position - transform.position;
        incomingVector = incomingVector.normalized;
        Vector3 normalVector = -transform.parent.transform.right;
        Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector);
        reflectVector = reflectVector.normalized;
        transform.forward = reflectVector;
    }
}

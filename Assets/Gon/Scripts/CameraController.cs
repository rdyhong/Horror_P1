using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target = null;
    bool _isSet = false;
    [SerializeField] float followSpeed = 0.8f;

    public void SetCameraTarget(Transform target)
    {
        _target = target;
        transform.SetParent(_target);

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        _isSet = true;
    }

    private void Update()
    {
        if (!_isSet) return;
        
        //transform.position = Vector3.Lerp(transform.position, _target.position, 0.5f);
    }
}

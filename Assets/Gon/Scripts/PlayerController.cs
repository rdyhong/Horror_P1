using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private CapsuleCollider _col;

    [SerializeField] Transform _cameraPos;

    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _sensitiveY = 2;
    [SerializeField] float _sensitiveX = 2;

    private float _cameraXRotate = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        PlayerMove();
        PlayerRotate();
        RayCheck();
    }

    void PlayerMove()
    {
        Vector3 nextStep = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        nextStep = Vector3.ClampMagnitude(nextStep, 1);
        nextStep = transform.position + nextStep * _moveSpeed * Time.deltaTime;
        _rb.MovePosition(nextStep);
    }

    void PlayerRotate()
    {
        // Rotate Y
        float yRotateSize = Input.GetAxis("Mouse X") * _sensitiveY;
        float yRotate = transform.eulerAngles.y + yRotateSize;
        transform.eulerAngles = new Vector3(0, yRotate, 0);

        // Rotate X
        float xRotateSize = -Input.GetAxis("Mouse Y") * _sensitiveX;
        _cameraXRotate = Mathf.Clamp(_cameraXRotate + xRotateSize, -80, 80);
        _cameraPos.localEulerAngles = new Vector3(_cameraXRotate, 0, 0);
    }

    void RayCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(_cameraPos.position, _cameraPos.forward * 5, Color.red);
        if (Physics.Raycast(_cameraPos.position, _cameraPos.forward, out hit, 3))
        {
            Item item = hit.transform.GetComponent<Item>();
            if(item)
            {
                Debug.Log(item.Name());
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private CapsuleCollider _col;

    [SerializeField] Transform _cameraTf;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _sensitiveX;
    [SerializeField] float _sensitiveY;

    private float _cameraXRotate = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _moveSpeed = GameDef.PLAYER_BASE_SPEED;
        _sensitiveX = GameDef.MOUSE_SENSITIVE_X;
        _sensitiveY = GameDef.MOUSE_SENSITIVE_Y;
    }

    void Update()
    {
        PlayerMove();
        PlayerRotate();
        RayCheck();
    }

    void PlayerMove()
    {
        Vector3 nextStep = transform.forward * Input.GetAxis(GameDef.INPUT_AXIS_VERTICAL) + transform.right * Input.GetAxis(GameDef.INPUT_AXIS_HORIZONTAL);
        nextStep = Vector3.ClampMagnitude(nextStep, 1);
        nextStep = transform.position + nextStep * _moveSpeed * Time.deltaTime;
        _rb.MovePosition(nextStep);
    }

    void PlayerRotate()
    {
        // Rotate Y
        float yRotateSize = Input.GetAxis(GameDef.INPUT_AXIS_MOUSE_X) * _sensitiveY;
        float yRotate = transform.eulerAngles.y + yRotateSize;
        transform.eulerAngles = new Vector3(0, yRotate, 0);

        // Rotate X
        float xRotateSize = -Input.GetAxis(GameDef.INPUT_AXIS_MOUSE_Y) * _sensitiveX;
        _cameraXRotate = Mathf.Clamp(_cameraXRotate + xRotateSize, GameDef.PLAYER_HEAD_ROTATE_X_MIN, GameDef.PLAYER_HEAD_ROTATE_X_MAX);
        _cameraTf.localEulerAngles = new Vector3(_cameraXRotate, 0, 0);
    }

    void RayCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(_cameraTf.position, _cameraTf.forward * GameDef.PLAYER_SIGHT_RAY_LENGTH, Color.red);
        if (Physics.Raycast(_cameraTf.position, _cameraTf.forward, out hit, GameDef.PLAYER_SIGHT_RAY_LENGTH))
        {
            Item item = hit.transform.GetComponent<Item>();
            if(item)
            {
                DebugUtil.Log(item.Name());
            }
        }

    }
}

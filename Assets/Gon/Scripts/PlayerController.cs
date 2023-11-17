using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private CapsuleCollider _col;

    [SerializeField] Transform _cameraTf;
    [SerializeField] Transform _handItemPosition;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _sensitiveX;
    [SerializeField] float _sensitiveY;

    private float _cameraXRotate = 0;

    Item _onHandItem = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _moveSpeed = GameDef.PLAYER_BASE_SPEED;
        _sensitiveX = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_X);
        _sensitiveY = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_Y);
    }

    void Update()
    {
        PlayerMove();
        PlayerRotate();
        RayCheck();

        // Test
        if(InputMgr.RMouseDown())
        {
            DebugUtil.Log("1");
        }
        if(InputMgr.RMouse())
        {
            DebugUtil.Log("2");
        }
        if(InputMgr.RMouseUp())
        {
            DebugUtil.Log("3");
        }
    }

    void PlayerMove()
    {
        Vector3 nextStep = transform.forward * InputMgr.KeyboardAxisY() + transform.right * InputMgr.KeyboardAxisX();
        nextStep = Vector3.ClampMagnitude(nextStep, 1);
        nextStep = transform.position + nextStep * _moveSpeed * Time.deltaTime;
        _rb.MovePosition(nextStep);
    }

    void PlayerRotate()
    {
        // Rotate Y
        float yRotateSize = InputMgr.MouseAxisX() * _sensitiveY;
        float yRotate = transform.eulerAngles.y + yRotateSize;
        transform.eulerAngles = new Vector3(0, yRotate, 0);

        // Rotate X
        float xRotateSize = -InputMgr.MouseAxisY() * _sensitiveX;
        _cameraXRotate = Mathf.Clamp(_cameraXRotate + xRotateSize, GameDef.PLAYER_HEAD_ROTATE_X_MIN, GameDef.PLAYER_HEAD_ROTATE_X_MAX);
        _cameraTf.localEulerAngles = new Vector3(_cameraXRotate, 0, 0);
    }

    void RayCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(_cameraTf.position, _cameraTf.forward * GameDef.PLAYER_SIGHT_RAY_LENGTH, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(_cameraTf.position, _cameraTf.forward, out hit, GameDef.PLAYER_SIGHT_RAY_LENGTH))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null && _onHandItem == null)
                {
                    _onHandItem = item;
                    _onHandItem.Obtain(_handItemPosition);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _onHandItem.Dump();
            _onHandItem = null;
        }
    }
}

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

    Vector3 _nextMoveStep = Vector3.zero;
    float _cameraXRotate = 0;

    Item _onHandItem = null;

    private void Awake()
    {
        GameInstance.SpawnPlayer();

        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _sensitiveX = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_X);
        _sensitiveY = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_Y);

        InputMgr.SetCursorAvtive(false); // Test Code

        Camera.main.transform.GetComponent<CameraController>().SetCameraTarget(_cameraTf); // Set Cam
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void Update()
    {
        PlayerRotate();
        PlayerJump();
        RayCheck();
    }
    
    void PlayerMove()
    {
        if(InputMgr.KeyHold(KeyCode.LeftShift)) _moveSpeed = GameDef.PLAYER_RUN_SPEED;
        else _moveSpeed = GameDef.PLAYER_BASE_SPEED;

        float dirX = InputMgr.KeyboardAxisX();
        float dirZ = InputMgr.KeyboardAxisZ();

        Vector3 nextPos;
        nextPos = transform.forward * dirZ + transform.right * dirX;
        nextPos = Vector3.ClampMagnitude(nextPos, 1);
        nextPos = nextPos * _moveSpeed;
        _nextMoveStep = Vector3.Lerp(_nextMoveStep, nextPos, 0.5f);
        _rb.MovePosition(transform.position + (_nextMoveStep * Time.fixedDeltaTime)); // Move Player
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

    void PlayerJump()
    {
        if(InputMgr.KeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * 200f, ForceMode.Impulse);
        }
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

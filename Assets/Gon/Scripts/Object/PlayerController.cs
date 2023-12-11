using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] CapsuleCollider _colStand;
    [SerializeField] CapsuleCollider _colCrouch;

    [SerializeField] Transform _cameraPosTf;
    [SerializeField] Transform _handItemPosition;
    [SerializeField] Transform _soundFootPosition;

    [SerializeField] float _moveSpeed;

    RaycastHit _playerHeighthit;

    InteractableObject _usingObj = null;
    public Transform OnHandTf => _handItemPosition;

    public bool IsFindObject => _isFindObject;
    bool _isFindObject = false;

    Vector3 _prevMoveStep = Vector3.zero;
    float _cameraXRotate = 0;
    float _cameraBaseHeight;

    float _moveDistance = 0;

    Item _onHandItem = null;

    private void Awake()
    {
        _cameraBaseHeight = _cameraPosTf.localPosition.y;
        GameInstance.Inst.SetPlayer(this);

        Camera.main.transform.GetComponent<CameraController>().SetCameraTarget(_cameraPosTf); // Set Cam
        _rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void Update()
    {
        PlayerRotate();
        RayCheck();
        PlayerCrouch();
    }

    void PlayerMove()
    {
        if (InputMgr.KeyHold(KeyCode.LeftControl)) _moveSpeed = GameDef.PLAYER_CROUCH_SPEED;
        else if (InputMgr.KeyHold(KeyCode.LeftShift)) _moveSpeed = GameDef.PLAYER_RUN_SPEED;
        else _moveSpeed = GameDef.PLAYER_BASE_SPEED;

        Vector3 nextPos;
        nextPos = transform.forward * InputMgr.KeyboardAxisZ() + transform.right * InputMgr.KeyboardAxisX();
        nextPos = Vector3.ClampMagnitude(nextPos, 1) * _moveSpeed;

        _rb.velocity = nextPos * Time.fixedDeltaTime;
        _moveDistance += Vector3.Distance(_prevMoveStep, transform.position);
        _prevMoveStep = transform.position;

        // Set height
        Debug.DrawRay(_cameraPosTf.position + Vector3.up, -transform.up * 20, Color.red);
        if (Physics.Raycast(transform.position + Vector3.up, -transform.up, out _playerHeighthit, 20))
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, _playerHeighthit.point.y + 0.1f, 0.4f), transform.position.z);
        }

        // Foot audio
        if (_moveDistance > 1)
        {
            _moveDistance = 0;
            PlayFootAudio();
        }
    }

    void PlayerRotate()
    {
        // Rotate Y
        float yRotateSize = InputMgr.PlayerMove_MouseAxisX() * UserData.s_MouseSensitiveY;
        float yRotate = transform.eulerAngles.y + yRotateSize;
        transform.eulerAngles = new Vector3(0, yRotate, 0);

        // Rotate X
        float xRotateSize = -InputMgr.PlayerMove_MouseAxisY() * UserData.s_MouseSensitiveX;
        _cameraXRotate = Mathf.Clamp(_cameraXRotate + xRotateSize, GameDef.PLAYER_HEAD_ROTATE_X_MIN, GameDef.PLAYER_HEAD_ROTATE_X_MAX);
        _cameraPosTf.localEulerAngles = new Vector3(_cameraXRotate, 0, 0);
    }

    void PlayerCrouch()
    {
        if (InputMgr.KeyHold(KeyCode.LeftControl))
        {
            _colStand.enabled = false;
            _colCrouch.enabled = true;

            _cameraPosTf.localPosition = Vector3.Lerp(_cameraPosTf.localPosition,
                new Vector3(_cameraPosTf.localPosition.x, _cameraBaseHeight - 0.8f, _cameraPosTf.localPosition.z), 3 * Time.deltaTime);
        }
        else
        {
            _colStand.enabled = true;
            _colCrouch.enabled = false;

            _cameraPosTf.localPosition = Vector3.Lerp(_cameraPosTf.localPosition,
                new Vector3(_cameraPosTf.localPosition.x, _cameraBaseHeight, _cameraPosTf.localPosition.z), 3 * Time.deltaTime);
        }
    }

    void PlayFootAudio()
    {
        RaycastHit hit;
        Vector3 rayStartPos = transform.position + (Vector3.up * 0.8f);
        float rayRange = 1.3f;

        if (Physics.Raycast(rayStartPos, -transform.up, out hit, rayRange, 1 << LayerMask.NameToLayer("GroundWood")))
        {
            AudioMgr.Inst.PlayFootEffect(ESoundTypeFoot.Wood, _soundFootPosition.position);
        }
        else if (Physics.Raycast(rayStartPos, -transform.up, out hit, rayRange, 1 << LayerMask.NameToLayer("GroundRock")))
        {
            AudioMgr.Inst.PlayFootEffect(ESoundTypeFoot.Rock, _soundFootPosition.position);
        }
        else if (Physics.Raycast(rayStartPos, -transform.up, out hit, rayRange, 1 << LayerMask.NameToLayer("GroundGrass")))
        {
            AudioMgr.Inst.PlayFootEffect(ESoundTypeFoot.Grass, _soundFootPosition.position);
        }
    }

    void RayCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(_cameraPosTf.position, _cameraPosTf.forward * GameDef.PLAYER_SIGHT_RAY_LENGTH, Color.red);

        if (InputMgr.LMouseDown())
        {
            if (Physics.Raycast(_cameraPosTf.position, _cameraPosTf.forward, out hit, GameDef.PLAYER_SIGHT_RAY_LENGTH))
            {
                DebugUtil.Log($"{hit.transform.name}");

                InteractableObject obj = hit.transform.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    _usingObj = obj;
                    _usingObj.OnEnter();
                }
            }

        }
        if (InputMgr.LMouse() && _usingObj != null)
        {
            _usingObj.OnUse();
        }
        if (InputMgr.LMouseUp() && _usingObj != null)
        {
            _usingObj.OnExit();
            _usingObj = null;
        }

        
        if (Physics.Raycast(_cameraPosTf.position, _cameraPosTf.forward, out hit, GameDef.PLAYER_SIGHT_RAY_LENGTH))
        {
            if(hit.transform.GetComponent<InteractableObject>() != null || hit.transform.GetComponent<Item>() != null)
            {
                _isFindObject = true;
            }
            else
            {
                _isFindObject = false;
            }
        }
        else
        {
            _isFindObject = false;
        }
    }

    public void SetOnHandItem(Item item)
    {
        _onHandItem = item;
    }
}

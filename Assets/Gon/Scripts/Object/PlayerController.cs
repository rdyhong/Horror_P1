using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;

    [SerializeField] Transform _cameraPosTf;
    [SerializeField] Transform _handItemPosition;
    [SerializeField] Transform _footPosition;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _gravity = 150f;

    InteractableObject _usingObj = null;
    public Transform OnHandTf => _handItemPosition;
    public Transform FpsCameraTf => _cameraPosTf;

    public bool IsFindObject => _isFindObject;
    bool _isFindObject = false;

    Vector3 _prevMoveStep = Vector3.zero;
    float _cameraXRotate = 0;
    float _cameraBaseHeight;

    float _moveDistance = 0;

    Item _onHandItem = null;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _cameraBaseHeight = _cameraPosTf.localPosition.y;
        GameInstance.Inst.SetPlayer(this);

        //Camera.main.transform.GetComponent<CameraController>().SetCameraTarget(_cameraPosTf); // Set Cam
    }

    void Update()
    {
        PlayerMove();
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

        //_rb.velocity = nextPos * Time.fixedDeltaTime;
        

        if (!_characterController.isGrounded)
        {
            nextPos.y -= _gravity * Time.deltaTime;
        }
        else
        {
            // Foot audio
            _moveDistance += Vector3.Distance(_prevMoveStep, transform.position);
            _prevMoveStep = transform.position;
            
            if (_moveDistance > 1)
            {
                _moveDistance = 0;
                PlayFootAudio();
            }
        }

        _characterController.Move(nextPos * Time.deltaTime);

        _animator.SetFloat("DirX", InputMgr.KeyboardAxisX());
        _animator.SetFloat("DirY", InputMgr.KeyboardAxisZ());
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
        float smooth = 5f;
        if (InputMgr.KeyHold(KeyCode.LeftControl))
        {
            _characterController.height = Mathf.Lerp(_characterController.height, 0.9f, smooth * Time.deltaTime);
            _characterController.center = Vector3.Lerp(_characterController.center, new Vector3(0, 0.45f, 0), smooth * Time.deltaTime);

            _cameraPosTf.localPosition = Vector3.Lerp(_cameraPosTf.localPosition,
                new Vector3(_cameraPosTf.localPosition.x, _cameraBaseHeight - 0.8f, _cameraPosTf.localPosition.z), smooth * Time.deltaTime);
        }
        else
        {
            _characterController.height = Mathf.Lerp(_characterController.height, 1.8f, smooth * Time.deltaTime);
            _characterController.center = Vector3.Lerp(_characterController.center, new Vector3(0, 0.9f, 0), smooth * Time.deltaTime);

            _cameraPosTf.localPosition = Vector3.Lerp(_cameraPosTf.localPosition,
                new Vector3(_cameraPosTf.localPosition.x, _cameraBaseHeight, _cameraPosTf.localPosition.z), smooth * Time.deltaTime);
        }
    }

    void PlayFootAudio()
    {
        RaycastHit hit;
        Vector3 rayStartPos = transform.position + (Vector3.up * 0.8f);
        float rayRange = 1.3f;

        if (Physics.Raycast(rayStartPos, -transform.up, out hit, rayRange, 1 << LayerMask.NameToLayer("GroundWood")))
        {
            AudioMgr.Inst.PlayFootEffect(ESoundTypeFoot.Wood, _footPosition.position);
        }
        else if (Physics.Raycast(rayStartPos, -transform.up, out hit, rayRange, 1 << LayerMask.NameToLayer("GroundRock")))
        {
            AudioMgr.Inst.PlayFootEffect(ESoundTypeFoot.Rock, _footPosition.position);
        }
        else if (Physics.Raycast(rayStartPos, -transform.up, out hit, rayRange, 1 << LayerMask.NameToLayer("GroundGrass")))
        {
            AudioMgr.Inst.PlayFootEffect(ESoundTypeFoot.Grass, _footPosition.position);
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

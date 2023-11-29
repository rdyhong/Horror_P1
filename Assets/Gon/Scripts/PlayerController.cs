using System.Collections;
using System.Collections.Generic;
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

    public bool IsFindObject => _isFindObject;
    bool _isFindObject = false;

    Vector3 _prevMoveStep = Vector3.zero;
    Vector3 _nextMoveStep = Vector3.zero;
    float _cameraXRotate = 0;
    float _cameraBaseHeight;

    float _moveDistance = 0;

    Item _onHandItem = null;

    

    private void Awake()
    {
        _cameraBaseHeight = _cameraPosTf.position.y;
        GameInstance.Inst.SetPlayer(this);

        Camera.main.transform.GetComponent<CameraController>().SetCameraTarget(_cameraPosTf); // Set Cam
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
        PlayerCrouch();
    }
    
    void PlayerMove()
    {
        if (InputMgr.KeyHold(KeyCode.LeftControl)) _moveSpeed = GameDef.PLAYER_CROUCH_SPEED;
        else if (InputMgr.KeyHold(KeyCode.LeftShift)) _moveSpeed = GameDef.PLAYER_RUN_SPEED;
        else _moveSpeed = GameDef.PLAYER_BASE_SPEED;

        _prevMoveStep = transform.position;

        float dirX = InputMgr.KeyboardAxisX();
        float dirZ = InputMgr.KeyboardAxisZ();

        Vector3 nextPos;
        nextPos = transform.forward * dirZ + transform.right * dirX;
        nextPos = Vector3.ClampMagnitude(nextPos, 1) * _moveSpeed;
        //nextPos = nextPos ;
        _nextMoveStep = Vector3.Lerp(_nextMoveStep, nextPos, 0.3f);
        _rb.MovePosition(transform.position + (_nextMoveStep * Time.fixedDeltaTime)); // Move Player

        
        _moveDistance += Vector3.Distance(_prevMoveStep, _prevMoveStep + (_nextMoveStep * Time.fixedDeltaTime));
        Debug.DrawRay(transform.position + (Vector3.up * 0.8f), -transform.up, Color.blue);
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

            _cameraPosTf.position = Vector3.Lerp(_cameraPosTf.position,
                new Vector3(_cameraPosTf.position.x, _cameraBaseHeight - 0.8f, _cameraPosTf.position.z), 3 * Time.deltaTime);
        }
        else
        {
            _colStand.enabled = true;
            _colCrouch.enabled = false;

            _cameraPosTf.position = Vector3.Lerp(_cameraPosTf.position,
                new Vector3(_cameraPosTf.position.x, _cameraBaseHeight, _cameraPosTf.position.z), 3 * Time.deltaTime);
        }
    }

    void PlayerJump()
    {
        if(InputMgr.KeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * 200f, ForceMode.Impulse);
        }
    }

    void PlayFootAudio()
    {
        RaycastHit hit;
        Vector3 rayStartPos = transform.position + (Vector3.up * 0.8f);
        float rayRange = 1.3f;
        LayerMask layer;

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

    InteractableObject _usingObj = null;
    void RayCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(_cameraPosTf.position, _cameraPosTf.forward * GameDef.PLAYER_SIGHT_RAY_LENGTH, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(_cameraPosTf.position, _cameraPosTf.forward, out hit, GameDef.PLAYER_SIGHT_RAY_LENGTH))
            {
                Item item = hit.transform.GetComponent<Item>();
                if (item != null && _onHandItem == null)
                {
                    _onHandItem = item;
                    _onHandItem.Obtain(_handItemPosition);
                }
            }
        }

        if (InputMgr.LMouseDown())
        {
            if (Physics.Raycast(_cameraPosTf.position, _cameraPosTf.forward, out hit, GameDef.PLAYER_SIGHT_RAY_LENGTH))
            {
                DebugUtil.Log($"{hit.transform.name}");
                InteractableObject obj = hit.transform.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    //InputMgr.StopPlayerMove(true);
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
            //InputMgr.StopPlayerMove(false);
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

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _onHandItem.Dump();
            _onHandItem = null;
        }
        */
    }
}

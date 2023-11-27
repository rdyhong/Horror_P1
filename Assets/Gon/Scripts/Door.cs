using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Door : InteractableObject
{
    [SerializeField] BoxCollider _boxCol;
    float _moveSpeed = 50f;
    [SerializeField] float _maxOpenAngle = 90.0f;
    private Vector3 defaulRot;
    private Vector3 _cutRot;
    private Vector3 _prevRot;
    
    private void Awake()
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUse()
    {
        base.OnUse();

        RaycastHit hit;

        //float nextYValue = transform.eulerAngles.y + (InputMgr.GetMouseAxisY() * UserData.s_MouseSensitiveY);
        float addValue = 0;
        if (InputMgr.GetMouseAxisY() > 0) addValue = _moveSpeed * Time.deltaTime;
        else if (InputMgr.GetMouseAxisY() < 0) addValue = -_moveSpeed * Time.deltaTime;

        float nextYValue = transform.eulerAngles.y + addValue;

        if (nextYValue > _maxOpenAngle)
        {
            nextYValue = _maxOpenAngle;
        }
        else if(nextYValue < 0)
        {
            nextYValue = 0;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, nextYValue, transform.eulerAngles.z);

        //_cutRot = Vector3.Slerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, nextYValue, transform.eulerAngles.z), 0.5f);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

﻿using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Door : InteractableObject
{
    [SerializeField] float _closeAngle = 0f;
    [SerializeField] float _maxOpenAngle = 90f;
    [SerializeField] float _shortOpenAngle = 10f;
    [SerializeField] bool _isLock = false;
    
    int _step = -1;
    int _nextStep = -1;
    int _prevStep = -1;

    float _curTargetAngle = 0;
    float _slowMoveSpeed = 2f;
    float _holdTimer = 0;

    bool _isOpen = false;
    bool _isMoveSlow = true;
    bool _isOverDistance = false;

    private void Update()
    {
        if (_step == -1) return;

        if(_step == 0) // Do Close
        {
            _curTargetAngle = _closeAngle;
        }
        else if(_step == 1) // Do Open
        {
            _curTargetAngle = _maxOpenAngle;
        }
        else if (_step == 2) // Do Open Short
        {
            _curTargetAngle = _shortOpenAngle;
        }

        Quaternion tgRot = Quaternion.Euler(transform.localEulerAngles.x, _curTargetAngle, transform.localEulerAngles.z);

        if (_isMoveSlow) transform.localRotation = Quaternion.Slerp(transform.localRotation, tgRot, _slowMoveSpeed * Time.deltaTime);
        else transform.localEulerAngles = transform.localEulerAngles + new Vector3(transform.localEulerAngles.x, _curTargetAngle, transform.localEulerAngles.z) * 10 * Time.deltaTime;

        if (Mathf.Abs(transform.localEulerAngles.y - _curTargetAngle) < 0.01f)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _curTargetAngle, transform.localEulerAngles.z);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (_isOpen)
        {
            _nextStep = 0;
            _prevStep = 1;
        }
        else
        {
            _nextStep = 1;
            _prevStep = 0;
            _step = 2;
        }

        _holdTimer = 0;
        _isMoveSlow = true;
        _isOverDistance = false;
    }

    public override void OnUse()
    {
        base.OnUse();

        if (_isOverDistance) return;

        if (Vector3.Distance(GameInstance.GetPlayerPosition(), transform.position) > 2f)
        {
            if (!_isOverDistance)
            {
                _isOverDistance = true;
                _step = _prevStep;
            }

            return;
        }

        if (!_isOpen)
        {
            _holdTimer += Time.deltaTime;

            if (_holdTimer > 1f) _nextStep = 0;
            else _nextStep = 1;
        }
    }

    public override void OnExit()
    {
        if (_isOverDistance) return;

        base.OnExit();

        _step = _nextStep;
        if (_step == 1) _isOpen = true;
        else if (_step == 0 || _step == 2) _isOpen = false;
    }
}

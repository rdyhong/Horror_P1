using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected bool _isLock = false;
    protected bool _isUsing = false;

    public virtual void OnEnter()
    {
        _isUsing = true;
    }

    public virtual void OnUse()
    {

    }

    public virtual void OnExit()
    {
        _isUsing = false;
    }
}

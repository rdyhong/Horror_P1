using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Animator _camAnimator;

    void Start()
    {
        UIMgr.Inst.Push<TitlePanel>();

        _camAnimator.enabled = true;
    }

}

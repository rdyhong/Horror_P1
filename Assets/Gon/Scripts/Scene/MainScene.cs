using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    void Start()
    {
        UIMgr.Inst.Push<PlayerBasePanel>();
    }

    void Update()
    {
        
    }
}

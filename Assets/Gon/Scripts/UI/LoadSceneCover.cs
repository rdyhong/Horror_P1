using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneCover : UIRoot
{
    [SerializeField] Slider sliderLoadingBar;
    float _curValu = 0;

    public override void Push()
    {
        base.Push();

        sliderLoadingBar.value = 0;
    }

    public override void Pop()
    {
        base.Pop();
    }

    public void SetLoadingBar(float value)
    {
        if (_curValu == value) return;

        sliderLoadingBar.value = value;
    }
}

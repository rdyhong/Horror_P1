using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscPanel : UIRoot
{
    [SerializeField] Button _btn_Debug;
    [SerializeField] Button _btnSetting;

    [SerializeField] Button _btnExit;

    [SerializeField] Slider _sliderSensitiveX;
    [SerializeField] Slider _sliderSensitiveY;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        // Debug
        _btn_Debug.gameObject.SetActive(true);
        _btn_Debug.onClick.AddListener(() => {
            
        });
        //

        _btnSetting.onClick.AddListener(() => { OnClickSetting(); });
        _btnExit.onClick.AddListener(() => { OnClickExit(); });

        _sliderSensitiveX.value = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_X);
        _sliderSensitiveY.value = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_Y);

        _sliderSensitiveX.onValueChanged.AddListener((value) => {
            PlayerPrefsHelper.SaveFltData(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_X, value);
        });
        _sliderSensitiveY.onValueChanged.AddListener((value) => {
            PlayerPrefsHelper.SaveFltData(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_Y, value);
        });
    }

    public override void Push()
    {
        base.Push();
        InputMgr.SetCursorAvtive(true);
        InputMgr.StopPlayerMove(true);
        Time.timeScale = 0;

        _sliderSensitiveX.value = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_X);
        _sliderSensitiveY.value = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_Y);
    }

    public override void Pop()
    {
        base.Pop();
        InputMgr.SetCursorAvtive(false);
        InputMgr.StopPlayerMove(false);
        Time.timeScale = 1;
    }

    void OnClickSetting()
    {

    }

    void OnClickExit()
    {
        Application.Quit();
    }
}

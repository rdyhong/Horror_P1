using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : UIRoot
{
    [SerializeField] Button _startButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(() => {
            OnClickStartButton();
        });
    }

    void OnClickStartButton()
    {
        LoadSceneMgr.LoadScene(ESceneType.Main);
    }
}

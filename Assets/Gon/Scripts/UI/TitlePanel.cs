using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : UIRoot
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Button _startButton;

    private void Awake()
    {
        _canvasGroup.alpha = 0;
        _startButton.interactable = false;
        _startButton.onClick.AddListener(() => {
            OnClickStartButton();
        });

        StartCoroutine(ShowButtonAnimCo());
    }

    void OnClickStartButton()
    {
        SceneMgr.Inst.LoadScene(ESceneType.Main);
    }

    IEnumerator ShowButtonAnimCo()
    {
        while (true)
        {
            yield return null;
            _canvasGroup.alpha = _canvasGroup.alpha + 0.2f* Time.deltaTime;
            if (_canvasGroup.alpha >= 0.7f) break;
        }

        _startButton.interactable = true;
    }
}

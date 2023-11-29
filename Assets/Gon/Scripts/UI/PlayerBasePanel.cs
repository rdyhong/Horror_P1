using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerBasePanel : UIRoot
{
    // Item Dot
    [SerializeField] Image _dotImg;

    // SubTitle
    [SerializeField] CanvasGroup _subTitleAlpha;
    [SerializeField] Text _subTitleT;

    [SerializeField] RectTransform _questAlertRt;
    [SerializeField] Text _questAlertT;
    float _questAlertBaseX;

    Queue<string> _subTitleQue = new Queue<string>();

    private void Awake()
    {
        _questAlertBaseX = _questAlertRt.anchoredPosition.x;

        _dotImg.color = new Color(1, 1, 1, 0);

        _subTitleQue.Enqueue("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        _subTitleQue.Enqueue("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        _subTitleQue.Enqueue("Nice to meet you");
        _subTitleQue.Enqueue("And you?");
        _subTitleQue.Enqueue("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        _subTitleQue.Enqueue("wqqqwwqwqwqwqwqwqwqwqwqw");
        _subTitleQue.Enqueue("gggggggasdfasfawfawfawfawefgggggggggggggwaefawfeawfawfawgggggggggfwafawfawegggggggggg");

        StartCoroutine(SubTitleQueControl());
    }

    void Update()
    {
        if(GameInstance.PlayerController.IsFindObject)
        {
            if(_dotImg.color.a < 1) _dotImg.color = new Color(1, 1, 1, _dotImg.color.a + Time.deltaTime * 3f);
        }
        else
        {
            if (_dotImg.color.a > 0) _dotImg.color = new Color(1, 1, 1, _dotImg.color.a - Time.deltaTime * 3f);
        }

        if(InputMgr.KeyHold(KeyCode.CapsLock))
        {
            _questAlertRt.anchoredPosition = Vector2.Lerp(_questAlertRt.anchoredPosition, new Vector2(0, _questAlertRt.anchoredPosition.y), 4f * Time.deltaTime);
        }
        else
        {
            _questAlertRt.anchoredPosition = Vector2.Lerp(_questAlertRt.anchoredPosition, new Vector2(_questAlertBaseX, _questAlertRt.anchoredPosition.y), 4f * Time.deltaTime);
        }
    }
    

    IEnumerator SubTitleQueControl()
    {
        _subTitleAlpha.alpha = 0;
        float alphaSpeed = 0.5f;
        float nextAlphaValue = 0;

        while (true)
        {
            yield return null;
            if (_subTitleQue.Count == 0) continue;

            _subTitleT.text = _subTitleQue.Dequeue();

            /* ----- 자막의 사이즈를 가변으로 설정하려 했는데 실패....
            float hight = 50;
            RectTransform rt = _subTitleAlpha.transform.GetComponent<RectTransform>();
            int lineCount = (int)(_subTitleT.text.Length / 20) + 1;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, hight * lineCount);

            for(int i = 1; i < lineCount; i++)
            {
                _subTitleT.text.Insert(20 * (i), "\n");
            }
            */

            bool isOpening = true;

            while (true)
            {
                yield return null;

                if (isOpening) // Open and show
                {
                    nextAlphaValue = _subTitleAlpha.alpha + (alphaSpeed * Time.deltaTime);

                    if (nextAlphaValue >= 1)
                    {
                        _subTitleAlpha.alpha = 1;
                        isOpening = false;
                        yield return new WaitForSeconds(3);
                    }
                    else
                    {
                        _subTitleAlpha.alpha = nextAlphaValue;
                    }
                }
                else // close
                {
                    nextAlphaValue = _subTitleAlpha.alpha + (-alphaSpeed * Time.deltaTime);

                    if (nextAlphaValue <= 0)
                    {
                        _subTitleAlpha.alpha = 0;
                        break;
                    }
                    else
                    {
                        _subTitleAlpha.alpha = nextAlphaValue;
                    }
                }
            }
        }
    }
}

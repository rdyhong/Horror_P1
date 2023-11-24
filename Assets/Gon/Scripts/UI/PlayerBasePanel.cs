using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasePanel : UIRoot
{
    [SerializeField] CanvasGroup _subTitleAlpha;
    [SerializeField] Text _subTitleT;

    Queue<string> _subTitleQue = new Queue<string>();

    private void Awake()
    {
        _subTitleQue.Enqueue("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        _subTitleQue.Enqueue("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        _subTitleQue.Enqueue("Nice to meet you");
        _subTitleQue.Enqueue("And you?");
        _subTitleQue.Enqueue("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        _subTitleQue.Enqueue("wqqqwwqwqwqwqwqwqwqwqwqw");
        _subTitleQue.Enqueue("gggggggasdfasfawfawfawfawefgggggggggggggwaefawfeawfawfawgggggggggfwafawfawegggggggggg");

        StartCoroutine(SubTitleQueControl());
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

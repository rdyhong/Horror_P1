using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Animator _titleAnimator;

    void Start()
    {
        StartCoroutine(TitleAnim());
    }

    IEnumerator TitleAnim()
    {
        yield return new WaitForSeconds(1f);
        _titleAnimator.Play("TitleAnim");

        yield return null;
        yield return new WaitUntil(() => _titleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        yield return new WaitForSeconds(1f);

        UIMgr.Inst.Push<TitlePanel>();
    }
}

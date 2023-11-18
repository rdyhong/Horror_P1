using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : Singleton<UIMgr>
{
    Stack<UIRoot> _stack = new Stack<UIRoot>();

    [SerializeField] Transform _normalTf;
    [SerializeField] Transform _popUpTf;

    public void Push(UIRoot root)
    {
        _stack.Peek()?.gameObject.SetActive(false);
        _stack.Push(root);
        _stack.Peek().gameObject.SetActive(true);
    }

    public void Pop()
    {
        if (_stack.Count > 0)
        {
            UIRoot root = _stack.Peek();
            if (root._isCloseBlock) return;

            root = _stack.Pop();
            root.gameObject.SetActive(false);
            root.Pop();
        }
    }

    private void Update()
    {
        if(_stack.Count > 0)
        {
            if(InputMgr.KeyDown(KeyCode.Escape))
            {
                Pop();
            }
        }
    }
}

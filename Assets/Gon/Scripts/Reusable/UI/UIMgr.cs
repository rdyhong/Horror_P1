using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUIType
{
    Normal,
    PopUp,
    Cover,
}

public class UIMgr : Singleton<UIMgr>
{
    Stack<UIRoot> _stack = new Stack<UIRoot>();
    Dictionary<string ,UIRoot> _loadedUI = new Dictionary<string, UIRoot>();
    const string _uiPath = "UI/";

    [SerializeField] Transform _normalTf;
    [SerializeField] Transform _popUpTf;
    [SerializeField] Transform _coverTf;
    [SerializeField] Transform _poolTf;

    public void Push<T>()
    {
        string uiName = typeof(T).Name;
        if (!_loadedUI.ContainsKey(uiName))
        {
            GameObject uiRootGo = Resources.Load<GameObject>(_uiPath + uiName);
            UIRoot uiRoot = Instantiate(uiRootGo).GetComponent<UIRoot>();
            
            _loadedUI[uiName] = uiRoot;
        }

        if (_stack.Count > 0)
        {
            _stack.Peek().gameObject.SetActive(false);
        }

        _stack.Push(_loadedUI[uiName]);
        _loadedUI[uiName].Push();
        _stack.Peek().gameObject.SetActive(true);

        switch (_loadedUI[uiName].uiType)
        {
            case EUIType.Normal:
                _loadedUI[uiName].transform.SetParent(_normalTf);
                break;
            case EUIType.PopUp:
                _loadedUI[uiName].transform.SetParent(_popUpTf);
                break;
            case EUIType.Cover:
                _loadedUI[uiName].transform.SetParent(_coverTf);
                break;
            default:
                DebugUtil.LogErr("UIType Err");
                break;
        }

        RectTransform rt = _loadedUI[uiName].GetComponent<RectTransform>();
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
    }

    public void Pop()
    {
        if (_stack.Count > 0)
        {
            UIRoot root = _stack.Peek();
            if (root.isCloseBlock) return;

            root = _stack.Pop();
            root.transform.SetParent(_poolTf);
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
        else if(_stack.Count == 0)
        {
            if (InputMgr.KeyDown(KeyCode.Escape))
            {
                Push<EscPanel>();
            }
        }
    }
}

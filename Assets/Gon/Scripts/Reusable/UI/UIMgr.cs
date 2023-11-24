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
    bool _isSceneLoading = false;

    List<UIRoot> _stack = new List<UIRoot>();
    Dictionary<string ,UIRoot> _loadedUI = new Dictionary<string, UIRoot>();
    const string _uiPath = "UI/";

    [SerializeField] Transform _normalTf;
    [SerializeField] Transform _popUpTf;
    [SerializeField] Transform _coverTf;
    [SerializeField] Transform _poolTf;

    public T Push<T>() where T : Object
    {
        if (_isSceneLoading) return null;

        string uiName = typeof(T).Name;
        if (!_loadedUI.ContainsKey(uiName))
        {
            GameObject uiRootGo = Resources.Load<GameObject>(_uiPath + uiName);
            UIRoot uiRoot = Instantiate(uiRootGo).GetComponent<UIRoot>();
            
            _loadedUI[uiName] = uiRoot;
        }

        _stack.Add(_loadedUI[uiName]);
        _loadedUI[uiName].Push();
        _stack[_stack.Count - 1].gameObject.SetActive(true);

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
        }

        RectTransform rt = _loadedUI[uiName].GetComponent<RectTransform>();
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
        rt.localScale = Vector3.one;
        rt.SetAsLastSibling();

        return _loadedUI[uiName] as T;
    }

    public void Pop(bool force = false)
    {
        if (_isSceneLoading) return;

        if (_stack.Count > 0)
        {
            UIRoot root = _stack[_stack.Count - 1];
            if (root.isCloseBlock && !force) return;

            root = _stack[_stack.Count - 1];
            _stack.RemoveAt(_stack.Count - 1);
            root.transform.SetParent(_poolTf);
            root.gameObject.SetActive(false);
            root.Pop();
        }
    }

    public void SetLock(bool isLock)
    {
        _isSceneLoading = isLock;
    }

    public void ClearAll()
    {
        while(true)
        {
            if(_stack.Count > 0 )
            {
                Pop(true);
            }
            else
            {
                break;
            }
        }
    }

    private void Update()
    {
        if (InputMgr.KeyDown(KeyCode.Escape))
        {
            if (SceneMgr.GetCurrentScene() != ESceneType.Main) return;

            if (_stack.Count > 0 && !_stack[_stack.Count - 1].isCloseBlock)
            {
                Pop();
            }
        }
    }
}

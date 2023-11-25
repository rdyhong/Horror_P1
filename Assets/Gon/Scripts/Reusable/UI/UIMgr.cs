using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EUIType
{
    Normal,
    PopUp,
    Cover,
}

public class UIMgr : Singleton<UIMgr>
{
    bool _isPushForceLock = false;
    bool _isPopForceLock = false;

    List<UIRoot> _openedUI = new List<UIRoot>();
    Dictionary<string ,UIRoot> _loadedUI = new Dictionary<string, UIRoot>();
    const string _uiPath = "Prefabs/UI/";

    [SerializeField] Transform _normalTf;
    [SerializeField] Transform _popUpTf;
    [SerializeField] Transform _coverTf;
    [SerializeField] Transform _poolTf;

    public T Push<T>(bool force = false) where T : Object
    {
        if (_isPushForceLock && !force) return null;

        string uiName = typeof(T).Name;
        if (!_loadedUI.ContainsKey(uiName))
        {
            GameObject uiRootGo = Resources.Load<GameObject>(_uiPath + uiName);
            UIRoot uiRoot = Instantiate(uiRootGo).GetComponent<UIRoot>();
            
            _loadedUI[uiName] = uiRoot;
        }

        _openedUI.Add(_loadedUI[uiName]);
        _loadedUI[uiName].Push();
        _openedUI[_openedUI.Count - 1].gameObject.SetActive(true);

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

    public void Pop(UIRoot root)
    {
        if (!_openedUI.Contains(root)) return;

        _openedUI.Remove(root);
        root.gameObject.SetActive(false);
        root.transform.SetParent(_poolTf);
    }

    public void PopByEsc()
    {
        if (_isPopForceLock) return;

        if (_openedUI.Count > 0)
        {
            UIRoot root = _openedUI[_openedUI.Count - 1];
            if (root._isEscLock) return;

            root.Pop();
        }
    }

    T GetPanelInOpened<T>() where T : Object
    {
        if (!IsPanelOpened<T>()) return null;
        string name = nameof(T);
        return _loadedUI[name].GetComponent<T>();
    }

    public bool IsPanelOpened<T>()
    {
        string name = nameof(T);
        if (!_loadedUI.ContainsKey(name)) return false;
        if (!_openedUI.Contains(_loadedUI[name])) return false;

        return true;
    }

    public bool IsLastPanel<T>() where T : Object
    {
        if(_openedUI.Count == 0) return false;

        if (_openedUI[_openedUI.Count - 1].GetComponent<T>() == null) return false;
        else return true;
    }

    public void ForceLockPush(bool isLock = true)
    {
        _isPushForceLock = isLock;
    }
    public void ForceLockPop(bool isLock = true)
    {
        _isPopForceLock = isLock;
    }

    public void ClearAll()
    {
        while(true)
        {
            if(_openedUI.Count > 0 )
            {
                Pop(_openedUI[0]);
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
            if (_openedUI.Count == 0) return;

            if (_openedUI[_openedUI.Count - 1]._isEscLock)
            {
                if (UIMgr.Inst.IsLastPanel<PlayerBasePanel>() && !UIMgr.Inst.IsPanelOpened<EscPanel>())
                {
                    UIMgr.Inst.Push<EscPanel>();
                    return;
                }
                return;
            }

            PopByEsc();
        }
    }
    
}

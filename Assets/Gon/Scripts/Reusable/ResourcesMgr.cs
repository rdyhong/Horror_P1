using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum EResourceType
{
    Item,
    UI,

}

public class ResourcesMgr : Singleton<ResourcesMgr>
{
    Dictionary<string, List<GameObject>> _pooledObject = new Dictionary<string, List<GameObject>>();
    Dictionary<string, List<GameObject>> _usingObject = new Dictionary<string, List<GameObject>>();

    public T Spawn<T>(EResourceType type) where T : Object
    {
        string name = typeof(T).Name;

        GameObject go;

        if (_pooledObject.ContainsKey(name))
        {
            if(_pooledObject[name].Count > 0)
            {
                go = _pooledObject[name][0];
                _pooledObject[name].Remove(go);
            }
            else
            {
                go = Instantiate(Resources.Load<GameObject>(GetResourcePath(type) + name));
            }
        }
        else
        {
            _pooledObject[name] = new List<GameObject>();
            _usingObject[name] = new List<GameObject>();
            go = Instantiate(Resources.Load<GameObject>(GetResourcePath(type) + name));
        }

        _usingObject[name].Add(go);

        return go.GetComponent<T>();
    }

    string GetResourcePath(EResourceType type)
    {
        switch(type)
        {
            case EResourceType.Item:
                return "Prefabs/Item";
            case EResourceType.UI:
                return "Prefabs/UI";
            default:
                return string.Empty;
        }
    }
}

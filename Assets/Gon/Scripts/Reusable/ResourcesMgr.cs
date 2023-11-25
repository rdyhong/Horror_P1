using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum EResourcePath
{
    Item,
    UI,

}

public class ResourcesMgr : Singleton<ResourcesMgr>
{
    Dictionary<string, Queue<GameObject>> _pooledObject = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, List<GameObject>> _usingObject = new Dictionary<string, List<GameObject>>();

    public T Spawn<T>(EResourcePath type) where T : Object
    {
        string name = typeof(T).Name;

        GameObject go;

        if (_pooledObject.ContainsKey(name))
        {
            if(_pooledObject[name].Count > 0)
            {
                go = _pooledObject[name].Dequeue();
            }
            else
            {
                go = Instantiate(Resources.Load<GameObject>(GetResourcePath(type) + name));
            }
        }
        else
        {
            _pooledObject[name] = new Queue<GameObject>();
            _usingObject[name] = new List<GameObject>();
            go = Instantiate(Resources.Load<GameObject>(GetResourcePath(type) + name));
        }

        _usingObject[name].Add(go);

        go.GetComponent<PoolObject>().Spawn();

        return go.GetComponent<T>();
    }

    public void Recycle(GameObject go)
    {
        string name = go.name;
        name.Replace("(Clone)", "");

        go.transform.position = new Vector3(0, 1000, 0);
        go.SetActive(false);

        _pooledObject[name].Enqueue(go);
        _usingObject[name].Remove(go);
    }

    string GetResourcePath(EResourcePath type)
    {
        switch(type)
        {
            case EResourcePath.Item:
                return "Prefabs/Item/";
            case EResourcePath.UI:
                return "Prefabs/UI/";
            default:
                return string.Empty;
        }
    }
}

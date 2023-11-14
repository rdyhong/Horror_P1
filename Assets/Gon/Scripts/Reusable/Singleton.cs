using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static object s_syncObject = new object();
    private static T s_instance = null;
    private static bool s_isInit = true;
    private static Transform s_parentTf = null;
    public static T Inst
    {
        get
        {
            if (s_instance == null)
            {
                lock (s_syncObject)
                {
                    s_instance = FindObjectOfType<T>();
                    if (s_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        s_instance = obj.AddComponent<T>();
                    }
                }
            }
            return s_instance;
        }
    }

    protected virtual void Awake()
    {
        if (null == s_instance)
        {
            s_instance = this as T;
        }
        SetParent();

        DebugUtil.Log($"Singleton Awake ({this.name})");
    }

    void SetParent()
    {
        if(s_isInit)
        {
            s_isInit = false;
            s_parentTf = new GameObject(GameDef.SINGLETON_PARENT_NAME).transform;
            transform.SetParent(s_parentTf);
            DontDestroyOnLoad(s_parentTf);
        }
        else
        {
            transform.SetParent(s_parentTf);
        }
    }
}

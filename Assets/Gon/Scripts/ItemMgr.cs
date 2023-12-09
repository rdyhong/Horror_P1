using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemMgr : Singleton<ItemMgr>
{
    public List<Item> _onFieldItems = new List<Item>();

    List<int> _ownedItems = new List<int>();
    int _onHandItem = -1;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    void Init()
    {
        // Get saved owned item
        _ownedItems = PlayerPrefsHelper.GetIntArr(PlayerPrefsHelper.PPKEY_OWNED_ITEM).ToList();
        _onHandItem = PlayerPrefsHelper.GetInt(PlayerPrefsHelper.PPKEY_ON_HAND_ITEM);
    }

    public void AddItem(int idx)
    {
        _ownedItems.Add(idx);
    }

    public T GetItemObject<T>() where T : Object
    {


        return ResourcesMgr.Inst.Spawn<T>(EResourcePath.Item);
    }
}

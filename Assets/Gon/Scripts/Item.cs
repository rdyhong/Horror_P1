using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data_Item
{
    public string name = string.Empty;
    public string iconPath = string.Empty;
}

public class Item : MonoBehaviour
{
    [SerializeField] Transform _markPos;
    [SerializeField] Data_Item _data;

    ItemMark _itemMark;
    Rigidbody _rb;
    MeshCollider _col;

    bool _isPlayerOwned = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<MeshCollider>();

        _itemMark = ResourcesMgr.Inst.Spawn<ItemMark>(EResourcePath.UI);
        _itemMark.SetMark(_markPos == null ? transform : _markPos);
    }

    public virtual Data_Item GetData()
    {
        return _data;
    }

    // 획득
    public virtual Data_Item Obtain(Transform parent)
    {
        if (_isPlayerOwned) return null;
        _isPlayerOwned = true;

        _rb.isKinematic = true;
        _col.enabled = false;

        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

        DebugUtil.LogAssert(_data != null, "Item data is Null");
        DebugUtil.Log($"Item Gain ({name})");
        return _data;
    }

    // 버림
    public virtual void Dump()
    {
        _isPlayerOwned = false;

        transform.SetParent(null);
        _rb.isKinematic = false;
        _col.enabled = true;
    }

}

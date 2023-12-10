using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableObject, IPoolObject
{
    [SerializeField] int Index = -1;
    [SerializeField] Data_Item _data;
    [SerializeField] Transform _markPos;

    Transform _FollowTf;
    ItemMark _itemMark;
    Rigidbody _rb;
    BoxCollider _col;

    bool _isPlayerOwned = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();

        _itemMark = ResourcesMgr.Inst.Spawn<ItemMark>(EResourcePath.UI);
        _itemMark.SetMark(_markPos == null ? transform : _markPos);
    }
    private void FixedUpdate()
    {
        if (_FollowTf != null)
        {
            transform.position = Vector3.Lerp(transform.position, _FollowTf.position, 20f * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _FollowTf.rotation, 20f * Time.fixedDeltaTime);
        }
    }

    public Data_Item GetData()
    {
        return _data;
    }

    // 획득
    public virtual Data_Item Obtain(Transform parent)
    {
        if (_isPlayerOwned) return null;
        _isPlayerOwned = true;

        _col.enabled = false;
        transform.SetParent(null);
        _FollowTf = parent;

        DebugUtil.LogAssert(_data != null, "Item data is Null");
        DebugUtil.Log($"Item Gain ({name})");
        return _data;
    }

    // 버림
    public virtual void Dump()
    {
        _isPlayerOwned = false;
        _FollowTf = null;
        transform.SetParent(null);
        _rb.isKinematic = false;
        _col.enabled = true;
    }

    public void Spawn()
    {
        throw new NotImplementedException();
    }

    public void Recycle()
    {
        ResourcesMgr.Inst.Recycle(this.gameObject);
    }
}

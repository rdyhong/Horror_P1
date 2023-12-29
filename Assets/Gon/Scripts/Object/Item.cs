using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableObject, IPoolObject
{
    [SerializeField] int Index = -1;
    [SerializeField] Item_Data _data;
    [SerializeField] Transform _markPos;

    Transform _FollowTf;
    ItemMark _itemMark;
    BoxCollider _col;

    bool _isPlayerOwned = false;

    private void Awake()
    {
        _col = GetComponent<BoxCollider>();

        _itemMark = ResourcesMgr.Inst.Spawn<ItemMark>(EResourcePath.UI);
        _itemMark.SetMark(_markPos == null ? transform : _markPos);
    }
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (_FollowTf != null)
        {
            transform.position = Vector3.Lerp(transform.position, _FollowTf.position, 50f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _FollowTf.rotation, 50f * Time.deltaTime);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Obtain();
    }
    public override void OnUse()
    {
        base.OnUse();

    }
    public override void OnExit()
    {
        base.OnExit();

    }

    // 획득
    public virtual Item_Data Obtain()
    {
        if (_isPlayerOwned) return null;
        _isPlayerOwned = true;

        _col.enabled = false;
        _markPos.gameObject.SetActive(false);
        //transform.SetParent(null);
        //_FollowTf = parent;
        _FollowTf = GameInstance.PlayerController.OnHandTf;
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
        _col.enabled = true;
    }

    public void Spawn()
    {
        throw new NotImplementedException();
    }

    public void Recycle()
    {
    }
}

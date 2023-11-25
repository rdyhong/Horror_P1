using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMark : PoolObject
{
    [SerializeField] Transform _baseMarkTf;

    float _markMoveSpeed = 0.1f;
    float _markTopYPos = 0f;
    float _markDownYPos = 0f;

    public void SetMark(Transform tf)
    {
        transform.parent = tf;
        transform.localPosition = Vector3.zero;
        _baseMarkTf.localPosition = Vector3.zero;

        _markTopYPos = transform.position.y + 0.01f;
        _markDownYPos = transform.position.y - 0.01f;

        StartCoroutine(BaseCycleCo());
    }

    IEnumerator BaseCycleCo()
    {
        while(true)
        {
            yield return null;
            _baseMarkTf.forward = Camera.main.transform.forward;
            _baseMarkTf.transform.position = transform.position + (Camera.main.transform.position - _baseMarkTf.transform.position).normalized * 0.2f + (Camera.main.transform.up * 0.2f);
            
            /*
            Vector3 nextPos;

            if (isUp)
            {
                nextPos = _baseMarkTf.position + new Vector3(0, 0, _markTopYPos);
                if (_markTopYPos - nextPos.y < 0.001f) isUp = false;
            }
            else
            {
                nextPos = _baseMarkTf.position + new Vector3(0, 0, -_markTopYPos);
                if (nextPos.y - _markDownYPos < 0.001f) isUp = false;
            }
            
            _baseMarkTf.position = Vector3.Lerp(_baseMarkTf.position, nextPos, 0.1f);
            */
        }
    }
}

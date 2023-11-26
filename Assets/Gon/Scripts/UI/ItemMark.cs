using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMark : PoolObject
{
    [SerializeField] CanvasGroup _canvasGroup;
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
        Transform camTf = Camera.main.transform;
        float maxDistance = 4;

        while (true)
        {
            yield return null;
            float distance = Vector3.Distance(camTf.position, transform.position);
            if (distance < maxDistance)
            {
                _baseMarkTf.forward = camTf.forward;
                _baseMarkTf.transform.position = transform.position + (camTf.position - _baseMarkTf.transform.position).normalized * 0.2f + (Camera.main.transform.up * 0.2f);
                _canvasGroup.alpha = ((maxDistance - distance) * 1.5f) / maxDistance;
            }
            else
            {
                _canvasGroup.alpha = 0;
            }
        }
    }
}
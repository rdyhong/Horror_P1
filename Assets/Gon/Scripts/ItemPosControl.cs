using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ItemPosControl : MonoBehaviour
{
    RaycastHit _hit;
    Vector3 basePos;

    private void Awake()
    {
        basePos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward.normalized, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out _hit, 0.5f))
        {
            float targetZ = basePos.z - Vector3.Distance(_hit.point, transform.position);

            if (targetZ > 0)
            {
                Vector3 nextPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - targetZ);
                if (nextPos.z < 0) nextPos.z = 0;
                transform.localPosition = Vector3.Lerp(transform.localPosition, nextPos, 10f * Time.fixedDeltaTime);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, basePos, 10f * Time.fixedDeltaTime);
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, basePos, 10f * Time.fixedDeltaTime);
        }
    }
}

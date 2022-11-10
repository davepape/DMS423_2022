/* Drags an object around with the mouse.  "layermask" controls which other objects the moving object can be placed on. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabber : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] LayerMask layermask;

    void OnMouseDrag()
    {
        if (camera)
        {
            Ray r = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r.origin, r.direction, out hit, Mathf.Infinity, layermask))
            {
                transform.position = hit.point;
            }

        }
    }
}

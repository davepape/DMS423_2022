/* Point at an object & click to change its color to blue */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(r.origin, r.direction * 10, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(r.origin, r.direction, out hit, 100.0f))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}

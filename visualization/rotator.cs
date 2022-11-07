using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(Input.GetAxis("Vertical"),0,0), Space.World);
        transform.Rotate(new Vector3(0,-Input.GetAxis("Horizontal"),0));
    }
}

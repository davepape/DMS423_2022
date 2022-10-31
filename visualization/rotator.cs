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
        transform.Rotate(new Vector3(Input.GetAxis("Vertical"),-Input.GetAxis("Horizontal"),0), Space.World);
    }
}

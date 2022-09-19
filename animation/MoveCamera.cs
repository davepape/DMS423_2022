/* MoveCamera.cs
 by Dave Pape, 2022

 Code from my example Unity version of McLaren's "Lines Vertical".
 This is meant to move the camera continuously downward, over the
 strip of virtual film, looping back to the start when it reaches
 "endY".
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float endY = -100.0f;

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed);
        if (transform.position.y < endY)
            transform.position = Vector3.zero;
    }
}

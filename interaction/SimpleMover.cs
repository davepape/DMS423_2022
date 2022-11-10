using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class SimpleMover : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 180.0F;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

        // Move forward / backward
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(transform.forward * curSpeed);
    }
}

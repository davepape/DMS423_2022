/* Teleports the player to another location when they enter a collider */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private string playerTag = "Player";

    void OnTriggerEnter(Collider c)
    {
        if ((c.tag == playerTag) && (destination))
        {
            c.transform.position = destination.position;
            Physics.SyncTransforms();
        }
    }
}

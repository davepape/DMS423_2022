using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solarsystem : MonoBehaviour
{
    [SerializeField] private float gravityConstant = 1.0f;

    public float G { get => gravityConstant; }
}

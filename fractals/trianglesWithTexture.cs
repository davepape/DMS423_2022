// trianglesWithTexture.cs
//
// Create a Unity mesh that consists of 4 points and 2 triangles,
// with UV coordinates and normals.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class trianglesWithTexture : MonoBehaviour
{
    [SerializeField] private Material material;

    void Start()
        {
        GetComponent<Renderer>().material = material;
        Vector3[] newVertices = new Vector3[] { new Vector3(-2, 0, -1),
                                                new Vector3(0, 1, -1),
                                                new Vector3(1, 0, -1),
                                                new Vector3(0, -1, -1) };
        Vector2[] newUV = new Vector2[] { new Vector2(0, 0.5f),
                                          new Vector2(0.5f, 1),
                                          new Vector2(1, 0.5f),
                                          new Vector2(0.5f, 0) };
        int[] newTriangles = new int[] { 0, 1, 2,  0, 2, 3 };
        Mesh m = GetComponent<MeshFilter>().mesh;
        m.Clear();
        m.vertices = newVertices;
        m.uv = newUV;
        m.triangles = newTriangles;
        m.RecalculateNormals();
        }
}

// midpointland.cs
//
// Creates a fractal landscape via midpoint-subdivision.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class midpointland : MonoBehaviour
{
    [SerializeField] private int numLevels = 3;

    void Start ()
        {
        Vector3[][] verts0;
        int size = 2, size0;
        float yscale = 10f;
        Vector3[][] verts = newArray(2);
        verts[0][0] = new Vector3(-10,0,-10);
        verts[1][0] = new Vector3(10,0,-10);
        verts[0][1] = new Vector3(-10,0,10);
        verts[1][1] = new Vector3(10,0,10);
        for (int level=0; level < numLevels; level++)
            {
            verts0 = verts;
            size0 = size;
            size = size * 2 - 1;
            verts = newArray(size);
            yscale /= 1.8f;
            for (int i=0; i < size0; i++)
                for (int j=0; j < size0; j++)
                    {
                    verts[i*2][j*2] = verts0[i][j];
                    }
            for (int j=0; j < size0; j++)
                for (int i=0; i < size0-1; i++)
                    {
                    Vector3 v = (verts0[i][j] + verts0[i+1][j])/2;
                    v.y += heightOffset(yscale);
                    verts[i*2+1][j*2] = v;
                    }
            for (int i=0; i < size0; i++)
                for (int j=0; j < size0-1; j++)
                    {
                    Vector3 v = (verts0[i][j] + verts0[i][j+1])/2;
                    v.y += heightOffset(yscale);
                    verts[i*2][j*2+1] = v;
                    }
            for (int i=0; i < size0-1; i++)
                for (int j=0; j < size0-1; j++)
                    {
                    Vector3 v = (verts0[i][j] + verts0[i+1][j] + verts0[i][j+1] + verts0[i+1][j+1])/4;
                    v.y += heightOffset(yscale);
                    verts[i*2+1][j*2+1] = v;
                    }
            }

        Vector3[] newVerts = new Vector3[size*size];
        for (int i=0; i < size; i++)
            for (int j=0; j < size; j++)
                {
                newVerts[j*size+i] = verts[i][j];
                }

        Vector2[] newUVs = new Vector2[size*size];
        for (int j=0; j < size; j++)
            {
            for (int i=0; i < size; i++)
                {
                float u = i / (size-1.0f);
                float v = j / (size-1.0f);
                newUVs[j*size+i] = new Vector2(u,v);
                }
            }

        int[] newTris = new int[(size-1)*(size-1)*2*3];
        int index = 0;
        for (int j=0; j < size-1; j++)
            for (int i=0; i < size-1; i++)
                {
                newTris[index++] = i + j*size;
                newTris[index++] = i + (j+1)*size;
                newTris[index++] = (i+1) + j*size;
                newTris[index++] = i + (j+1)*size;
                newTris[index++] = (i+1) + (j+1)*size;
                newTris[index++] = (i+1) + j*size;
                }

        Mesh m = GetComponent<MeshFilter>().mesh;
        m.Clear();
        m.vertices = newVerts;
        m.uv = newUVs;
        m.triangles = newTris;
	    m.RecalculateNormals();
        }
	

    Vector3[][] newArray(int size)
        {
        Vector3[][] v = new Vector3[size][];
        for (int i=0; i < size; i++)
            v[i] = new Vector3[size];
        return v;
        }

    float heightOffset(float yscale)
        {
        return (Random.Range(-yscale,yscale) + Random.Range(-yscale,yscale) + Random.Range(-yscale,yscale))/3f;
        }


    void OnDrawGizmos()
        {
        Mesh m = GetComponent<MeshFilter>().sharedMesh;
        if ((m) && (m.vertices.Length > 0))
            {
            Gizmos.matrix = transform.localToWorldMatrix; 
            for (int i=0; i < m.vertices.Length; i++)
                Gizmos.DrawSphere(m.vertices[i], 0.05f);
            Gizmos.DrawWireMesh(m);
            }
        }
}

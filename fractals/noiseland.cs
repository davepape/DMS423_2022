// noiseland.cs
//
// Creates a fractal landscape using filtered Perlin noise.
// A gridded heightfield mesh is created, where the elevation
// of each vertex (height() function) is the sum of several
// frequencies of Mathf.PerlinNoise().
// A texture map is also created, where the color is based on
// the height at a texel's location, with some randomization.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class noiseland2 : MonoBehaviour
{
    [SerializeField] private int rows=10, cols=10;
    [SerializeField] private float minX=-1, maxX=1, minZ=-1, maxZ=1;

    [SerializeField] private int tex_width=256;
    [SerializeField] private int tex_height=256;

    void Start ()
        {
        generateGeometry();
        generateTexture();
        }

    void generateGeometry()
        {
        Vector3[] newVerts;
        newVerts = new Vector3[rows*cols];

        for (int j=0; j < rows; j++)
            {
            for (int i=0; i < cols; i++)
                {
                float x = Mathf.Lerp(minX, maxX, i / (cols-1.0f));
                float z = Mathf.Lerp(minZ, maxZ, j / (rows-1.0f));
                float y = height(x,z);
                newVerts[j*cols+i] = new Vector3(x,y,z);
                }
            }

        Vector2[] newUVs = new Vector2[rows*cols];
        for (int j=0; j < rows; j++)
            {
            for (int i=0; i < cols; i++)
                {
                float u = i / (cols-1.0f);
                float v = j / (rows-1.0f);
                newUVs[j*cols+i] = new Vector2(u,v);
                }
            }

        int[] newTris = new int[(cols-1)*(rows-1)*2*3];
        int index=0;
        for (int j=0; j < rows-1; j++)
            {
            for (int i=0; i < cols-1; i++)
                {
                newTris[index++] = j*cols+i;
                newTris[index++] = j*cols+i + cols;
                newTris[index++] = j*cols+i + 1;

                newTris[index++] = j*cols+i + cols;
                newTris[index++] = j*cols+i + cols+1;
                newTris[index++] = j*cols+i + 1;
                }
            }

        Mesh m = GetComponent<MeshFilter>().mesh;
        m.Clear();
        m.vertices = newVerts;
        m.uv = newUVs;
        m.triangles = newTris;
        m.RecalculateNormals();
        }

    float height(float x, float y)
        {
        x -= minX;
        y -= minZ;
        float h = 0;
        float f = 1, s = 1;
        for (int i=0; i < 8; i++)
            {
            h += s * Mathf.PerlinNoise(f*x, f*y);
            f *= 2f;
            s /= 3f;
            }
        return h;
        }

    void generateTexture()
        {
        Texture2D tex = new Texture2D(tex_width,tex_height);
        Color32[] colors = new Color32[tex_width*tex_height];
        GetComponent<Renderer>().material.mainTexture = tex;
        int index = 0;
        for (int j=0; j < tex_height; j++)
            for (int i=0; i < tex_width; i++)
                {
                float x = Mathf.Lerp(minX, maxX, i/(tex_width-1f));
                float z = Mathf.Lerp(minZ, maxZ, j/(tex_height-1f));
                float h = height(x,z);
                if (h < 0.5)
                    colors[index] = Color.blue;
                else if (h < 0.8)
                    colors[index] = Random.ColorHSV(0.3f,0.35f,0.7f,1f,0.8f,0.9f);
                else if (h < 0.95)
                    colors[index] = Random.ColorHSV(0.15f,0.2f,0.7f,0.8f,0.5f,0.75f);
                else
                    colors[index] = Random.ColorHSV(0f,1f,0f,0.1f,0.9f,1f);
                index++;
                }
        tex.SetPixels32(colors);
        tex.Apply();
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

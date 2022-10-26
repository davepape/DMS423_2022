// mandelbrot1.cs
//
// Creates a procedural texture consisting of an image of the
// Mandelbrot set.  Calculations are done one iteration per
// Update(), to produce an animated texture.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Numerics = System.Numerics;

public class mandelbrot1 : MonoBehaviour
{
    [SerializeField] private int width=32;
    [SerializeField] private int height=32;
    [SerializeField] private int maxIter=32;
    [SerializeField] private Vector2 min = new Vector2(-1,-1);
    [SerializeField] private Vector2 max = new Vector2(1,1);
    private Texture2D texture;
    private Color32[] pixels;
    private Numerics.Complex[] mandelZ;
    private Numerics.Complex[] mandelC;
    private int iter=0;

    void Start()
        {
        texture = new Texture2D(width,height);
        pixels = new Color32[width*height];
        mandelZ = new Numerics.Complex[width*height];
        mandelC = new Numerics.Complex[width*height];
        for (int j=0; j < height; j++)
            {
            for (int i=0; i < width; i++)
                {
                float x = Mathf.Lerp(min.x, max.x, i/(float)(width-1));
                float y = Mathf.Lerp(min.y, max.y, j/(float)(height-1));
                mandelC[i+j*width] = new Numerics.Complex(x,y);
                mandelZ[i+j*width] = mandelC[i+j*width];
                pixels[i+j*width] = Colorize(iter);
                }
            }
        texture.SetPixels32(pixels);
        GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();
        }

    void Update()
    {
        if (iter < maxIter)
            {
            iter++;
            for (int j=0; j < height; j++)
                {
                for (int i=0; i < width; i++)
                    {
                        int index = i+j*width;
                        if (mandelZ[index].Magnitude <= 2)
                            {
                                mandelZ[index] = mandelZ[index]*mandelZ[index] + mandelC[index];
                                if (mandelZ[index].Magnitude > 2)
                                    pixels[index] = Colorize(iter);
                            }
                    }
                }
            texture.SetPixels32(pixels);
            texture.Apply();
            }
    }


    Color Colorize(int i)
    	{
        float fraction = i/((float)maxIter);
    	if (i >= maxIter)
    		return Color.white;
        else if (fraction > 0.5f)
            return Color.Lerp(Color.green, Color.blue, 2*(fraction-0.5f));
        else if (fraction > 0.25f)
            return Color.Lerp(Color.red, Color.green, 4*(fraction-0.25f));
    	else if (fraction > 0)
    		return Color.Lerp(Color.yellow, Color.red, 4*fraction);
    	else
    		return Color.black;
    	}

}

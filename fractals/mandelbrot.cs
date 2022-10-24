// mandelbrot.cs
//
// Creates a procedural texture consisting of an image of the
// Mandelbrot set.  For each texel, the function Mandelbrot()
// estimates whether the point is in the Mandelbrot set, and if
// not uses the number of iterations taken to compute a color.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Numerics = System.Numerics;

public class mandelbrot : MonoBehaviour
{
    [SerializeField] private int width=32;
    [SerializeField] private int height=32;
    [SerializeField] private int maxIter=32;

    void Start()
        {
        Texture2D texture = new Texture2D(width,height);
        var pixels = new Color32[width*height];
        for (int j=0; j < height; j++)
            {
            for (int i=0; i < width; i++)
                {
                pixels[i+j*width] = Mandelbrot(Mathf.Lerp(-0.5f,-0.25f,i/(float)width),
                							   Mathf.Lerp(0.5f,0.75f,j/(float)height));
                }
            }
        texture.SetPixels32(pixels);
        GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();
        }

    Color Mandelbrot(float x, float y)
    	{
    	Numerics.Complex c = new Numerics.Complex(x,y);
    	Numerics.Complex z = c;
    	for (int iter=0; iter < maxIter; iter++)
    		{
    		z = z * z + c;
    		if (z.Magnitude > 2)
    			return Colorize(iter);
    		}
    	return Colorize(maxIter);
    	}

    Color Colorize(int i)
    	{
    	if (i >= maxIter)
    		return Color.white;
    	else if (i > maxIter/2)
    		return Color.Lerp(Color.red, Color.green, 2*i/((float)maxIter)-1);
    	else if (i > 0)
    		return Color.Lerp(Color.yellow, Color.red, 2*i/((float)maxIter));
    	else
    		return Color.black;
    	}

    void Update()
        {
        }
}

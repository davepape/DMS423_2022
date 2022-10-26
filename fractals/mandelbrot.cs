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
    [SerializeField] private Vector2 min = new Vector2(-1,-1);
    [SerializeField] private Vector2 max = new Vector2(1,1);

    void Start()
        {
        Texture2D texture = new Texture2D(width,height);
        var pixels = new Color32[width*height];
        for (int j=0; j < height; j++)
            {
            for (int i=0; i < width; i++)
                {
                pixels[i+j*width] = Mandelbrot(Mathf.Lerp(min.x, max.x, i/(float)width),
                                               Mathf.Lerp(min.y, max.y, j/(float)height));
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

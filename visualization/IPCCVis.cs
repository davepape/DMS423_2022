using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IPCCVis : MonoBehaviour
{
    [SerializeField] private TextAsset[] datafile;
    [SerializeField] private float maxValue = 1;
    private Texture2D[] texture;
    private int currentTexture=0;
    private float nextUpdate = 0;

    void Start()
    {
        texture = new Texture2D[datafile.Length];
        var lineseparators = new string[] { "\n", "\r\n", "\r" };
        for (int i = 0; i < datafile.Length; i++)
        {
            string[] lines = datafile[i].text.Split(lineseparators, StringSplitOptions.None);
            texture[i] = CreateTexture(lines[12..]);
        }
    }

    void Update()
    {
        if (Time.time > nextUpdate)
        {
            currentTexture = (currentTexture + 1) % texture.Length;
            GetComponent<Renderer>().material.mainTexture = texture[currentTexture];
            nextUpdate = Time.time + 0.25f;
        }
    }

    private Texture2D CreateTexture(string[] lines)
    {
        Texture2D texture = new Texture2D(360, 180);
        var initColors = new Color32[360 * 180];
        Color blank = new Color(0, 0, 0, 0);
        for (int i = 0; i < 360 * 180; i++)
            initColors[i] = blank;
        texture.SetPixels32(initColors);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length > 2)
            {
                int texCol = int.Parse(tokens[0]);
                int texRow = int.Parse(tokens[1]);
                float v = float.Parse(tokens[2]);
                texture.SetPixel(texCol+180, texRow+90, TransferFunction(v));
            }
        }
        texture.Apply();
        return texture;
    }

    private Color TransferFunction(float data)
    {
        Color color;
        Color start = Color.blue, end=Color.red;
        start[3] = 0.25f;
        color = Color.Lerp(start, end, data / maxValue);
        return color;
    }

}

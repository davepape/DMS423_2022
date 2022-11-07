using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class quakeData : MonoBehaviour
{
    public string title="default";
    public TextMeshProUGUI textGui;
    private Material mat;

    void Start()
    {
        mat = GetComponentInChildren<Renderer>().material;
    }

    void OnMouseDown()
    {
        print(title);
        textGui.text = title;
    }

    void OnMouseEnter()
    {
        mat.color = Color.red;
    }

    void OnMouseExit()
    {
        mat.color = Color.white;
    }
}

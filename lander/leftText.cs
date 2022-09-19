using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class leftText : MonoBehaviour
{
    [SerializeField] private GameObject landerObj;
    private lander landerScript;
    private TextMeshProUGUI textGui;

    void Start()
        {
        landerScript = landerObj.GetComponent<lander>();
        textGui = GetComponent<TextMeshProUGUI>();
        }

    void Update()
        {
        int score = landerScript.Score;
        float time = Time.time;
        float fuel = landerScript.Fuel;
        string msg = $"Score:  {score}\nTime:   {time:0.#}\nFuel:   {fuel:0.#}";
        textGui.text = msg;
        }
}

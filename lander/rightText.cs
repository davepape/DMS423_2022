using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rightText : MonoBehaviour
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
        float altitude = landerScript.Altitude();
        float horizspeed = landerScript.Velocity.x;
        float vertspeed = landerScript.Velocity.y;
        string msg = $"Altitude:          {altitude:0.#}\nHorizontal speed:  {horizspeed:0.#}\nVertical speed:    {vertspeed:0.#}";
        textGui.text = msg;
        }
}

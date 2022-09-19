using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketFlame : MonoBehaviour
{
    [SerializeField] private GameObject landerObj;
    private lander landerScript;
    private AudioSource soundeffect;

    void Start()
        {
        landerScript = landerObj.GetComponent<lander>();
        soundeffect = GetComponent<AudioSource>();
        soundeffect.loop = true;
        soundeffect.Play();
        }

    void Update()
        {
        float thrust = landerScript.ThrustFraction;
        if (!landerScript.Playing)
            thrust = 0f;
        soundeffect.volume = thrust;
        Vector3 s;
        if (Time.frameCount % 2 == 0)
            s = new Vector3(thrust, thrust, thrust);
        else
            s = Vector3.zero;
        transform.localScale = s;
        }
}

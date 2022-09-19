/*  arabesque.cs
  A Unity implementation of the "Arabesque" example code from John Whitney's
  "Digital Harmony: On the Complementarity of Music and Visual Art", page 136.

  The only notable change I've made to Whitney's algorithm is in the timing -
  rather than building on a number of frames to compute (9 in his example),
  I specify a "duration" in seconds for the animation, and compute the
  "time" variable based on that and Unity's Time.time, also allowing it to
  repeat infinitely.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arabesque : MonoBehaviour
{
    [SerializeField] private int npoints = 360;
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private float stepstart = 0.0f;
    [SerializeField] private float stepend = 1.0f/60.0f;
    [SerializeField] private float radius = 60.0f;
    [SerializeField] private float xcenter = 140.0f;
    [SerializeField] private float ycenter = 96.0f;
    [SerializeField] private GameObject prefab;
    private GameObject[] primitives;

    void Start()
    {
        primitives = new GameObject[npoints];
        for (int i=0; i < npoints; i++)
        {
            primitives[i] = Instantiate(prefab, transform);
        }
    }

    void Update()
    {
        float time = (Time.time % duration) / duration;
        float step = stepstart + (time * (stepend - stepstart));
        for (int point=0; point < npoints; point++)
        {
            float a = -90.0f + 360.0f * point / npoints;
            float r = 3 * radius;
            float x = Mathf.Cos(a * Mathf.Deg2Rad) * radius + point * step * r;
            x = xcenter - (r / 2) + (x + (r / 2)) % r;
            float y = ycenter + Mathf.Sin(a * Mathf.Deg2Rad) * radius;
            primitives[point].transform.localPosition = new Vector3(x, y, 0);
        }
    }
}

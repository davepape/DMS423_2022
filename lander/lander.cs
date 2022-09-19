using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lander : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 gravity = new Vector3(0f, -1f, 0f);
    [SerializeField] private float maxThrust = 1f;
    [SerializeField] private float turnRate = 90f;
    [SerializeField] private float fuel = 100f;
    [SerializeField] private float thrustLevel = 0f;
    [SerializeField] private int numRects = 4;
    private Rect[] terrain;
    [SerializeField] private int score = 0;
    [SerializeField] private bool playing = true;
    [SerializeField] private GameObject endgameTextObj;

    public Vector3 Velocity { get => velocity; }
    public int Score { get => score; }
    public float Fuel { get => fuel; }
    public float ThrustFraction { get => thrustLevel/maxThrust; }
    public bool Playing { get => playing; }

    void Start()
        {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        terrain = new Rect[numRects];
        float viewWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        float width = viewWidth / numRects;
        for (int i=0; i < terrain.Length; i++)
            {
            float x = -viewWidth/2 + width*i;
            terrain[i] = new Rect(x,-100,width,Random.value*800+110);
            makeBox(terrain[i]);
            }
        }

    void makeBox(Rect r)
        {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
        cube.transform.localScale = new Vector3(r.width, r.height, 1f);
        cube.transform.localPosition = new Vector3(r.xMin+r.width/2f, r.yMin+r.height/2f, 0);
        }


    void Update()
        {
        if (playing)
            {
            doControls();
            doPhysics();

            if (Altitude() <= 0f)
                {
                if (velocity.magnitude > 50.0f)
                    {
                    endgameTextObj.GetComponent<TextMeshProUGUI>().text = "You crashed!";
                    GameObject.Find("CrashSound").GetComponent<AudioSource>().Play();
                    }
                else
                    {
                    endgameTextObj.GetComponent<TextMeshProUGUI>().text = "Safe landing!";
                    Vector3 pos = transform.localPosition;
                    pos.y -= Altitude();
                    transform.localPosition = pos;
                    score += 250;
                    }
                playing = false;
                }
            }
        }

    void doControls()
        {
        float orientation = transform.localEulerAngles.z;
        orientation += -Input.GetAxis("Horizontal") * turnRate * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0f, 0f, orientation);
        
        thrustLevel = Input.GetAxis("Vertical") * maxThrust;
        if ((thrustLevel < 0) || (fuel <= 0))
            thrustLevel = 0;
        fuel -= thrustLevel * Time.deltaTime;
        if (fuel < 0)
            fuel = 0;
        }

    void doPhysics()
        {
        Vector3 thrust = thrustLevel * transform.up;
        velocity += thrust * Time.deltaTime;
        velocity += gravity * Time.deltaTime;
    
        Vector3 position = transform.localPosition;
        position += velocity * Time.deltaTime;
        transform.localPosition = position;
        }

    public float Altitude()
        {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        for (int i=0; i < terrain.Length; i++)
            {
            if ((x >= terrain[i].xMin) && (x <= terrain[i].xMax))
                return y - terrain[i].yMax;
            }
        return y;
        }
}

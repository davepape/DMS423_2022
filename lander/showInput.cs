// Display the state of a few example Unity Input values
// This script should be attached to a text object.
// It uses the "Jump" and "Cancel" buttons to switch between very slow framerate & 60 fps, so that the behavior of GetButtonDown & GetButtonUp can be seen.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showInput : MonoBehaviour
    {
    private TextMeshProUGUI textGui;

    void Start()
      {
      QualitySettings.vSyncCount = 0;
      textGui = GetComponent<TextMeshProUGUI>();    
      }

    void Update()
      {
      string msg = "";
      msg += "Fire1: " + Input.GetButton("Fire1") + "\n";
      msg += "Fire1 down: " + Input.GetButtonDown("Fire1") + "\n";
      msg += "Fire1 up: " + Input.GetButtonUp("Fire1") + "\n";
      msg += "Vertical: " + Input.GetAxis("Vertical") + "\n";
      msg += "Horizontal: " + Input.GetAxis("Horizontal") + "\n";
      msg += "Mouse X: " + Input.GetAxis("Mouse X") + "\n";
      msg += "Mouse Y: " + Input.GetAxis("Mouse Y") + "\n";
      msg += "Mouse pos: " + Input.mousePosition + "\n";
      textGui.text = msg;

      if (Input.GetButtonDown("Jump")) Application.targetFrameRate = 1;
      if (Input.GetButtonDown("Cancel")) Application.targetFrameRate = 60;
      }
}

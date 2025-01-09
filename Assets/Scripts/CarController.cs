using UnityEngine;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.UI; 

using UnityEngine;
using UnityEngine.UI; // For Text UI
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public int GearValue = 0; // 0 for neutral, 1-5 for forward gears, -1 for reverse
    public WheelCollider[] wheels = new WheelCollider[4];
    public InputActionReference trigger;
    public InputActionReference BreakBTN;
    public XRKnob knob;
    public bool isPressed = false;
    public float motorTorque;
    public float breakTorque;
    public float steeringMax;
    public Text gearDisplayText; // UI Text for displaying gear changes
    private Coroutine gearDisplayCoroutine;
    public GetYRotation GearVal;

    void Start()
    {
        
    }

    void Update()
    {
        GearValue=GearVal.mappedValue;
        Debug.Log("Val" + GearValue);
        // Set motorTorque and brakeTorque based on the gear
        switch (GearValue)
        {
            case 1: // Park
                motorTorque = 0;
                breakTorque = 100;
                gearDisplayText.text="Park";
                break;
            case 2: //Neutral
                motorTorque = 0;
                breakTorque = 100;
                gearDisplayText.text="Neutral";

                break;
            case 3: // Drive
                motorTorque = 400;
                breakTorque = 250;
                gearDisplayText.text="Drive";

                break;
            case 4: // Reverse
                motorTorque = -100;
                breakTorque = 100;
                gearDisplayText.text="Reverse";

                break;
        }

        // Acceleration logic
        if (trigger.action.WasPressedThisFrame())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0;
                wheels[i].motorTorque = motorTorque;
            }
        }

        // Brake logic
        if (trigger.action.WasReleasedThisFrame())
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = breakTorque;
            }
        }
        if(BreakBTN.action.WasPressedThisFrame()){
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = breakTorque+2000;
            }
        }

        // Steering logic
        for (int i = 0; i < wheels.Length - 2; i++)
        {
            wheels[i].steerAngle = -(knob.value - 0.5f) * 120f; // Adjusting steering sensitivity
        }
    }

    private void ShowGearDisplay(string gearText)
    {
        if (gearDisplayText == null) return;

        if (gearDisplayCoroutine != null)
        {
            StopCoroutine(gearDisplayCoroutine); // Stop any ongoing coroutine to reset the timer
        }

        gearDisplayCoroutine = StartCoroutine(DisplayGearText(gearText));
    }

    private IEnumerator DisplayGearText(string gearText)
    {
        gearDisplayText.text = gearText;
        gearDisplayText.gameObject.SetActive(true); // Show the text
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        gearDisplayText.gameObject.SetActive(false); // Hide the text
    }
}

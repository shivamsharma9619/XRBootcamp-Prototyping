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
    public InputActionReference GearUp, GearDown;
    public XRKnob knob;
    public bool isPressed = false;
    public float motorTorque;
    public float breakTorque;
    public float steeringMax;
    public Text gearDisplayText, gearDisplayTextDash; // UI Text for displaying gear changes
    private Coroutine gearDisplayCoroutine;

    void Start()
    {
        if (gearDisplayText != null)
        {
            gearDisplayText.gameObject.SetActive(false); // Hide the text initially
        }
    }

    void Update()
    {
        // Set motorTorque and brakeTorque based on the gear
        switch (GearValue)
        {
            case 0: // Park
                motorTorque = 0;
                breakTorque = 100;
                break;
            case 1: // Neutral
                motorTorque = 0;
                breakTorque = 100;
                break;
            case 2: // Drive
                motorTorque = 400;
                breakTorque = 250;
                break;
            case 3: // Reverse
                motorTorque = -100;
                breakTorque = 100;
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

        // Steering logic
        for (int i = 0; i < wheels.Length - 2; i++)
        {
            wheels[i].steerAngle = -(knob.value - 0.5f) * 120f; // Adjusting steering sensitivity
        }

        // Gear Change logic
        if (GearUp.action.WasPressedThisFrame())
        {
            if (GearValue < 3) // Max gear is 5
            {
                GearValue++;
                Debug.Log("Gear Up: " + GearValue);
                ShowGearDisplay((GearValue == 0 ? "Park": GearValue==1? "Neutral": GearValue == 2?"Drive" : GearValue == 3 ? "Reverse" : GearValue.ToString()));
            }
        }

        if (GearDown.action.WasPressedThisFrame())
        {
            if (GearValue > 0) // Min gear is reverse (-1)
            {
                GearValue--;
                Debug.Log("Gear Down: " + GearValue);
                ShowGearDisplay((GearValue == 0 ? "Park": GearValue==1? "Neutral": GearValue == 2?"Drive" : GearValue == 3 ? "Reverse" : GearValue.ToString()));
            }
        }
    }

    private void ShowGearDisplay(string gearText)
    {
        gearDisplayTextDash.text = gearText;
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

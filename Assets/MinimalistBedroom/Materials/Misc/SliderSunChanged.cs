using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSunChanged : MonoBehaviour
{
   public Slider rotationSlider;   // Reference to the slider
    public Transform targetObject; // The object to rotate
    public float rotationMultiplier = 10f; // Multiplier to increase the rotation effect

    private float startingXRotation; // Store the initial X-axis rotation;

    void Start()
    {
        if (rotationSlider == null || targetObject == null)
        {
            Debug.LogError("Please assign both the slider and the target object in the Inspector.");
            return;
        }

        // Record the starting X-axis rotation of the target object
        startingXRotation = targetObject.eulerAngles.x;

        // Add a listener to the slider to detect value changes
        rotationSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnSliderValueChanged(float value)
    {
        // Apply rotation along the X-axis based on the slider value, added to the initial rotation
        float newXRotation = startingXRotation + (-value * rotationMultiplier);
        targetObject.rotation = Quaternion.Euler(newXRotation, 0, 0);
    }

    void OnDestroy()
    {
        // Remove listener to prevent memory leaks
        if (rotationSlider != null)
        {
            rotationSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}

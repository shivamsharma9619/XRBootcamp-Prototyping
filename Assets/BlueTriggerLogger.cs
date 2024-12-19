using UnityEngine;

public class BlueTriggerLogger : MonoBehaviour
{
    public GameObject Potassium;  // The GameObject whose material you want to modify
    private Material liquid;  // The material reference
    public string shaderPropertyNameFill = "_Fill"; // The name of the shader property for the fill (default "_Fill")
    public string shaderPropertyNameSideColor = "_Side_Color"; // The name of the shader property for the side color
    public string shaderPropertyNameTopColor = "_Top_Color"; // The name of the shader property for the side color
    public float increaseRate = 0.01f; // Rate at which the property will increase
    private bool isInsideTrigger = false; // Flag to check if the object is inside the trigger
    public Color AfterChange;

    private void Start()
    {
        // Ensure the Potassium GameObject is assigned
        if (Potassium == null)
        {
            Debug.LogError("Potassium GameObject is not assigned!");
            return;
        }

        // Get the material of the Potassium GameObject
        Renderer liq = Potassium.GetComponent<Renderer>();
        if (liq != null)
        {
            liquid = liq.material; // Assign the material instance to modify
            Debug.Log("Material component found on Potassium GameObject!");
        }
        else
        {
            Debug.LogError("Renderer component not found on Potassium GameObject!");
        }
    }

    private void Update()
    {
        // If the object is inside the trigger, keep increasing the property over time
        if (isInsideTrigger && liquid != null)
        {
            float currentFill = liquid.GetFloat(shaderPropertyNameFill);
            currentFill += increaseRate * Time.deltaTime;
            if (currentFill > 0.56f){
isInsideTrigger = false; 
}
            if (currentFill > 0.54f)
            {
                // Change the side color to green once the _Fill value exceeds 0.56
                liquid.SetColor(shaderPropertyNameSideColor, AfterChange);
                liquid.SetColor(shaderPropertyNameTopColor, AfterChange);
            }

            // Clamp the value between 0 and 1
            currentFill = Mathf.Clamp(currentFill, 0f, 1f);

            // Update the material's _Fill property
            liquid.SetFloat(shaderPropertyNameFill, currentFill);
        }
    }

    // This method is called when a collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blue"))
        {
            isInsideTrigger = true; // Start increasing the property
            Debug.Log("Entered trigger with 'blue' tag.");
        }
    }

    // This method is called when a collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("blue"))
        {
            isInsideTrigger = false; // Stop increasing the property
            Debug.Log("Exited trigger with 'blue' tag.");
        }
    }
}

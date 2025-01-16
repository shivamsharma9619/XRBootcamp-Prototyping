using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
   public Renderer targetRenderer; // Renderer of the object whose material you want to change
    public Material[] materials;   // Array to hold up to 3 materials

    void Start()
    {
        if (materials.Length != 3)
        {
            Debug.LogWarning("Please assign exactly 3 materials in the Materials array.");
        }

        if (targetRenderer == null)
        {
            Debug.LogError("No Renderer assigned. Please assign a target renderer.");
        }
    }

    // Function to set the first material
    public void SetMaterial1()
    {
        ChangeMaterialByIndex(0); // First material
    }

    // Function to set the second material
    public void SetMaterial2()
    {
        ChangeMaterialByIndex(1); // Second material
    }

    // Function to set the third material
    public void SetMaterial3()
    {
        ChangeMaterialByIndex(2); // Third material
    }

    // Helper function to change the material by index
    private void ChangeMaterialByIndex(int index)
    {
        if (materials.Length > index && index >= 0)
        {
            if (targetRenderer != null)
            {
                targetRenderer.material = materials[index];
            }
        }
        else
        {
            Debug.LogWarning($"Material index {index} is out of range. Make sure you have assigned the correct materials.");
        }
    }
}

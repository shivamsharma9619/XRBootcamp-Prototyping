using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialAlpha : MonoBehaviour
{
   public Renderer[] objectRenderers; // Array of Renderers with materials
    public float intervalTime = 3f;    // Time interval for switching materials
    private int currentIndex = 0;     // Index of the currently active material
    private float timer = 0f;         // Timer to track elapsed time

    void Start()
    {
        // Initialize all materials to an alpha of 0.1f
        foreach (Renderer renderer in objectRenderers)
        {
            if (renderer != null)
            {
                SetAlpha(renderer.material, 0.1f);
            }
        }

        // Set the first material to full alpha
        if (objectRenderers.Length > 0 && objectRenderers[0] != null)
        {
            SetAlpha(objectRenderers[0].material, 1f);
        }
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the interval time has passed
        if (timer >= intervalTime)
        {
            // Reset the timer
            timer = 0f;

            // Reset all materials to 0.1 alpha
            foreach (Renderer renderer in objectRenderers)
            {
                if (renderer != null)
                {
                    SetAlpha(renderer.material, 0.1f);
                }
            }

            // Increment the index to the next material
            currentIndex = (currentIndex + 1) % objectRenderers.Length;

            // Set the current material to full alpha
            if (objectRenderers[currentIndex] != null)
            {
                SetAlpha(objectRenderers[currentIndex].material, 1f);
            }
        }
    }

    // Helper method to set the alpha of a material
    private void SetAlpha(Material material, float alpha)
    {
        if (material != null)
        {
            Color color = material.color;
            color.a = Mathf.Clamp01(alpha);
            material.color = color;
        }
    }
}

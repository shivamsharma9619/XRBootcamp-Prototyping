using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
   public Renderer[] renderers; // Array to hold renderers

    void Start()
    {
        if (renderers.Length == 0)
        {
            Debug.LogWarning("No renderers assigned!");
        }
        SetColor3();
    }

    // Function to change all renderers to Color 1
    public void SetColor1()
    {
        ChangeColor("#FF5733"); // Example: Reddish-Orange
    }

    // Function to change all renderers to Color 2
    public void SetColor2()
    {
        ChangeColor("#33FF57"); // Example: Green
    }

    // Function to change all renderers to Color 3
    public void SetColor3()
    {
        ChangeColor("#3357FF"); // Example: Blue
    }

    // General method to apply the color to all renderers
    private void ChangeColor(string hexColor)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
        }
        else
        {
            Debug.LogWarning($"Invalid Hex Color Code: {hexColor}");
        }
    }
}

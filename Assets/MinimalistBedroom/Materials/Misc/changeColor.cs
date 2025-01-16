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
    }

    // Function to change all renderers to Color 1
    public void SetColor1()
    {
        ChangeColor("#383030"); 
    }

    // Function to change all renderers to Color 2
    public void SetColor2()
    {
        ChangeColor("#FFF6AA");
    }

    // Function to change all renderers to Color 3
    public void SetColor3()
    {
        ChangeColor("#E7E7E7");
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

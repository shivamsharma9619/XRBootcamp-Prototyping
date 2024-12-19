using UnityEngine;

public class AdjustShaderFillBasedOnRotation : MonoBehaviour
{
    public string shaderPropertyName = "_Fill"; // The name of the shader property (default "_Fill")
    public float decreaseRate = 0.01f; // The rate at which the fill value decreases
    private Material childMaterial;  // The material of the child object
    public GameObject flow;
    private void Start()
    {
        // Get the material of the child object
        if (transform.childCount > 0)
        {
            Renderer childRenderer = transform.GetChild(0).GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childMaterial = childRenderer.material;
            }
            else
            {
                Debug.LogError("No Renderer found on the child object.");
            }
        }
        else
        {
            Debug.LogError("No child object found.");
        }
        flow.SetActive(false);
    }

    private void Update()
    {
        if (childMaterial == null) return;

        // Get the X-axis rotation of the parent object
        float xRotation = transform.eulerAngles.x;

        // Adjust the angle to handle Unity's 0-360 wrapping
        if (xRotation > 180)
        {
            xRotation -= 360;
        }

        // Check if the rotation is between 0 and 90 degrees
        if (xRotation >= 0 && xRotation <= 90)
        {


            // Get the current value of the shader's fill property
            float currentFill = childMaterial.GetFloat(shaderPropertyName);


            if(currentFill<0.510){
                flow.SetActive(false);
            }
            else{
                    flow.SetActive(true);

            }

            // Decrease the fill value
            currentFill -= decreaseRate * Time.deltaTime*0.25f;

            // Clamp the fill value between 0 and 1
            currentFill = Mathf.Clamp(currentFill, 0f, 1f);

            Debug.Log(currentFill);
            // Set the new fill value on the shader
            childMaterial.SetFloat(shaderPropertyName, currentFill);

            
        }
    }
}

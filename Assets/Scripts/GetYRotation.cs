using UnityEngine;
using UnityEngine.UI;


public class GetYRotation : MonoBehaviour
{
    // Reference to the target GameObject (assign in the Inspector or dynamically)
    public GameObject targetObject;
    public Text GearDisplay;
    public int mappedValue = 0;


    void Update()
    {
        if (targetObject != null)
        {
            // Get the Y-axis rotation of the target GameObject
            float yRotation = targetObject.transform.localEulerAngles.y;

            // Adjust rotation to range [-180°, 180°] for easier mapping
            if (yRotation > 180f)
            {
                yRotation -= 360f;
            }

            // Determine the mapped value based on the specified ranges

            if (yRotation >= -20f && yRotation < 20f)
            {
                mappedValue = 1;
                GearDisplay.text = "P";
            }
            else if (yRotation >= 70f && yRotation < 110f)
            {
                mappedValue = 2;
                GearDisplay.text = "N";

            }
            else if (yRotation >= 160f && yRotation < 200f)
            {
                mappedValue = 3;
                GearDisplay.text = "D";

            }
            else if (yRotation <= -70f && yRotation > -110f)
            {
                mappedValue = 4;
                GearDisplay.text = "R";

            }


        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned!");
        }
    }
}

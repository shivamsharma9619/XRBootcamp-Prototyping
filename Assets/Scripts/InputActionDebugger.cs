using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionDebugger : MonoBehaviour
{
    public InputActionAsset inputActions; // Reference to your Input Action Asset

    private void OnEnable()
    {
        if (inputActions != null)
        {
            // Iterate through all action maps and actions
            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    // Subscribe to performed events for each action
                    action.performed += OnActionPerformed;
                }
            }

            // Enable all action maps
            inputActions.Enable();
        }
        else
        {
            Debug.LogError("InputActionAsset is not assigned!");
        }
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            // Unsubscribe from all actions
            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    action.performed -= OnActionPerformed;
                }
            }

            // Disable all action maps
            inputActions.Disable();
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        // Log the name of the triggered action
        Debug.Log($"Action Triggered: {context.action.name}, Value: {context.ReadValueAsObject()}");
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    private XRBaseInteractor currentInteractor;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnSelectEnter);
        grabInteractable.selectExited.AddListener(OnSelectExit);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
        grabInteractable.selectExited.RemoveListener(OnSelectExit);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        // Cast the interactor to XRBaseInteractor
        var interactor = args.interactorObject as XRBaseInteractor;

        if (!isGrabbed)
        {
            // First click: Grab the object
            isGrabbed = true;
            currentInteractor = interactor;
        }
        else if (currentInteractor == interactor) // Ensure same interactor toggles
        {
            // Second click: Release the object
            isGrabbed = false;
            grabInteractable.interactionManager.SelectExit(currentInteractor, grabInteractable);
            currentInteractor = null;
        }
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        // Ensure proper cleanup if object is force-released
        if (!isGrabbed)
        {
            currentInteractor = null;
        }
    }
}

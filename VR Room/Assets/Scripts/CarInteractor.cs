using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CarInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask carPartLayer; // Only detect car parts
    [SerializeField] private UiManager uiManager;  // Reference to the UI Manager
    [SerializeField] private InputActionProperty menuButtonAction;  // Button to trigger UI

    private XRBaseInteractor xRBaseInteractor;
    private CarPart hoveredCarPart; // Store the hovered car part

    void Start()
    {
        xRBaseInteractor = GetComponent<XRBaseInteractor>();
        xRBaseInteractor.hoverEntered.AddListener(OnHoverEntered);
        xRBaseInteractor.hoverExited.AddListener(OnHoverExited);
        uiManager = Container.GetInstance<UiManager>();
        // Optionally initialize anything else, for example, assign button action.
        if (menuButtonAction == null)
        {
            Debug.LogError("Menu Button Action is not assigned!");
        }
    }

    void Update()
    {
        if (menuButtonAction.action.WasPerformedThisFrame())
        {
            Debug.Log("Primary button was pressed");
        }
        // Check if the hover action is active and the button is pressed
        if (hoveredCarPart != null && menuButtonAction.action.WasPerformedThisFrame())
        {
            // Show the UI when button is pressed
            Debug.Log("Should show car ui");
            ShowCarUi(hoveredCarPart);
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"Interactor entered {args.interactableObject.transform.name}");
        if(args.interactableObject.transform.TryGetComponent<CarPart>(out CarPart part))
        {
            Debug.Log($"Hover over the part {part.name}");
            hoveredCarPart = part;
        }
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"Interactor exited {args.interactableObject.transform.name}");
        if (args.interactableObject.transform.TryGetComponent<CarPart>(out CarPart part))
        {
            Debug.Log($"Stop hovering over the part {part.name}");
            hoveredCarPart = null;
        }
    }

    // Show the UI linked to the hovered car part
    private void ShowCarUi(CarPart carPart)
    {
        // Assume car part has a reference to a Car UI, or you can find it in the parent
        CarUi carUi = carPart.GetComponentInParent<CarUi>();
        if (carUi != null && carUi.CarMenuUi != null)
        {
            uiManager.CreateUi(carUi.CarMenuUi);  // Trigger UI to appear
        }
    }
}

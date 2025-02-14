using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CarInteractor : MonoBehaviour
{
    [SerializeField] private UiManager m_uiManager;  // Reference to the UI Manager
    [SerializeField] private InputActionProperty m_menuButtonAction;  // Button to trigger UI

    private XRBaseInteractor m_xRBaseInteractor;
    private CarPart m_hoveredCarPart;

    void Start()
    {
        m_uiManager = Container.GetInstance<UiManager>();
        m_xRBaseInteractor = GetComponent<XRBaseInteractor>();
        m_xRBaseInteractor.hoverEntered.AddListener(OnHoverEntered);
        m_xRBaseInteractor.hoverExited.AddListener(OnHoverExited);
        if (m_menuButtonAction == null)
        {
            Debug.LogError("Menu Button Action is not assigned!");
        }
    }

    void Update()
    {
        if (m_menuButtonAction.action.WasPerformedThisFrame() && m_hoveredCarPart != null)
        {
            ShowCarUi(m_hoveredCarPart);
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent<CarPart>(out CarPart part))
        {
            m_hoveredCarPart = part;
        }
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent<CarPart>(out CarPart part))
        {
            m_hoveredCarPart = null;
        }
    }

    private void ShowCarUi(CarPart carPart)
    {
        CarUi carUi = carPart.GetComponentInParent<CarUi>();
        if (carUi != null && carUi.CarMenuUi != null)
        {
            m_uiManager.CreateUi(carUi.CarMenuUi);
        }
    }
}
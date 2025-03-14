using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolInteractor : XRDirectInteractor
{
    private XRGrabInteractable m_grabInteractable;
    //private XRBaseController m_baseController;
    [SerializeField]
    private List<CarPartType> m_carPartTypeToInteract;

    public List<CarPartType> CarPartTypeToInteract => m_carPartTypeToInteract;

    protected override void Awake()
    {
        base.Awake();
        m_grabInteractable = GetComponent<XRGrabInteractable>();
        /*m_baseController = GetComponent<XRBaseController>();
        m_baseController.enabled = false;*/
        if (m_grabInteractable)
        {
            m_grabInteractable.selectEntered.AddListener(OnGrabbed);
            m_grabInteractable.selectExited.AddListener(OnReleased);
            m_grabInteractable.activated.AddListener(Interact);
        }

        enabled = false;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} was grabbed");

        enabled = true;  // Enable interactor when grabbed
    }

    private void Interact(ActivateEventArgs args)
    {
        Debug.Log("activate the controller");
        if(interactablesHovered.Count > 0)
        {
            (interactablesHovered[0] as CarPartInteractable).OnActivate(args);
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} was released");

        enabled = false;
    }

    private void OnDestroy()
    {
        if (m_grabInteractable)
        {
            m_grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            m_grabInteractable.selectExited.RemoveListener(OnReleased);
            m_grabInteractable.activated.RemoveListener(Interact);
        } 
    }
}

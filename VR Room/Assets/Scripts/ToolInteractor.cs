using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolInteractor : XRDirectInteractor
{
    [SerializeField]
    private List<CarPartType> m_carPartTypeToInteract;
    private XRGrabInteractable m_grabInteractable; 

    public List<CarPartType> CarPartTypeToInteract => m_carPartTypeToInteract;

    protected override void Awake()
    {
        base.Awake();
        m_grabInteractable = GetComponentInParent<XRGrabInteractable>();
        if (m_grabInteractable)
        {
            m_grabInteractable.selectEntered.AddListener(OnGrabbed);
            m_grabInteractable.selectExited.AddListener(OnReleased);
            m_grabInteractable.activated.AddListener(Interact);
        }

        enabled = false;
    }

    private void Interact(ActivateEventArgs args)
    {
        Debug.Log("activate the controller");
        if (interactablesHovered.Count > 0)
        {
            foreach (var interactable in interactablesHovered)
            {
                var tmp = (interactable as CarPartInteractable);
                if (m_carPartTypeToInteract.Contains(tmp.CarPartType))
                {
                    tmp.OnActivate(args);
                    break;
                }
            }
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} was grabbed");
        enabled = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} was released");

        enabled = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (m_grabInteractable)
        {
            m_grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            m_grabInteractable.selectExited.RemoveListener(OnReleased);
            m_grabInteractable.activated.RemoveListener(Interact);
        } 
    }
}

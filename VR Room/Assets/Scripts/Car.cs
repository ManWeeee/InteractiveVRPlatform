using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Material m_inactiveMaterial;
    [SerializeField] private Material m_highlightedMaterial;
    [SerializeField] private CarPartType m_brokenPartsType;
    private List<CarPart> m_parts = new List<CarPart>();
    private void Awake()
    {
        Debug.Log($"Parts amount = {m_parts.Count}");
        m_parts = GetComponentsInChildren<CarPart>().ToList();
        if(Container.TryGetInstance<LevelManager>(out var manager))
        {
            SetBrokenPartsType(manager.LevelInfo.brokenPartType);
        }
    }

    public void SetBrokenPartsType(CarPartType brokenPartsType)
    {
        m_brokenPartsType = brokenPartsType;
        Debug.Log("Broken Parts are set");
    }

    public void EnableInteractionToAll()
    {
        foreach (var part in m_parts)
        {
            if(part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                EnableInteraction(interactable);
            }
        }
    }

    public void DisableInteractionToAll()
    {
        foreach (var part in m_parts)
        {
            if (part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.SetInteraction();
            }
        }
    }

    

    public void EnableInteraction(CarPartInteractable interactable)
    {
        interactable.enabled = true;
    }

    public void EnterOverviewMode()
    {
        Debug.Log("Entered Overview Mode");
        foreach (var part in m_parts)
        {
            if(part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.ResetRendererMaterialsToDefault();
                interactable.SetInteraction();
            }
        }
    }

    public void EnterInspectionMode()
    {
        List<CarPart> parts = new();
        foreach (var part in m_parts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            if (part.GetCarPartType == m_brokenPartsType)
            {
                parts.AddRange(part.GetAllDependableParts());
                interactable.SetRendererMaterialToSelf(m_highlightedMaterial);
            }
            else
            {
                if (!parts.Contains(part))
                {
                    interactable.SetRendererMaterialsTo(m_inactiveMaterial);
                }
            }
            interactable.SetInteraction();
        }
    }

    public void EnterDisassemblyMode()
    {
        Debug.Log("Entered Disassembly mode");
        foreach (var part in m_parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.ResetRendererMaterialsToDefault();
                interactable.SetInteraction(true);
            }
        }
    }

    public void ExitDisassemblyMode()
    {
        foreach (var part in m_parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.ResetRendererMaterialsToDefault();
                interactable.SetInteraction();
            }
        }
    }
}

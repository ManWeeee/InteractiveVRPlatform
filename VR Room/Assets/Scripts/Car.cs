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
    private List<CarPart> m_brokenParts = new List<CarPart>();
    private List<CarPart> m_removedCarParts = new List<CarPart>();
    private void Awake()
    {
        m_parts = GetComponentsInChildren<CarPart>().ToList();
        foreach (var part in m_parts)
        {
            part.Disassembled += (item) => m_removedCarParts.Add(item);
            part.Assembled += OnAssembled;
        }
        if(Container.TryGetInstance<LevelManager>(out var manager))
        {
            SetBrokenPartsType(manager.LevelInfo.brokenPartType);
            m_brokenParts = GetBrokenPartsByType(m_brokenPartsType, m_parts);
        }
    }

    private void OnAssembled(CarPart part)
    {
        if (m_removedCarParts.Contains(part))
        {
            m_removedCarParts.Remove(part);
        }
        var interactable = part.GetComponent<CarPartInteractable>();
        interactable.ResetRendererMaterialsToDefault();
        interactable.SetInteraction();
        UpdateAssemblyMode();
    }

    public void SetBrokenPartsType(CarPartType brokenPartsType)
    {
        m_brokenPartsType = brokenPartsType;
    }

    private List<CarPart> GetBrokenPartsByType(CarPartType brokenPartsType, List<CarPart> carParts)
    {
        List<CarPart> parts = new();
        foreach (var part in carParts)
        {
            if (part.GetCarPartType == m_brokenPartsType)
            {
                parts.AddRange(part.GetAllDependableParts());
            }
        }
        return parts;
    }

    public void EnterOverviewMode()
    {
        Debug.Log("Entered Overview Mode");
        foreach (var part in m_parts)
        {
            if(!part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            interactable.ResetRendererMaterialsToDefault();
            interactable.SetInteraction();
        }
    }

    public void EnterInspectionMode()
    {
        foreach (var part in m_parts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            if (!m_brokenParts.Contains(part))
            {
                interactable.SetRendererMaterialsTo(m_inactiveMaterial);
            }
        }
    }

    public void ExitInspectionMode()
    {
        foreach(var part in m_parts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            interactable.ResetRendererMaterialsToDefault();
        }
    }

    public void EnterDisassemblyMode()
    {
        foreach (var part in m_parts)
        {
            if (!part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }

            if(m_brokenParts.Count <= 0)
            {
                interactable.SetInteraction(true);
            }
            else if (m_brokenParts.Contains(part))
            {
                interactable.SetInteraction(true);
            }
        }
    }

    public void ExitDisassemblyMode()
    {
        foreach (var part in m_parts)
        {
            if (!part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }

            if (m_brokenParts.Count <= 0)
            {
                interactable.SetInteraction();
            }
            else if (m_brokenParts.Contains(part))
            {
                interactable.SetInteraction();
            }
        }
    }

    public void EnterAssemblyMode()
    {
        foreach(var part in m_removedCarParts)
        {
            if(!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            if(part.CanBeAssembled)
            {
                part.gameObject.SetActive(true);
            }
            interactable.SetRendererMaterialsTo(m_inactiveMaterial);
            interactable.SetInteraction(canAssemble : true);
        }
    }

    public void ExitAssemblyMode()
    {
        foreach (var part in m_removedCarParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            if (part.CanBeAssembled)
            {
                part.gameObject.SetActive(false);
            }
            interactable.ResetRendererMaterialsToDefault();
            interactable.SetInteraction();
        }
    }

    public void UpdateAssemblyMode()
    {
        foreach (var part in m_removedCarParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                return;
            }
            if (part.CanBeAssembled)
            {
                part.gameObject.SetActive(true);
            }
            interactable.SetRendererMaterialsTo(m_inactiveMaterial);
            interactable.SetInteraction(canAssemble: true);
        }
    }

    private void OnDestroy()
    {
        foreach (var part in m_parts)
        {
            part.Disassembled -= (item) => m_removedCarParts.Add(item);
            part.Assembled -= OnAssembled;
        }
    }
}

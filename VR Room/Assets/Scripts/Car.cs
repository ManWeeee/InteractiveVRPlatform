using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private CarStateManager m_stateManager;
    [SerializeField] private Material m_inactiveMaterial;

    private CarPartManager m_partManager;
    public CarStateManager StateManager => m_stateManager;

    private void Awake()
    {
        if (!Container.TryGetInstance<LevelManager>(out var manager))
        {
            Debug.LogError($"Unable to instance of type {manager.GetType()} in {this.GetType()}");
        }
        m_partManager = new(GetComponentsInChildren<CarPart>().ToList(), manager.LevelInfo.brokenPartType);
        m_stateManager = new(m_partManager, m_inactiveMaterial);
    }

}

public class CarStateManager
{
    private Material m_inactiveMaterial;
    private Material m_highlightedMaterial;
    private CarPartManager m_partManager;
    public CarStateManager(CarPartManager partManager, Material inactive)
    {
        m_partManager = partManager;
        m_partManager.PartAssembled += UpdateAssemblyMode;
        m_inactiveMaterial = inactive;
    }
    public void EnterOverviewMode()
    {
        Debug.Log("Entered Overview Mode");
        foreach (var part in m_partManager.AllParts)
        {
            if (!part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
            }
            interactable.ResetRendererMaterialsToDefault();
            interactable.SetInteraction();
        }
    }

    public void EnterInspectionMode()
    {
        foreach (var part in m_partManager.AllParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
            }
            if (!m_partManager.BrokenParts.Contains(part))
            {
                interactable.SetRendererMaterialsTo(m_inactiveMaterial);
            }
        }
    }

    public void ExitInspectionMode()
    {
        foreach (var part in m_partManager.AllParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
            }
            interactable.ResetRendererMaterialsToDefault();
        }
    }

    public void EnterDisassemblyMode()
    {
        foreach (var part in m_partManager.AllParts)
        {
            if (!part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
            }

            if (m_partManager.BrokenParts.Count <= 0)
            {
                interactable.SetInteraction(true);
            }
            else if (m_partManager.BrokenParts.Contains(part))
            {
                interactable.SetInteraction(true);
            }
        }
    }

    public void ExitDisassemblyMode()
    {
        foreach (var part in m_partManager.AllParts)
        {
            if (!part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
            }

            if (m_partManager.BrokenParts.Count <= 0)
            {
                interactable.SetInteraction();
            }
            else if (m_partManager.BrokenParts.Contains(part))
            {
                interactable.SetInteraction();
            }
        }
    }

    public void EnterAssemblyMode()
    {
        foreach (var part in m_partManager.RemovedParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
            }
            if (part.CanBeAssembled)
            {
                part.gameObject.SetActive(true);
            }
            interactable.SetRendererMaterialsTo(m_inactiveMaterial);
            interactable.SetInteraction(canAssemble: true);
        }
    }

    public void ExitAssemblyMode()
    {
        foreach (var part in m_partManager.RemovedParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
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
        foreach (var part in m_partManager.RemovedParts)
        {
            if (!part.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                continue;
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
        m_partManager.PartAssembled -= UpdateAssemblyMode;
    }
}

public class CarPartManager
{
    private CarPartType m_brokenPartsType;
    private List<CarPart> m_parts = new();
    private List<CarPart> m_brokenParts = new();
    private List<CarPart> m_removedCarParts = new();
    public Action PartAssembled;
    public List<CarPart> AllParts => m_parts;
    public List<CarPart> BrokenParts => m_brokenParts;
    public List<CarPart> RemovedParts => m_removedCarParts;

    public CarPartManager(List<CarPart> parts, CarPartType brokenPartType = CarPartType.None)
    {
        m_parts = parts;
        Debug.Log($"Number of collected parts = {m_parts.Count}");
        foreach (var part in m_parts)
        {
            part.Disassembled += (item) => m_removedCarParts.Add(item);
            part.Assembled += OnAssembled;
        }
        SetBrokenPartsType(brokenPartType);
        m_brokenParts = GetBrokenPartsByType(m_brokenPartsType, m_parts);
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
        PartAssembled?.Invoke();
    }

    public void SetBrokenPartsType(CarPartType type) { m_brokenPartsType = type; }

    public List<CarPart> GetBrokenPartsByType(CarPartType brokenPartsType, List<CarPart> carParts)
    {
        List<CarPart> parts = new();
        foreach (var part in carParts)
        {
            if (part.PartInfo && part.PartInfo.GetCarPartType == m_brokenPartsType)
            {
                parts.AddRange(part.GetAllDependableParts());
            }
        }
        return parts;
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
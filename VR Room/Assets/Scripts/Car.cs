using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Material m_inactiveMaterial;
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

    private void Start()
    {
        var parts = GetBrokenParts();
        DisableInteraction();
        if (parts.Count > 0)
        {
            m_parts = parts;
        }
        EnableInteraction();
    }

    public void SetBrokenPartsType(CarPartType brokenPartsType)
    {
        m_brokenPartsType = brokenPartsType;
        Debug.Log("Broken Parts are set");
    }

    private List<CarPart> GetBrokenParts()
    {
        Debug.Log($"Start to set broken parts");
        List<CarPart> parts = new();
        foreach (var part in m_parts)
        {
            if (part.GetCarPartType == m_brokenPartsType)
            {
                parts.AddRange(part.GetAllDependableParts());
            }
            else
            {
                if (!parts.Contains(part))
                    part.GetComponent<CarPartInteractable>().SetRendererMaterialsTo(m_inactiveMaterial);
            }
        }
        Debug.Log($"Broken parts amount = {parts.Count}");
        return parts;
    }
    public void DisableInteraction()
    {
        foreach (var part in m_parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.enabled = false;
            }
        }
    }

    public void EnableInteraction()
    {
        foreach (var part in m_parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.enabled = true;
            }
        }
    }
}

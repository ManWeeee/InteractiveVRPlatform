using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private bool m_inTutorial;
    [SerializeField] private Material m_inactiveMaterial;

    List<CarPart> _parts = new List<CarPart>();
    private void Start()
    {
        _parts = GetComponentsInChildren<CarPart>().ToList();
        if (m_inTutorial)
        {
            List<CarPart> parts = new();
            foreach (var part in _parts) {
                if (part.IsBroken)
                {
                    parts.AddRange(part.GetAllDependableParts());
                }
                else
                {
                    if(!parts.Contains(part))
                        part.GetComponent<CarPartInteractable>().SetRendererMaterialsTo(m_inactiveMaterial);
                }
            }
            DisableInteraction();
            _parts = parts;
            EnableInteraction();
        }
    }


    public void DisableInteraction()
    {
        foreach (var part in _parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.enabled = false;
            }
        }
    }

    public void EnableInteraction()
    {
        foreach (var part in _parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.enabled = true;
            }
        }
    }
}

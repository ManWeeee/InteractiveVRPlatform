using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverviewMode : ICarViewMode
{
    List<CarPart> _parts = new List<CarPart>();
    public void EnterMode(GameObject car)
    {
        _parts = car.GetComponentsInChildren<CarPart>().ToList();
        foreach (var part in _parts)
        {
            if (part.gameObject.TryGetComponent<CarPartInteractable>(out CarPartInteractable interactable))
            {
                interactable.enabled = false;
            }
        }
    }

    public void UpdateMode(GameObject car)
    {
        return;
    }

    public void ExitMode(GameObject car)
    {
        return;
    }
}

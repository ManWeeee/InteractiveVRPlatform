using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverviewMode : ICarViewMode
{
    public bool CanInteractWithParts { get => false;}

    public void EnterMode(Car car)
    {
        car.StateManager.EnterOverviewMode();
    }

    public void UpdateMode(Car car)
    {
        return;
    }

    public void ExitMode(Car car)
    {
        return;
    }
}

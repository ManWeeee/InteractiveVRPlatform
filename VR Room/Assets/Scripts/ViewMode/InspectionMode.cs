using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionMode : ICarViewMode
{
    public bool CanInteractWithParts => throw new System.NotImplementedException();

    public void EnterMode(Car car)
    {
        car.EnterInspectionMode();
    }

    public void ExitMode(Car car)
    {
        return;
    }

    public void UpdateMode(Car car)
    {
        throw new System.NotImplementedException();
    }
}

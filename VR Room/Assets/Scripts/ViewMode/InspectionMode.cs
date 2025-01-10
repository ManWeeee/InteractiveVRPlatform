using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionMode : ICarViewMode
{

    public void EnterMode(Car car)
    {
        car.StateManager.EnterInspectionMode();
    }

    public void ExitMode(Car car)
    {
        car.StateManager.ExitInspectionMode();
    }

    public void UpdateMode(Car car)
    {
        return;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionMode : ICarViewMode
{

    public void EnterMode(Car car)
    {
        car.EnterInspectionMode();
    }

    public void ExitMode(Car car)
    {
        car.ExitInspectionMode();
    }

    public void UpdateMode(Car car)
    {
        throw new System.NotImplementedException();
    }
}

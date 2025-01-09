using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyMode : ICarViewMode
{
    public void EnterMode(Car car)
    {
        car.EnterAssemblyMode();
    }

    public void ExitMode(Car car)
    {
        car.ExitAssemblyMode();
    }

    public void UpdateMode(Car car)
    {
        car.UpdateAssemblyMode();
    }
}

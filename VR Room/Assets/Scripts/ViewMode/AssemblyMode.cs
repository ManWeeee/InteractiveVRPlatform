using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyMode : ICarViewMode
{
    public void EnterMode(Car car)
    {
        car.StateManager.EnterAssemblyMode();
    }

    public void ExitMode(Car car)
    {
        car.StateManager.ExitAssemblyMode();
    }

    public void UpdateMode(Car car)
    {
        car.StateManager.UpdateAssemblyMode();
    }
}

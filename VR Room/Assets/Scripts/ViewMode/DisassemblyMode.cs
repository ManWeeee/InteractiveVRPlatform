using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class DisassemblyMode : ICarViewMode
{

    public void EnterMode(Car car)
    {
        car.StateManager.EnterDisassemblyMode();
    }

    public void UpdateMode(Car car)
    {
        return;
    }

    public void ExitMode(Car car)
    {
        car.StateManager.ExitDisassemblyMode();
    }
}

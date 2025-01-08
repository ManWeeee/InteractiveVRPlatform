using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class DisassemblyMode : ICarViewMode
{
    public bool CanInteractWithParts => true;

    public void EnterMode(Car car)
    {
        car.EnterDisassemblyMode();
    }

    public void UpdateMode(Car car)
    {
        return;
    }

    public void ExitMode(Car car)
    {
        //car.ExitDisassemblyMode();
    }
}

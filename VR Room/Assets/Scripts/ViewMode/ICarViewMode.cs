using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarViewMode
{
    public void EnterMode(GameObject car);
    public void UpdateMode(GameObject car);
    public void ExitMode(GameObject car);
}

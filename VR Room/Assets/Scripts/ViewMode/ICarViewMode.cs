using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarViewMode
{
    public void EnterMode(Car car);
    public void UpdateMode(Car car);
    public void ExitMode(Car car);
}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class CarViewModeManager : MonoBehaviour
{
    private Car car;

    private ICarViewMode currentMode;

    private void Awake()
    {
        Container.Register(this);
    }

   /* private void Start()
    {
        car = GetComponent<Car>();
        SetMode(new OverviewMode());
    }*/
    private void OnEnable()
    {
        car = GetComponent<Car>();
        SetMode(new OverviewMode());
        Debug.Log("CarViewModeManager ON_ENABLE");
    }

    public void SetMode(ICarViewMode newMode)
    {
        currentMode?.ExitMode(car);
        currentMode = newMode;
        currentMode.EnterMode(car);
    }

    private void OnDestroy()
    {
        Container.Unregister<CarViewModeManager>();
    }
}

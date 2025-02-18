using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [SerializeField]
    private CarViewModeManager m_viewModeManager;

    private void Start()
    {
        Container.ResolveWhenAvailable<CarViewModeManager>(manager =>
        {
            m_viewModeManager = manager;
            Debug.Log("CarViewModeManager resolved!");
        });
    }

    public void SwitchToOverview()
    {
        m_viewModeManager.SetMode(new OverviewMode());
    }

    public void SwitchToAssembly()
    {
       m_viewModeManager.SetMode(new AssemblyMode());
    }

    public void SwitchToInspection()
    {
        m_viewModeManager.SetMode(new InspectionMode());
    }

    public void SwitchToDisassembly()
    {
        m_viewModeManager.SetMode(new DisassemblyMode());
    }
}

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
        Container.TryGetInstance<CarViewModeManager>(out m_viewModeManager);
        Debug.Log($"View mode manager is {m_viewModeManager.gameObject.name}");
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

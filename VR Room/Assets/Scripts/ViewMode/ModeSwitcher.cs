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
        m_viewModeManager = Container.GetInstance<CarViewModeManager>();
    }

    public void SwitchToOverview()
    {
        m_viewModeManager.SetMode(new OverviewMode());
    }

    /*private void SwitchToInspection()
    {
        //m_viewModeManager.SetMode(new PartInspectionMode());
    }*/

    public void SwitchToDisassembly()
    {
        m_viewModeManager.SetMode(new DisassemblyMode());
    }
}

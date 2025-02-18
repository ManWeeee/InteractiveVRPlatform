using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOpener : MonoBehaviour
{
    [SerializeField] private InputActionProperty m_menuOpenActionProperty;
    [SerializeField] private GameObject m_mainMenu;

    private UiManager m_uiManager;

    private void Start()
    {
        m_uiManager = Container.GetInstance<UiManager>();
    }

    private void Update()
    {
        if (m_menuOpenActionProperty.action.WasPerformedThisFrame())
        {
            m_uiManager.OpenUi(m_mainMenu);
        }
    }
}

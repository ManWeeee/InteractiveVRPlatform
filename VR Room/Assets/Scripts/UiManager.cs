using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    [SerializeField] private float m_uiOffset;
    [SerializeField] private InputActionProperty m_actionProperty;
    [SerializeField] private MenuUi m_mainMenu;

    private List<UiInstance> m_loadedUIs = new();

    private void Start()
    {
        Container.GetInstance<SceneLoader>().SceneGroupManager.OnSceneGroupLoaded += MakeAllUiInvisible;
    }

    private void Update()
    {
        if (m_actionProperty.action.WasPerformedThisFrame())
        {
            ChangeUiVisibility(m_mainMenu);
        }
    }

    public void RegisterUI(UiInstance instance)
    {
        m_loadedUIs.Add(instance);
    }

    public void UnregisterUi(UiInstance instance)
    {
        if (m_loadedUIs.Contains(instance))
        {
            m_loadedUIs.Remove(instance);
        }
    }

    public void ChangeUiVisibility(string uiPrefabName)
    {
        var instance = m_loadedUIs.Find(item => item.UiObject.name == uiPrefabName);
        if (m_mainMenu.gameObject.activeSelf == true && instance != null)
        {
            ChangeUiVisibility(m_mainMenu);
        }
        ChangeUiVisibility(instance);
    }

    public void ChangeUiVisibility(IUiInstance instance)
    {
        instance.UiObject.SetActive(!instance.UiObject.activeSelf);
        instance.SetPosition(Camera.main.transform.position + Camera.main.transform.forward * m_uiOffset);
        if (instance.UiObject.TryGetComponent<Billboard>(out Billboard billboard))
        {
            billboard.SetTarget(Camera.main.transform);
        }
    }

    private void MakeAllUiInvisible()
    {
        var ui = Resources.FindObjectsOfTypeAll<UiInstance>();
        foreach (var item in ui)
        {
            if (item.UiObject.activeSelf == true)
            {
                RegisterUI(item);
                item.DestroyAction += UnregisterUi;
                ChangeUiVisibility(item);
            }
        }
    }

    private void OnDestroy()
    {
        Container.GetInstance<SceneLoader>().SceneGroupManager.OnSceneGroupLoaded -= MakeAllUiInvisible;
    }
}

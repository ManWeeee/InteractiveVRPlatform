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

    private void Awake()
    {
        Container.Register<UiManager>(this);
    }
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
        if (m_loadedUIs.Contains(instance))
        {
            return;
        }
        m_loadedUIs.Add(instance);
        instance.DestroyAction += UnregisterUi;
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
        instance.SetPosition(Camera.main.transform.position + Camera.main.transform.forward * m_uiOffset);
        if (instance.UiObject.TryGetComponent<Billboard>(out Billboard billboard))
        {
            billboard.SetTarget(Camera.main.transform);
        }
        instance.UiObject.SetActive(!instance.UiObject.activeSelf);
    }

    private void MakeAllUiInvisible()
    {
        foreach (var item in m_loadedUIs)
        {
            Debug.Log($"Ui {item.gameObject.name} is now should become invisible");
            if (item.UiObject.activeSelf == true)
            {
                
                ChangeUiVisibility(item);
            }
            Debug.Log($"Ui {item.gameObject.name} become invisible");
        }
    }

    private void OnDestroy()
    {
        Container.GetInstance<SceneLoader>().SceneGroupManager.OnSceneGroupLoaded -= MakeAllUiInvisible;
    }
}

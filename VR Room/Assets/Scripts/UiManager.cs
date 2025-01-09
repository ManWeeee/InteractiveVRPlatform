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
    [SerializeField] private InputActionProperty m_menuOpenActionProperty;
    [SerializeField] private GameObject m_mainMenu;

    private Dictionary<GameObject, UiInstance> m_CreatedUiPrefabs = new();

    private void Awake()
    {
        Container.Register(this);
    }
    private void Start()
    {
        if(Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded += CloseAllUi;
        }
    }

    private void Update()
    {
        if (m_menuOpenActionProperty.action.WasPerformedThisFrame())
        {
            CreateUi(m_mainMenu);
        }
    }

/*    public void RegisterUI(UiInstance instance)
    {
        if (m_loadedUIs.Contains(instance))
        {
            return;
        }
        m_loadedUIs.Add(instance);
        instance.DestroyAction += UnregisterUi;
    }*/

    public void UnregisterUi(UiInstance instance)
    {
        if (m_CreatedUiPrefabs.ContainsValue(instance))
        {
            m_CreatedUiPrefabs.Remove(m_CreatedUiPrefabs.FirstOrDefault(item => item.Value == instance).Key);
        }
        instance.DestroyAction -= UnregisterUi;
    }
    public void SetUiInFrontOfPlayer(IUiInstance instance)
    {
        instance.SetPosition(Camera.main.transform.position + Camera.main.transform.forward * m_uiOffset);
    }

    public void ChangeUiVisibility(IUiInstance instance)
    {
        instance.UiObject.SetActive(!instance.UiObject.activeSelf);
    }

    
    private void CloseAllUi()
    {
        foreach (var item in m_CreatedUiPrefabs)
        {
            if (item.Value.UiObject.activeSelf == true)
            {
                item.Value.CloseUi();
            }
        }
    }

    public void CreateUi(GameObject uiPrefab)
    {
        var existingInstance = m_CreatedUiPrefabs.TryGetValue(uiPrefab, out UiInstance uiInstance);
        if(!existingInstance)
        {
            var instance = Instantiate(uiPrefab).GetComponent<UiInstance>();
            SetUiTarget(instance);
            m_CreatedUiPrefabs.Add(uiPrefab, instance);
            instance.DestroyAction += UnregisterUi;
            instance.ShowUi();
            SetUiInFrontOfPlayer(instance);
            return;
        }

        uiInstance.ShowUi();
        SetUiInFrontOfPlayer(uiInstance);
    }

    private void SetUiTarget(UiInstance instance)
    {
        if (instance.UiObject.TryGetComponent<Billboard>(out Billboard billboard))
        {
            billboard.SetTarget(Camera.main.transform);
        }
    }

    private void OnDestroy()
    {
        if(Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded -= CloseAllUi;
        }
    }
}

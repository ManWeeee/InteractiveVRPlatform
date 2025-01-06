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
    private List<UiInstance> m_loadedUIs = new();

    private void Awake()
    {
        Container.Register(this);
    }
    private void Start()
    {
        if(Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded += MakeAllUiInvisible;
        }
    }

    private void Update()
    {
        if (m_menuOpenActionProperty.action.WasPerformedThisFrame())
        {
            CreateUi(m_mainMenu);
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
        if (m_CreatedUiPrefabs.ContainsValue(instance))
        {
            m_CreatedUiPrefabs.Remove(m_CreatedUiPrefabs.FirstOrDefault(item => item.Value == instance).Key);
        }
    }
    public void SetUiInFrontOfPlayer(IUiInstance instance)
    {
        instance.SetPosition(Camera.main.transform.position + Camera.main.transform.forward * m_uiOffset);
    }

    public void ChangeUiVisibility(string uiPrefabName)
    {
        var instance = m_loadedUIs.Find(item => item.UiObject.name == uiPrefabName);
        if (m_mainMenu.gameObject.activeSelf == true && instance != null)
        {
            //ChangeUiVisibility(m_mainMenu);
        }
        ChangeUiVisibility(instance);
    }

    public void ChangeUiVisibility(IUiInstance instance)
    {
        instance.UiObject.SetActive(!instance.UiObject.activeSelf);
    }

    private void MakeAllUiInvisible()
    {
        foreach (var item in m_loadedUIs)
        {
            if (item.UiObject.activeSelf == true)
            {
                
                ChangeUiVisibility(item);
            }
        }
    }

    private void CreateUi(GameObject uiPrefab)
    {
        var existingInstance = m_CreatedUiPrefabs.TryGetValue(uiPrefab, out UiInstance uiInstance);
        if(existingInstance)
        {
            ChangeUiVisibility(uiInstance);
            SetUiInFrontOfPlayer(uiInstance);
            return;
        }
        
        var instance = Instantiate(uiPrefab).GetComponent<UiInstance>();
        if (instance.UiObject.TryGetComponent<Billboard>(out Billboard billboard))
        {
            billboard.SetTarget(Camera.main.transform);
        }
        m_CreatedUiPrefabs.Add(uiPrefab, instance);
        RegisterUI(instance);
        SetUiInFrontOfPlayer(instance);
    }

    private void OnDestroy()
    {
        if(Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded -= MakeAllUiInvisible;
        }
    }
}

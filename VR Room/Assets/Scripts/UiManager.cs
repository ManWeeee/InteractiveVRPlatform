using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using SceneManagement;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private float m_uiOffset;
    private IUiInstance m_activeUi;

    private Dictionary<GameObject, IUiInstance> m_CreatedUiPrefabs = new();

    private void Awake()
    {
        Container.Register(this);
    }
    private void Start()
    {
        if (Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded += CloseAllUi;
        }
    }

    public void RegisterUI(GameObject uiPrefab, IUiInstance instance)
    {
        if (m_CreatedUiPrefabs.ContainsKey(uiPrefab))
        {
            return;
        }
        
        var uiInstance = instance as UiInstance;
        m_CreatedUiPrefabs.Add(uiPrefab, uiInstance);
        uiInstance.DestroyAction += UnregisterUi;
    }

    public void UnregisterUi(GameObject key)
    {
        if (m_activeUi == m_CreatedUiPrefabs[key])
        {
            m_activeUi = null;
        }
        if (m_CreatedUiPrefabs.ContainsKey(key))
        {
            var uiInstance = m_CreatedUiPrefabs[key] as UiInstance;
            if (uiInstance != null)
            {
                uiInstance.DestroyAction -= UnregisterUi;
            }

            m_CreatedUiPrefabs.Remove(key);
        }
    }

    private void CloseAllUi()
    {
        foreach (var item in m_CreatedUiPrefabs)
        {
            if (item.Key != null && item.Value.UiObject != null && item.Value.UiObject.activeSelf)
            {
                item.Value.CloseUi();
            }
        }
    }

    public void OpenUi(GameObject uiPrefab)
    {
        var existingInstance = m_CreatedUiPrefabs.TryGetValue(uiPrefab, out IUiInstance ui);
        if (!existingInstance)
        {
            var instance = CreateUi(uiPrefab);
            RegisterUI(instance.gameObject, instance);
            /*          m_CreatedUiPrefabs.Add(uiPrefab, instance);
                      instance.DestroyAction += UnregisterUi;*/
            m_activeUi = instance;
        }
        else
        {
            m_activeUi = ui;
        }

        CloseAllUi();
        m_activeUi.SetPosition(Camera.main.transform.position + (Camera.main.transform.forward) * m_uiOffset);
        m_activeUi.ShowUi();
    }

    public UiInstance CreateUi(GameObject uiPrefab)
    {
        var instance = Instantiate(uiPrefab).GetComponent<UiInstance>();
        //m_CreatedUiPrefabs.Add(uiPrefab, instance);
        instance.SetTarget(Camera.main.transform);
        instance.SetPosition(Camera.main.transform.position + (Camera.main.transform.forward) * m_uiOffset);
        return instance;
    }

    private void OnDestroy()
    {
        if (Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded -= CloseAllUi;
        }
    }
}

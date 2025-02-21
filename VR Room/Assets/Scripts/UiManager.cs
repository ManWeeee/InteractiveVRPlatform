using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using SceneManagement;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private float m_uiOffset;
    private IUiInstance m_activeUi;

    private Dictionary<GameObject, IUiInstance> m_CreatedUiPrefabs = new();

    [SerializeField]
    public int NumberOfCreatedUI = 0;
    private void Awake()
    {
        Container.Register(this);
    }

    private void Start()
    {
    }

    public void RegisterUI(GameObject uiPrefab, IUiInstance instance)
    {
        if (!m_CreatedUiPrefabs.ContainsKey(uiPrefab))
        {
            m_CreatedUiPrefabs.Add(uiPrefab, instance);
            var Instance = instance as UiInstance;
            Instance.DestroyAction += UnregisterUi;
            NumberOfCreatedUI = m_CreatedUiPrefabs.Count;
        }
    }

    public void UnregisterUi(GameObject key)
    {
        Debug.Log($"Start unregistering {key} from list");
        if (m_activeUi == m_CreatedUiPrefabs[key])
        {
            Debug.Log("Reset active ui");
            m_activeUi = null;
        }
        if (m_CreatedUiPrefabs.ContainsKey(key))
        {
            var uiInstance = m_CreatedUiPrefabs[key] as UiInstance;
            
            if (uiInstance != null)
            {
                uiInstance.DestroyAction -= UnregisterUi;
            }
            Debug.Log($"Unregistering {key} from list");
            m_CreatedUiPrefabs.Remove(key);
        }
        Debug.Log($"Unregistered {key} from list");
    }

/*    private void CleanupDestroyedUi()
    {
        var destroyedKeys = m_CreatedUiPrefabs
            .Where(kvp => kvp.Value == null || kvp.Key == null) // Check if instance or key is destroyed
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var key in destroyedKeys)
        {
            m_CreatedUiPrefabs.Remove(key);
        }
    }*/

    private UniTask CloseAllUi()
    {

        foreach (var item in m_CreatedUiPrefabs)
        {
            if(item.Value == m_activeUi)
            {
                continue;
            }
            if (item.Key != null && item.Value.UiObject != null && item.Value.UiObject.activeSelf)
            {
                item.Value.CloseUi();
            }
        }
        return UniTask.CompletedTask;
    }

    public async void OpenUi(GameObject uiPrefab)
    {
        var existingInstance = m_CreatedUiPrefabs.TryGetValue(uiPrefab, out IUiInstance ui);
        if (m_activeUi != null && m_activeUi == ui)
        {
            m_activeUi.SetPosition(Camera.main.transform.position + (Camera.main.transform.forward) * m_uiOffset);
            return;
        }
        if (!existingInstance)
        {
            var instance = CreateUi(uiPrefab);
            m_activeUi = instance;
        }
        else
        {
            m_activeUi = ui;
        }
        m_activeUi.ShowUi();
        m_activeUi.SetPosition(Camera.main.transform.position + (Camera.main.transform.forward) * m_uiOffset);
        await CloseAllUi();
    }

    public UiInstance CreateUi(GameObject uiPrefab)
    {
        var obj = Instantiate(uiPrefab);
        var instance = obj.GetComponent<UiInstance>();
        RegisterUI(uiPrefab, instance);
        instance.SetTarget(Camera.main.transform);
        return instance;
    }

    private void OnDestroy()
    {
       /* if (Container.TryGetInstance<SceneLoader>(out var loader))
        {
            loader.SceneGroupManager.OnSceneGroupLoaded -= CloseAllUi;
        }*/
    }
}
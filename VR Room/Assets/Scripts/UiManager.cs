using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

    public void RegisterUI(GameObject uiPrefab, IUiInstance instance)
    {
        m_CreatedUiPrefabs.Add(uiPrefab, instance);
        var Instance = instance as UiInstance;
        Instance.DestroyAction += UnregisterUi;
        NumberOfCreatedUI = m_CreatedUiPrefabs.Count;
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

    private UniTask CloseAllUi()
    {
        foreach (var item in m_CreatedUiPrefabs)
        {
            if(item.Value == m_activeUi)
            {
                continue;
            }
            else
            {
                item.Value.CloseUi();
            }
        }
        return UniTask.CompletedTask;
    }

    public async void OpenUi(GameObject uiPrefab)
    {
        bool existingInstance = false;
        IUiInstance ui = null;
        foreach (var item in m_CreatedUiPrefabs)
        {
            if(item.Value.PrefabUiObject == uiPrefab)
            {
                existingInstance = true;
                ui = item.Value;
                Debug.Log($"Retrieved instance of {uiPrefab}: {ui.GetType()}");
                break;
            }
        }
        if (!existingInstance)
        {
            Debug.Log($"Created new instance of {uiPrefab}");
            var instance = CreateUi(uiPrefab);
            m_activeUi = instance;
        }
        else
        {
            m_activeUi = ui;
            Debug.Log($"Already have an instance of {uiPrefab}, retrieved {ui.GetType()}");
            if (m_activeUi != null && m_activeUi == ui && (m_activeUi as UiInstance).gameObject.activeSelf)
            {
                m_activeUi.SetPosition(Camera.main.transform.position + (Camera.main.transform.forward) * m_uiOffset);
                return;
            }
        }
        
        m_activeUi.ShowUi();
        m_activeUi.SetPosition(Camera.main.transform.position + (Camera.main.transform.forward) * m_uiOffset);
        await CloseAllUi();
    }

    public IUiInstance CreateUi(GameObject uiPrefab)
    {
        var obj = Instantiate(uiPrefab);
        var instance = obj.GetComponent<IUiInstance>();
        RegisterUI(obj, instance);
        instance.SetTarget(Camera.main.transform);
        instance.SetPrefab(uiPrefab);
        return instance;
    }
}
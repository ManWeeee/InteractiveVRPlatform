using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SceneManagement;
using UnityEngine.Events;

public class MenuUi : UiInstance
{
    [SerializeField] private GameObject m_modesUiPrefab;
    [SerializeField] private Button m_modesButton;
    [SerializeField] private Button m_closeUiButton;
    [SerializeField] private Button m_returnToLobbyUiButton;

    private void Start()
    {
        if(Container.TryGetInstance<SceneLoader>(out SceneLoader loader))
        {
            if(loader.SceneGroupManager.ActiveSceneGroup != loader[0])
                m_returnToLobbyUiButton.onClick.AddListener(async () => await loader.LoadSceneGroup(0));
        }
        m_closeUiButton.onClick.AddListener(m_closeUiButton.GetComponent<UiInstance>().CloseUi);
    }
}

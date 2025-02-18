using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SceneManagement;
using UnityEngine.Events;
using System;

public class MenuUi : UiInstance
{
    [SerializeField] private GameObject m_modesUiPrefab;
    [SerializeField] private Button m_modesButton;
    [SerializeField] private Button m_closeUiButton;
    [SerializeField] private Button m_returnToLobbyUiButton;
    [SerializeField] private Button m_exitGameButton;

    private void Start()
    {
        if(Container.TryGetInstance<SceneLoader>(out SceneLoader loader))
        {
            if(loader.SceneGroupManager.ActiveSceneGroup != loader[0])
                m_returnToLobbyUiButton.onClick.AddListener(async () => await loader.LoadSceneGroup(0));
        }
        m_modesButton.onClick.AddListener(ModeButtonPressed);
        m_closeUiButton.onClick.AddListener(this.CloseUi);
        m_exitGameButton.onClick.AddListener(Application.Quit);
    }

    public void ModeButtonPressed()
    {
        Container.GetInstance<UiManager>().OpenUi(m_modesUiPrefab);
    }
}

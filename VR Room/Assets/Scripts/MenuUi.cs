using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SceneManagement;
using UnityEngine.Events;
using System;
using Cysharp.Threading.Tasks.Triggers;

public class MenuUi : UiInstance
{
    [SerializeField] private GameObject m_modesUiPrefab;
    [SerializeField] private GameObject m_levelChangeUiPrefab;
    [SerializeField] private Button m_modesButton;
    [SerializeField] private Button m_levelButton;
    [SerializeField] private Button m_closeUiButton;
    [SerializeField] private Button m_returnToLobbyUiButton;
    [SerializeField] private Button m_exitGameButton;

    private UnityAction SceneLoadMethod = () => { };

    private void Start()
    {
        //base.Start();
        if(Container.TryGetInstance<SceneLoader>(out SceneLoader loader))
        {
            if (loader.SceneGroupManager.ActiveSceneGroup != loader[0])
            {
                SceneLoadMethod = async () => await loader.LoadSceneGroup(0);
                m_returnToLobbyUiButton.onClick.AddListener(SceneLoadMethod);
            }
        }
        m_modesButton.onClick.AddListener(ModeButtonPressed);
        m_closeUiButton.onClick.AddListener(this.CloseUi);
        m_exitGameButton.onClick.AddListener(Application.Quit);
        m_levelButton.onClick.AddListener(LevelButtonPressed);
    }

    public void ModeButtonPressed()
    {
        OpenUi(m_modesUiPrefab);
    }
    public void LevelButtonPressed()
    {
        OpenUi(m_levelChangeUiPrefab);
    }

    public void OpenUi(GameObject uiPrefab)
    {
        Container.GetInstance<UiManager>().OpenUi(uiPrefab);
    }
    private new void OnDestroy()
    {
        base.OnDestroy();
        m_returnToLobbyUiButton.onClick?.RemoveListener(SceneLoadMethod);
        m_modesButton.onClick.RemoveListener(ModeButtonPressed);
        m_closeUiButton.onClick.RemoveListener(this.CloseUi);
        m_exitGameButton.onClick.RemoveListener(Application.Quit);
        m_levelButton.onClick.RemoveListener(LevelButtonPressed);
    }
}

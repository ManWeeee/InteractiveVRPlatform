using SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScenePresenter : MonoBehaviour
{
    [SerializeField] private SceneManipulator m_sceneManipulator;
    [SerializeField] private TextMeshProUGUI m_description;

    private SceneData m_currentScene;

    void Awake()
    {
        m_sceneManipulator.SceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(SceneData data)
    {
        m_currentScene = data;
        UpdateUi();
    }

    private void UpdateUi()
    {
        m_description.text = m_currentScene.Description;
    }
}

using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using TMPro;
using UnityEngine;

public class SceneManipulator : MonoBehaviour
{
    [SerializeField] private SceneLoader m_sceneLoader;
    private SceneData m_chosenScene;
    private int m_index = 1;

    public Action<SceneData> SceneChanged;

    private void Start()
    {
        m_sceneLoader = Container.GetInstance<SceneLoader>();
        ChangeChosenScene(m_index);
    }

    public void NextIndex()
    {
        m_index++;
        if (m_index > m_sceneLoader.SceneGroupSize - 1)
        {
            m_index = m_sceneLoader.SceneGroupSize - 1;
        }

        ChangeChosenScene(m_index);
    }

    public void PreviousIndex()
    {
        m_index--;
        if (m_index < 1)
        {
            m_index = 1;
        }

        ChangeChosenScene(m_index);
    }

    public async void Select()
    { 
        await m_sceneLoader.LoadSceneGroup(m_index);
    }

    private void ChangeChosenScene(int index)
    {
        if (!m_sceneLoader)
        {
            return;
        }
        m_chosenScene = m_sceneLoader.GetActiveSceneFromGroup(index);
        SceneChanged?.Invoke(m_chosenScene);
    }
}

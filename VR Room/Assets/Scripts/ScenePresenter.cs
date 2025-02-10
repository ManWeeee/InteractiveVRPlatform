using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class ScenePresenter : MonoBehaviour
{
    [SerializeField] private SceneManipulator m_sceneManipulator;
    [SerializeField] private TextMeshProUGUI m_locationName;
    [SerializeField] private VideoPlayer m_videoPlayer;

    private SceneData m_currentScene;

    void Awake()
    {
        m_sceneManipulator.SceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(SceneData data)
    {
        m_currentScene = data;
        UpdateUi();
        UpdateAndPlayVideoClip();
    }

    private void UpdateUi()
    {
        m_locationName.text = "Location :" + m_currentScene.LocationName;
    }

    private void UpdateAndPlayVideoClip()
    {
        if (!m_currentScene.PreviewVideoClip)
        {
            return;
        }
        m_videoPlayer.clip = m_currentScene.PreviewVideoClip;
        PlayVideoClip();
    }

    private void PlayVideoClip()
    {
        m_videoPlayer.Play();
    }
}

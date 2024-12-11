using Cysharp.Threading.Tasks;
using SceneManagement;
using TMPro;
using UnityEngine;

public class SceneManipulator : MonoBehaviour
{
    [SerializeField] private SceneLoader m_loader;
    [SerializeField] private TextMeshProUGUI m_text;
    private int m_index = 1;

    private void Start()
    {
        m_loader = Container.GetInstance<SceneLoader>();
        m_text.text = m_index.ToString();
    }

    public void NextIndex()
    {
        m_index++;
        if (m_index > m_loader.SceneGroupSize - 1)
        {
            m_index = m_loader.SceneGroupSize - 1;
        }
        Debug.Log(m_index);
        m_text.text = m_index.ToString();
    }

    public void PreviousIndex()
    {
        m_index--;
        if (m_index < 1)
            m_index = 1;
        Debug.Log(m_index);
        m_text.text = m_index.ToString();
    }

    public async void Select()
    { 
        await m_loader.LoadSceneGroup(m_index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_uiButtonPressedSound;
    private Button m_button;

    private void Start()
    {
        m_button = GetComponent<Button>();
        if (m_button != null)
        {
            m_button.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        AudioManager.PlaySound(m_button.transform, m_uiButtonPressedSound);
    }
}

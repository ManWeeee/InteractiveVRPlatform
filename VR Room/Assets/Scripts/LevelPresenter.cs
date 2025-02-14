using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private LevelInfoHolder m_levelInfoHolder;
    [SerializeField] private TextMeshProUGUI m_levelDescription;

    private int m_index = 0;

    private void Start()
    {
        if (Container.TryGetInstance<LevelInfoHolder>(out LevelInfoHolder holder))
        {
            m_levelInfoHolder = holder;
            m_index = m_levelInfoHolder.GetLevelInfoIndex(m_levelInfoHolder.CurrentLevelInfo);
            UpdateUi();
        }
    }
    
    public void NextIndex()
    {
        m_index++;
        if (m_index > m_levelInfoHolder.LevelCount - 1)
        {
            m_index = m_levelInfoHolder.LevelCount - 1;
        }

        m_levelInfoHolder.CurrentLevelInfo = m_levelInfoHolder.GetLevelInfo(m_index);
        UpdateUi();
    }

    public void PreviousIndex()
    {
        m_index--;
        if (m_index < 0)
        {
            m_index = 0;
        }

        m_levelInfoHolder.CurrentLevelInfo = m_levelInfoHolder.GetLevelInfo(m_index);
        UpdateUi();
    }

    private void UpdateUi()
    {
        m_levelDescription.text = "Application : " + m_levelInfoHolder.CurrentLevelInfo.levelDescription;
    }
}

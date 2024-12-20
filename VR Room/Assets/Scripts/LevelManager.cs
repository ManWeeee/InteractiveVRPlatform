using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfo m_levelInfo;

    public Action LevelInfoChanged;

    public LevelInfo LevelInfo => m_levelInfo;

    private void Awake()
    {
        Container.Register(this);
    }

    public void SetLevelInfo(LevelInfo levelInfo)
    {
        m_levelInfo = levelInfo;
        LevelInfoChanged?.Invoke();
    }

    private void OnDestroy()
    {
        Container.Unregister<LevelManager>();
    }
}

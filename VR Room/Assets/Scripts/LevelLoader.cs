using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelInfo m_levelInfo;

    private void Start()
    {
        if (Container.TryGetInstance<LevelManager>(out var instance))
        {
            instance.SetLevelInfo(m_levelInfo);
            Debug.Log("Level info was given to manager successfuly");
        }
    }
}

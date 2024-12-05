using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;
using UnityEngine;

namespace SceneManagement
{
    [Serializable]
    public class SceneGroup
    {
        [SerializeField] private List<SceneData> m_scenesData;

        public List<SceneData> Scenes => m_scenesData;

        public string FindSceneByType(SceneType type)
        {
            return m_scenesData.FirstOrDefault(scene => scene.SceneType == type)?.Reference.Name;
        }
    }
    [Serializable]
    public class SceneData
    {
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }

    public enum SceneType
    {
        ActiveScene,
        Ui,
        Lobby
    }
}
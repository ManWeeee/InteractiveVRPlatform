using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneGroupManager
    {
        public Action<string> OnSceneLoaded;
        public Action<string> OnSceneUnloaded;
        public Action OnSceneGroupLoaded;
        private SceneGroup m_activeSceneGroup;

        public SceneGroup ActiveSceneGroup => m_activeSceneGroup;
        public async UniTask LoadSceneGroup(SceneGroup group)
        {
            m_activeSceneGroup = group;
            var loadedScenes = new List<string>();

            await UnloadSceneGroup();

            int sceneCount = SceneManager.sceneCount;
            
            for (int i = 0; i < sceneCount; ++i)
            {
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }

            var totalScenesToLoad = m_activeSceneGroup.Scenes.Count;

            var operationGroup = new AsyncOperationGroup(totalScenesToLoad);

            for (int i = 0; i < totalScenesToLoad; ++i)
            {
                var sceneData = m_activeSceneGroup.Scenes[i];

                var operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
                operationGroup.Operations.Add(operation);

                OnSceneLoaded?.Invoke(sceneData.Name);
            }

            while (!operationGroup.IsDone)
            {
                await UniTask.Delay(100);
            }

            Scene activeScene = SceneManager.GetSceneByName(m_activeSceneGroup.FindSceneByType(SceneType.ActiveScene));
            if (activeScene.IsValid())
            {
                SceneManager.SetActiveScene(activeScene);
            }

            OnSceneGroupLoaded?.Invoke();
        }

        private async UniTask UnloadSceneGroup()
        {
            var scenes = new List<string>();

            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                var sceneAt = SceneManager.GetSceneAt(i);
                if (!sceneAt.isLoaded)
                {
                    Debug.Log($"Scene {sceneAt.name} is not loaded fully");
                    continue;
                }

                var sceneName = sceneAt.name;
                if (sceneName == "Bootstrapper")
                {
                    continue;
                }
                scenes.Add(sceneName);
            }

            var operationGroup = new AsyncOperationGroup(scenes.Count);
            foreach (var scene in scenes)
            {
                var operation = SceneManager.UnloadSceneAsync(scene);
                if (operation == null)
                {
                    continue;
                }
                operationGroup.Operations.Add(operation);
                OnSceneUnloaded?.Invoke(scene);
            }

            while (!operationGroup.IsDone)
            {
                await UniTask.Delay(100);
            }
        }
    }

    public readonly struct AsyncOperationGroup
    {
        public readonly List<AsyncOperation> Operations;
        public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
        public bool IsDone => Operations.All(o => o.isDone);

        public AsyncOperationGroup(int initialCapacity)
        {
            Operations = new List<AsyncOperation>(initialCapacity);
        }
    }
}
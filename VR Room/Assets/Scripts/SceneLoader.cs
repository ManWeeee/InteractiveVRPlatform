using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Canvas m_loadingScreen;
        [SerializeField] private Camera m_loadingCamera;
        [SerializeField] private SceneGroup[] m_sceneGroups;

        public readonly SceneGroupManager SceneGroupManager = new();

        public int SceneGroupSize => m_sceneGroups.Length;

        private void Awake()
        {
            Container.Register(this);
            SceneGroupManager.OnSceneLoaded += sceneName => Debug.Log($"Scene {sceneName} is finished loading");
            SceneGroupManager.OnSceneUnloaded += sceneName => Debug.Log($"Scene {sceneName} is finished unloading");
            SceneGroupManager.OnSceneGroupLoaded += () => Debug.Log("All scenes are finished loading");
        }
        //TODO: Find out why the scenes are loaded and old ones do not deleted
        async void Start()
        {
            await LoadSceneGroup(0);
        }

        public async UniTask LoadSceneGroup(int index)
        {
            EnableLoadingScreen();
            await SceneGroupManager.LoadSceneGroup(m_sceneGroups[index]);
            EnableLoadingScreen(false);
        }

        private void EnableLoadingScreen(bool enable = true)
        {
            m_loadingCamera.gameObject.SetActive(enable);
            m_loadingScreen.gameObject.SetActive(enable);
        }

        public SceneData GetActiveScene(int index)
        {
            return m_sceneGroups[index].FindSceneDataByType(SceneType.ActiveScene);
        }
    }
}
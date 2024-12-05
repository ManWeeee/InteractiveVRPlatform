using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Canvas m_loadingScreen;
        [SerializeField] private Slider m_progressBar;

        private readonly string bootstrapSceneName = "BootstrapScene";

        private async UniTask LoadSceneAsync(string sceneName)
        {
            var loadOperation = SceneManager.LoadSceneAsync(sceneName);
            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f)
            {
                if (m_progressBar != null)
                    m_progressBar.value = loadOperation.progress;

                await UniTask.Yield();
            }

            loadOperation.allowSceneActivation = true;
            await UniTask.WaitUntil(() => loadOperation.isDone);
        }

        private async UniTask UnloadSceneAsync()
        {
            var scene = SceneManager.GetActiveScene();

            
            if (scene.name == "Bootstrap")
            {
                return;
            }

            if (scene.isLoaded)
            {
                var unloadOperation = SceneManager.UnloadSceneAsync(scene);
                await UniTask.WaitUntil(() => unloadOperation.isDone);
            }
        }
    }
}
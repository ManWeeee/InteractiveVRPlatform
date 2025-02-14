using Cysharp.Threading.Tasks;
using UnityEngine;
using SceneManagement;
using UnityEngine.UIElements;
public class CarFactory : MonoBehaviour 
{
    //[SerializeField] private Transform m_spawnPosition;
    private LevelInfo m_levelInfo;
    private GameObject m_currentCar;

    private SceneLoader m_sceneLoader;

    private void Start()
    {
        if (Container.TryGetInstance<SceneLoader>(out SceneLoader loader))
        {
            m_sceneLoader = loader;
            m_sceneLoader.SceneGroupManager.OnSceneGroupLoaded += CreateInstance;
        }
        if(Container.TryGetInstance<LevelInfoHolder>(out var manager))
        {
            TakeLevelInfoFrom(manager);
            //manager.LevelInfoChanged += OnLevelInfoChanged;
        }
    }

    private void TakeLevelInfoFrom(LevelInfoHolder manager)
    {
        m_levelInfo = manager.CurrentLevelInfo;
    }

    private void OnLevelInfoChanged()
    {
        var info = Container.GetInstance<LevelInfoHolder>().CurrentLevelInfo;
        CreateInstance();
    }

/*    public GameObject CreateInstance(GameObject carPrefab, Vector3 position, Quaternion rotation)
    {
        m_currentCar = Instantiate(carPrefab, position, rotation);
        return m_currentCar;
    }*/
    public void CreateInstance()
    {
        m_currentCar = Instantiate(m_levelInfo.carPrefab, transform.position, transform.rotation);
    }

    public void OnDestroy()
    {
        m_sceneLoader.SceneGroupManager.OnSceneGroupLoaded -= CreateInstance;
    }
}

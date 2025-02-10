using Cysharp.Threading.Tasks;
using UnityEngine;

public class CarFactory : MonoBehaviour 
{
    [SerializeField] private Transform m_spawnPosition;
    private GameObject m_carGameObject;

    private void Start()
    {
        if(Container.TryGetInstance<LevelInfoHolder>(out var manager))
        {
            TakeLevelInfoFrom(manager);
            //manager.LevelInfoChanged += OnLevelInfoChanged;
        }
    }

    private void TakeLevelInfoFrom(LevelInfoHolder manager)
    {
        var info = Container.GetInstance<LevelInfoHolder>().CurrentLevelInfo;
        m_carGameObject = info.carPrefab;
        CreateInstance(m_spawnPosition.position, m_spawnPosition.rotation);
    }

    private void OnLevelInfoChanged()
    {
        var info = Container.GetInstance<LevelInfoHolder>().CurrentLevelInfo;
        m_carGameObject = info.carPrefab;
        CreateInstance(m_spawnPosition.position, m_spawnPosition.rotation);
    }

    public GameObject CreateInstance(Vector3 position, Quaternion rotation)
    {
        var car = Instantiate(m_carGameObject, position, rotation);
        return car;
    }

/*    private void OnDestroy()
    {
        if (Container.TryGetInstance<LevelManager>(out var instance)) { 
            instance.LevelInfoChanged -= OnLevelInfoChanged;
        } 
    }*/
}

using Cysharp.Threading.Tasks;
using UnityEngine;

public class CarFactory : MonoBehaviour 
{
    [SerializeField] private Transform m_spawnPosition;
    private GameObject m_carGameObject;

    private void Awake()
    {
        if(Container.TryGetInstance<LevelManager>(out var manager))
        {
            manager.LevelInfoChanged += OnLevelInfoChanged;
        }
    }

    private void OnLevelInfoChanged()
    {
        var info = Container.GetInstance<LevelManager>().LevelInfo;
        m_carGameObject = info.carPrefab;
        CreateInstance(m_spawnPosition.position, m_spawnPosition.rotation);
    }

    public GameObject CreateInstance(Vector3 position, Quaternion rotation)
    {
        var car = Instantiate(m_carGameObject, position, rotation);
        return car;
    }

    private void OnDestroy()
    {
        if (Container.TryGetInstance<LevelManager>(out var instance)) { 
            instance.LevelInfoChanged -= OnLevelInfoChanged;
        } 
    }
}

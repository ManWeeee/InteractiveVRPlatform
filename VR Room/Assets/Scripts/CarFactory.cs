using Cysharp.Threading.Tasks;
using UnityEngine;
using SceneManagement;
using UnityEngine.UIElements;
public class CarFactory : MonoBehaviour 
{
    //[SerializeField] private Transform m_spawnPosition;
    private LevelInfo m_levelInfo;
    private GameObject m_currentCar;

    private LevelInfoHolder m_holder;

    private void Awake()
    {
        if(Container.TryGetInstance<LevelInfoHolder>(out var holder))
        {
            m_holder = holder;
            TakeLevelInfo(m_holder.CurrentLevelInfo);
            CreateInstance();
            /*m_holder.LevelInfoChanged += OnLevelInfoChanged;
            OnLevelInfoChanged(m_holder.CurrentLevelInfo);*/
            return;
        }
        Debug.LogError($"{this.name} was unnable to create a anything because there is no LevelInfoHolder in Container");
    }

    private void TakeLevelInfo(LevelInfo info)
    {
        m_levelInfo = info;
    }

    private void OnLevelInfoChanged(LevelInfo info)
    {
        TakeLevelInfo(info);
        CreateInstance();
    }

/*    public GameObject CreateInstance(GameObject carPrefab, Vector3 position, Quaternion rotation)
    {
        m_currentCar = Instantiate(carPrefab, position, rotation);
        return m_currentCar;
    }*/
    public void CreateInstance()
    {
        if (m_currentCar) 
        { 
            Destroy(m_currentCar);
        }
        if (m_holder.CurrentLevelInfo != null)
        {
            m_currentCar = Instantiate(m_levelInfo.carPrefab, transform.position, transform.rotation);
        }
    }

    public void OnDestroy()
    {
        if (m_holder != null)
        {
            m_holder.LevelInfoChanged -= OnLevelInfoChanged;
        }
    }
}

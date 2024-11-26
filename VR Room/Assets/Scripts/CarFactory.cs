using UnityEngine;

public class CarFactory : MonoBehaviour
{
    [SerializeField] private GameObject m_carGameObject;
    [SerializeField] private Transform m_spawnPosition;

    private void Start()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        Instantiate(m_carGameObject, m_spawnPosition);
    }
}

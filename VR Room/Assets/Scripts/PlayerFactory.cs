using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] Transform m_playerPosition;

    private void Awake()
    {
        CreateInstance(m_playerPosition.position, m_playerPosition.rotation);
    }

    public GameObject CreateInstance(Vector3 position, Quaternion rotation)
    {
        var car = Instantiate(m_playerPrefab, position, rotation);
        return car;
    }
}

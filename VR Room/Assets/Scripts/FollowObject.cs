using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private Transform m_followObject;
        [SerializeField] private float m_followDistance;

        void Update()
        {
            Follow();
        }

        private void Follow()
        {
            transform.position = m_followObject.position +
                                 new Vector3(m_followObject.forward.x, 0, m_followObject.forward.z).normalized *
                                 m_followDistance;
        }
    }
}
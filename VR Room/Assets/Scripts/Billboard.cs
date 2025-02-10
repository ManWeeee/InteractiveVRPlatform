/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        public void SetTarget(Transform transform)
        {
            m_target = transform;
        }

        private void LateUpdate()
        {
            if (!m_target) 
            {
                return;
            }

            transform.LookAt(m_target.position);
            transform.forward *= -1;
            transform.rotation
        }
    }
}*/
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform m_target;

    public void SetTarget(Transform target)
    {
        m_target = target;
    }

    private void LateUpdate()
    {
        if (!m_target)
        {
            return;
        }
        else
        {
            Vector3 targetPosition = Camera.main.transform.position;

            // Calculate the direction to the camera
            Vector3 directionToCamera = transform.position - targetPosition;

            // Make sure the UI faces the camera directly
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }  
    }
}

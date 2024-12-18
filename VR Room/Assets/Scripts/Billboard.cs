using System.Collections;
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
        }
    }
}
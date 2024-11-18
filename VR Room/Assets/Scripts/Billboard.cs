using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        private void LateUpdate()
        {
            transform.LookAt(m_target.position);
            transform.forward *= -1;
        }
    }
}
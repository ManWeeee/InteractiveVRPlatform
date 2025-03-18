using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolBeltValidation : MonoBehaviour
{
    private CharacterController m_characterController;

    private void Start()
    {
        m_characterController = GetComponentInParent<CharacterController>();
    }
    private void FixedUpdate()
    {
        
        this.transform.position = new Vector3(transform.position.x, m_characterController.center.y, transform.position.z);
    }
}

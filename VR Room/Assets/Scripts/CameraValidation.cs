using Unity.XR.CoreUtils;
using UnityEngine;

public class CameraValidation : MonoBehaviour
{
    private CharacterController m_characterController;
    private XROrigin m_xrOrigin;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_xrOrigin = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        m_characterController.height = m_xrOrigin.CameraInOriginSpaceHeight + 0.15f;

        var centerPoint = transform.InverseTransformPoint(m_xrOrigin.Camera.transform.position);

        m_characterController.center = new Vector3(
            centerPoint.x, 
            m_characterController.height / 2 + m_characterController.skinWidth,
            centerPoint.z);


        //Done to make sure we don't allow the headset to pass through physical objects, validate right physic interaction
        m_characterController.Move(new Vector3(0.001f, -0.001f, 001f));
        m_characterController.Move(new Vector3(-0.001f, 0.001f, -001f));
    }
}
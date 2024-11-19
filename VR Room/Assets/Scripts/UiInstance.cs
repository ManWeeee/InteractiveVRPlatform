using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class UiInstance : MonoBehaviour
    {
        [SerializeField] private GameObject m_uiObject;
        [SerializeField] private InputActionProperty m_actionProperty;

        void Update()
        {
            ChangeVisibility();
        }

        private void ChangeVisibility()
        {
            if (m_actionProperty.action.WasPressedThisFrame())
            {
                m_uiObject.SetActive(!m_uiObject.activeSelf);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    [RequireComponent(typeof(XRSimpleInteractable), typeof(Collider))]
    public class CarPartVR : MonoBehaviour
    {
        private Action Pressed;
        private CommandHandler m_commandHandler;
        private XRSimpleInteractable m_xrSimpleInteractable;
        private List<CarPartVR> m_dependableParts;

        public void Start()
        {
            m_commandHandler = CommandHandler.Instance;
            m_xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
            m_xrSimpleInteractable.selectExited.AddListener(OnSelectExit);
            m_dependableParts = (GetComponentsInChildren<CarPartVR>()).ToList();
        }

        private void OnSelectExit(SelectExitEventArgs args)
        {
            if (m_dependableParts.Count == 0)
            {
                HidePart();
            }
        }

        public void HidePart()
        {
            var hideCommand = new HideCommand(gameObject);
            m_commandHandler.ExecuteCommand(hideCommand);
            Pressed?.Invoke();
        }
    }
}
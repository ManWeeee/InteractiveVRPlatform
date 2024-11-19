using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    [RequireComponent(typeof(XRSimpleInteractable), typeof(Collider))]
    public class CarPartVR : MonoBehaviour
    {
        private UnityAction Pressed;
        private CommandHandler m_commandHandler;
        [SerializeField] private XRSimpleInteractable m_xrSimpleInteractable;

        public void Start()
        {
            m_commandHandler = CommandHandler.Instance;
            m_xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
            m_xrSimpleInteractable.selectEntered.AddListener(OnSelectEntered);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            HidePart();
        }

        public void HidePart()
        {
            var hideCommand = new HideCommand(gameObject);
            m_commandHandler.ExecuteCommand(hideCommand);
            Pressed?.Invoke();
        }
    }
}
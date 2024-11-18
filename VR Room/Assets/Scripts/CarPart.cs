using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class CarPart : MonoBehaviour
    {
        public Action Pressed;
        private CommandHandler m_commandHandler;


        public void Start()
        {
            m_commandHandler = CommandHandler.Instance;
        }

        public void HideOnPressed()
        {
            var hideCommand = new HideCommand(gameObject);
            m_commandHandler.ExecuteCommand(hideCommand);
        }
    }
}
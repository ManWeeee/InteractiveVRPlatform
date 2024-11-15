using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class HideCommand : ICommand
    {
        private GameObject m_object;

        public HideCommand(GameObject obj)
        {
            m_object = obj;
        }

        public void Execute()
        {
            m_object.SetActive(false);
        }

        public void Undo()
        {
            m_object.SetActive(true);
        }
    }
}
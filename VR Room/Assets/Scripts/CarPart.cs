using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public abstract class CarPart : MonoBehaviour, IAssemblyPart
    {
        [SerializeField] protected AnimationHandler m_animationHandler;

        protected virtual void Start()
        {
            m_animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        protected const string DISASSEMBLE_ANIMATION = "Disassemble";
        protected const string ASSEMBLE_ANIMATION = "Assemble";

        public abstract Task StartAssemble();

        public abstract Task StartDisassemble();
    }
}
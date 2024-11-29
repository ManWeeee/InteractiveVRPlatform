using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts
{
    public abstract class CarPart : MonoBehaviour, IAssemblyPart
    {
        [SerializeField] protected AnimationHandler m_animationHandler;
        [SerializeField] protected List<Part> m_parentParts;
        [SerializeField] protected List<Part> m_dependableParts;

        protected const string DISASSEMBLE_ANIMATION_NAME = "Disassemble";
        protected const string ASSEMBLE_ANIMATION_NAME = "Assemble";

        protected Action<Part> Disassembled;

        public List<Part> ReadOnlyParentPartsList => m_parentParts;

        public bool HasDependableParts => m_dependableParts.Count > 0;


        protected virtual void Awake()
        {
            m_animationHandler = GetComponentInChildren<AnimationHandler>();
        }


        public abstract Task StartAssemble();

        public abstract Task StartDisassemble();
    }
}
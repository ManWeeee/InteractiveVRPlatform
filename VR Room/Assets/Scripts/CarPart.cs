using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


namespace Assets.Scripts
{
    public abstract class CarPart : MonoBehaviour, IAssemblyPart
    {
        [SerializeField] protected AnimationHandler m_animationHandler;
        [SerializeField] protected List<Part> m_parentParts;
        [SerializeField] protected List<Part> m_dependableParts;
        [SerializeField] protected bool m_isBroken = false;
        [SerializeField] CarPartType m_partType;

        protected const string DISASSEMBLE_ANIMATION_NAME = "Disassemble";
        protected const string ASSEMBLE_ANIMATION_NAME = "Assemble";

        public Action<Part> Disassembled;
        public Action<Part> Assembled;

        public List<Part> ReadOnlyParentPartsList => m_parentParts;

        public bool CanBeAssembled
        {
            get
            {
                return m_parentParts != null && m_parentParts.All(part => part.gameObject.activeSelf);
            }
        }
        public bool HasDependableParts => m_dependableParts.Count > 0;
        public bool IsBroken 
        {
            get => m_isBroken;
            set => m_isBroken = value;
        }
        public CarPartType GetCarPartType => m_partType;

        protected virtual void Awake()
        {
            m_animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        public List<CarPart> GetAllDependableParts()
        {
            List<CarPart> parts = new();
            parts.Add(this);
            CollectAllPartsRecursive(this, parts);
            return parts;
        }

        private void CollectAllPartsRecursive(CarPart part, List<CarPart> parts)
        {
            foreach (var dependablePart in part.m_dependableParts)
            {
                if (!parts.Contains(dependablePart)) // Avoid duplicates
                {
                    parts.Add(dependablePart);
                    CollectAllPartsRecursive(dependablePart, parts); // Recurse into dependencies
                }
            }
        }

        public abstract Task StartAssemble();

        public abstract Task StartDisassemble();
    }
}
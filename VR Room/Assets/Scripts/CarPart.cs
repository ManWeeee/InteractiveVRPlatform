using Assets.Scripts;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts
{
    public abstract class CarPart : MonoBehaviour, IAssemblyPart
    {
        [SerializeField] protected List<CarPart> m_parentParts;
        [SerializeField] protected List<CarPart> m_dependableParts;
        [SerializeField] protected PartInfo m_partInfo;

        protected CarPartAnimator m_animator;
        protected PartState m_currentState;

        public List<CarPart> ReadOnlyParentPartsList => m_parentParts;
        public bool HasDependableParts => m_dependableParts.Count > 0;

        public PartInfo PartInfo
        {
            get => m_partInfo;
            set => m_partInfo = value;
        }

        public bool CanBeAssembled
        {
            get
            {
                return m_parentParts != null && m_parentParts.All(part => part.gameObject.activeSelf);
            }
        }

        public Action<CarPart> Disassembled;
        public Action<CarPart> Assembled;

        protected virtual void Awake()
        {
            m_animator = new(GetComponentInChildren<AnimationHandler>());
            if (m_partInfo != null)
            {
                GetComponentInChildren<MeshFilter>().mesh = m_partInfo.PartMesh;
            }
        }

        public List<CarPart> GetAllDependableParts()
        {
            List<CarPart> parts = new();
            parts.Add(this);
            CollectAllDependablePartsRecursive(this, parts);
            return parts;
        }

        private void CollectAllDependablePartsRecursive(CarPart part, List<CarPart> parts)
        {
            foreach (var dependablePart in part.m_dependableParts)
            {
                if (!parts.Contains(dependablePart)) // Avoid duplicates
                {
                    parts.Add(dependablePart);
                    CollectAllDependablePartsRecursive(dependablePart, parts);
                }
            }
        }

        public virtual void SetParent(CarPart parent)
        {
            m_parentParts.Add(parent);
            Disassembled += parent.ReleaseChildren;
            Assembled += parent.SetChildren;
        }

        public virtual void SetChildren(CarPart partInteractable)
        {
            m_dependableParts.Add(partInteractable);
        }

        public virtual void ReleaseChildren(CarPart partInteractable)
        {
            m_dependableParts.Remove(partInteractable);
        }

        public abstract UniTask StartAssemble();

        public abstract UniTask Assemble();

        public abstract UniTask StartDisassemble();
    }
}

public class PartState
{
    protected bool m_isBroken = false;
    public bool IsBroken
    {
        get => m_isBroken;
        set => m_isBroken = value;
    }
}

public class CarPartAnimator
{
    [SerializeField] protected AnimationHandler m_animationHandler;

    public AnimationHandler AnimationHandler => m_animationHandler;
    public CarPartAnimator(AnimationHandler animationHandler)
    {
        m_animationHandler = animationHandler;
    }

    private const string DISASSEMBLE_ANIMATION_NAME = "Disassemble";
    private const string ASSEMBLE_ANIMATION_NAME = "Assemble";

    public string AssembleAnimationName => ASSEMBLE_ANIMATION_NAME;
    public string DisassembleAnimationName => DISASSEMBLE_ANIMATION_NAME;
}
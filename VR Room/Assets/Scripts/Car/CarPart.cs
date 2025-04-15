
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Tutorials;
using UnityEngine;

public abstract class CarPart : MonoBehaviour, IAssemblyPart, ITutorialProvider {
    [SerializeField] private List<CarPart> m_parentParts;
    [SerializeField] private List<CarPart> m_dependableParts;
    [SerializeField] private PartInfo m_partInfo;

    private CarPartAnimator m_animator;

    public List<CarPart> ReadOnlyParentPartsList => m_parentParts;
    public bool HasDependableParts => m_dependableParts.Count > 0;
    public bool disassembled = false;
    public PartInfo PartInfo {
        get => m_partInfo;
        set => m_partInfo = value;
    }

    public bool CanBeAssembled {
        get {
            return m_parentParts != null && m_parentParts.All(part => part.gameObject.activeSelf);
        }
    }

    public Action<CarPart> Disassembled;
    public Action<CarPart> Assembled;

    protected virtual void Awake() {
        m_animator = new(GetComponentInChildren<AnimationHandler>());
        if(m_partInfo != null) {
            GetComponentInChildren<MeshFilter>().mesh = m_partInfo.PartMesh;
        }
        if(!HasDependableParts) {
            return;
        }
        foreach(var part in m_dependableParts) {
            part.SetParent(this);
        }
    }

    public List<CarPart> GetAllDependableParts() {
        List<CarPart> parts = new();
        parts.Add(this);
        CollectAllDependablePartsRecursive(this, parts);
        return parts;
    }

    private void CollectAllDependablePartsRecursive(CarPart part, List<CarPart> parts) {
        foreach(var dependablePart in part.m_dependableParts) {
            if(!parts.Contains(dependablePart)) // Avoid duplicates
            {
                parts.Add(dependablePart);
                CollectAllDependablePartsRecursive(dependablePart, parts);
            }
        }
    }

    public virtual void SetParent(CarPart parent) {
        m_parentParts.Add(parent);
        Disassembled += parent.ReleaseChildren;
        Assembled += parent.SetChildren;
    }

    public virtual void SetChildren(CarPart partInteractable) {
        m_dependableParts.Add(partInteractable);
    }

    public virtual void ReleaseChildren(CarPart partInteractable) {
        m_dependableParts.Remove(partInteractable);
    }

    public virtual async UniTask StartAssemble() {
        if(!CanBeAssembled) {
            return;
        }
        await Assemble();
    }

    public virtual async UniTask Assemble() {
        if(m_animator.AnimationHandler) {
            await m_animator.AnimationHandler.PlayAnimationAndWait(m_animator.AssembleAnimationName);
        }
        disassembled = false;
        Assembled?.Invoke(this);
    }

    public virtual async UniTask StartDisassemble() {
        if(HasDependableParts) {
            return;
        }
        await Disassemble();
    }

    private async UniTask Disassemble() {
        if(m_animator.AnimationHandler) {
            await m_animator.AnimationHandler.PlayAnimationAndWait(m_animator.DisassembleAnimationName);
        }

        Disassembled?.Invoke(this);
        disassembled = true;
        gameObject.SetActive(false);
        /*        var command = new HideCommand(gameObject);
                CommandHandler.ExecuteCommand(command);*/
    }

    public virtual TutorialStep GetTutorialStep() {
        if(disassembled) {
            return new TutorialStep($"Assemble {gameObject.name}", new AssembledCondition(this));
        }
        return new TutorialStep($"Disassemble {gameObject.name}", new DisassembledCondition(this));
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
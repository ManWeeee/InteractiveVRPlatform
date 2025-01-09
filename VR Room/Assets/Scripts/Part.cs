using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEngine;

public class Part : CarPart
{
    [SerializeField] private PartInfo m_partInfo;
    public PartInfo PartInfo
    {
        get => m_partInfo;
        private set => m_partInfo = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if (m_partInfo != null)
        {
            GetComponentInChildren<MeshFilter>().mesh = m_partInfo.PartMesh;
        }

        if (!HasDependableParts)
        {
            return;
        }
        foreach (var part in m_dependableParts)
        {
            part.SetParent(this);
        }
    }

    public void SetParent(Part parent)
    {
        m_parentParts.Add(parent);
        Disassembled += parent.ReleaseChildren;
        Assembled += parent.SetChildren;
    }

    public void SetChildren(Part partInteractable)
    {
        m_dependableParts.Add(partInteractable);
    }

    public void ReleaseChildren(Part partInteractable)
    {
        m_dependableParts.Remove(partInteractable);
    }

    public override async Task StartAssemble()
    {
        if (!CanBeAssembled)
        {
            return;
        }
        await Assemble();
    }


    private async Task Assemble()
    {
        if (m_animationHandler)
        {
            await m_animationHandler.PlayAnimationAndWait(ASSEMBLE_ANIMATION_NAME);
        }
        Assembled?.Invoke(this);
    }

    public override async Task StartDisassemble()
    {
        if (HasDependableParts)
        {
            return;
        }
        await Disassemble();
    }

    private async Task Disassemble()
    {
        if (m_animationHandler)
        {
            await m_animationHandler.PlayAnimationAndWait(DISASSEMBLE_ANIMATION_NAME);
        }

        Disassembled?.Invoke(this);
        gameObject.SetActive(false);
/*        var command = new HideCommand(gameObject);
        CommandHandler.ExecuteCommand(command);*/
    }
}
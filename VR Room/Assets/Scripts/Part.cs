using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEngine;

public class Part : CarPart
{
    [SerializeField] private PartInfo m_partInfo;

    private AudioSource m_audioSource;

    protected override void Awake()
    {
        base.Awake();
        m_audioSource = GetComponent<AudioSource>();
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
        await Assemble();
    }


    private async Task Assemble()
    {
        //m_audioSource.PlayOneShot(m_partInfo.AssembleAudioClip);
        await m_animationHandler.PlayAnimationAndWait(ASSEMBLE_ANIMATION_NAME);
    }

    public override async Task StartDisassemble()
    {
        await Disassemble();
    }

    private async Task Disassemble()
    {
        if (HasDependableParts)
        {
            return;
        }

        /*if (m_partInfo)
        {
            m_audioSource.PlayOneShot(m_partInfo.DisassembleAudioClip);
        }*/

        if (m_animationHandler)
        {
            await m_animationHandler.PlayAnimationAndWait(DISASSEMBLE_ANIMATION_NAME);
        }

        Disassembled?.Invoke(this);
        var command = new HideCommand(gameObject);
        CommandHandler.ExecuteCommand(command);
    }
}
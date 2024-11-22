using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(AudioSource))]
public class Part : CarPart
{
    [SerializeField] private PartInfo m_partInfo;
    
    private AudioSource m_audioSource;

    protected override void Start()
    {
        base.Start();
        m_audioSource = GetComponent<AudioSource>();
    }

    public override async Task StartAssemble()
    {
        await Assemble();
    }

    private async Task Assemble()
    {
        m_audioSource.PlayOneShot(m_partInfo.AssembleAudioClip);
        await m_animationHandler.PlayAnimationAndWait(ASSEMBLE_ANIMATION_NAME);
    }

    public override async Task StartDisassemble()
    {
        await Disassemble();
    }
    private async Task Disassemble()
    {
        m_audioSource.PlayOneShot(m_partInfo.DisassembleAudioClip);
        await m_animationHandler.PlayAnimationAndWait(DISASSEMBLE_ANIMATION_NAME);
        var command = new HideCommand(gameObject);
        CommandHandler.ExecuteCommand(command);
    }
}
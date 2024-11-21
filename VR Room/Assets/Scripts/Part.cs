using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEngine;

public class Part : CarPart
{

    public override async Task StartAssemble()
    {
        await Assemble();
    }

    private async Task Assemble()
    {
        await m_animationHandler.PlayAnimationAndWait(ASSEMBLE_ANIMATION);
    }

    public override async Task StartDisassemble()
    {
        await Disassemble();
    }
    private async Task Disassemble()
    {
        await m_animationHandler.PlayAnimationAndWait(DISASSEMBLE_ANIMATION);
        var command = new HideCommand(gameObject);
        CommandHandler.ExecuteCommand(command);
    }
}

using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Tutorials;
using UnityEngine;

public class Wheel : CarPart {
    public override async UniTask StartAssemble() {
        if(!CanBeAssembled) {
            return;
        }
        await Assemble();
    }

    public override async UniTask Assemble() {
        if(m_animator.AnimationHandler) {
            await m_animator.AnimationHandler.PlayAnimationAndWait(m_animator.AssembleAnimationName);
        }
        disassembled = false;
        Assembled?.Invoke(this);
    }

    public override async UniTask StartDisassemble() {
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

    public override TutorialStep GetTutorialStep() {
        return new TutorialStep($"Disassemble {gameObject.name}", new DisassembledCondition(this));
    }
}

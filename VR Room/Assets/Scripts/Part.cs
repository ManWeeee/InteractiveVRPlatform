using System.Net;
using System.Threading.Tasks;
using Assets.Scripts;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Part : CarPart
{
    //private void OnDrawGizmos()
    //{
    //    if (m_partInfo && m_partInfo.GetCarPartType == CarPartType.Bolt )
    //        return;

    //    if (m_dependableParts.Count == 0)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(transform.position, 0.01f);
    //    }
    //}
    protected override void Awake()
    {
        base.Awake();
        if (!HasDependableParts)
        {
            return;
        }
        foreach (var part in m_dependableParts)
        {
            part.SetParent(this);
        }
    }

    public override async UniTask StartAssemble()
    {
        if (!CanBeAssembled)
        {
            return;
        }
        await Assemble();
    }

    public override async UniTask Assemble()
    {
        if (m_animator.AnimationHandler)
        {
            await m_animator.AnimationHandler.PlayAnimationAndWait(m_animator.AssembleAnimationName);
        }
        Assembled?.Invoke(this);
    }

    public override async UniTask StartDisassemble()
    {
        if (HasDependableParts)
        {
            return;
        }
        await Disassemble();
    }

    private async UniTask Disassemble()
    {
        if (m_animator.AnimationHandler)
        {
            await m_animator.AnimationHandler.PlayAnimationAndWait(m_animator.DisassembleAnimationName);
        }

        Disassembled?.Invoke(this);
        gameObject.SetActive(false);
        /*        var command = new HideCommand(gameObject);
                CommandHandler.ExecuteCommand(command);*/
    }
}

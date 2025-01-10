using System.Threading.Tasks;
using Assets.Scripts;

public class Part : CarPart
{
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
        if (m_animator.AnimationHandler)
        {
            await m_animator.AnimationHandler.PlayAnimationAndWait(m_animator.AssembleAnimationName);
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

using UnityEngine;

[CreateAssetMenu(menuName = "Disassembly Strategies/Twist Strategy")]
public class TwistStrategySO : DisassemblyStrategySO {
    [SerializeField] private float requiredAngle = 180f;


    private float accumulatedAngle;
    private CarPartInteractable target;
    private ToolInteractor tool;
    private bool isDisassembling;

    public override void Initialize(CarPartInteractable target, ToolInteractor tool) {
        this.target = target;
        this.tool = tool;
        accumulatedAngle = 0f;
        isDisassembling = target.CarPart.disassembled == false;
    }

    public override void ProcessDelta(float angleDegrees) {
        bool isCorrectDirection = isDisassembling
            ? angleDegrees < 0
            : angleDegrees > 0;

        if(!isCorrectDirection)
            return;

        accumulatedAngle += Mathf.Abs(angleDegrees);
        float progress = Mathf.Clamp01(accumulatedAngle / requiredAngle);
        target.UpdateProgress(progress);
    }

    public override bool CheckIsComplete() {
        return accumulatedAngle >= requiredAngle;
    }

    public override void Finish() {
        if(isDisassembling)
            target.CarPart.Disassemble();
        else
            target.CarPart.Assemble();
    }

    public override void Cancel() {
        target.UpdateProgress(0);
    }
}
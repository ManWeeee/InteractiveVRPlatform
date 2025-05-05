using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class DisassemblyStrategySO : ScriptableObject {
    public abstract void Initialize(CarPartInteractable target, ToolInteractor tool);
    public abstract void ProcessDelta(float delta);
    public abstract bool CheckIsComplete();
    public abstract void Finish();
    public abstract void Cancel();
}
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ToolInteractor : XRDirectInteractor {
    [SerializeField] private DisassemblyStrategySO strategy;
    [SerializeField] private List<CarPartType> supportedTypes;

    private CarPartInteractable currentTarget;
    private bool isActive;

    private XRBaseInteractor controller;
    private Quaternion lastRotation;

    public List<CarPartType> SupportedTypes => supportedTypes;

    protected override void Awake() {
        base.Awake();
        var grabInteractable = GetComponentInParent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
        grabInteractable.activated.AddListener(OnActivated);
        enabled = false;
    }

    private void OnGrabbed(SelectEnterEventArgs args) {
        controller = args.interactorObject.transform.GetComponent<XRBaseInteractor>();
        lastRotation = controller.transform.rotation;
        enabled = true;
    }

    private void OnReleased(SelectExitEventArgs args) {
        enabled = false;
        controller = null;
        if(currentTarget != null) {
            strategy?.Cancel();
            currentTarget = null;
            isActive = false;
        }
    }

    private void OnActivated(ActivateEventArgs args) {
        if(interactablesHovered.Count == 0 || strategy == null) return;

        foreach(var hovered in interactablesHovered) {
            var part = hovered as CarPartInteractable;
            if(part == null) continue;
            if(!supportedTypes.Contains(part.CarPartType)) continue;

            currentTarget = part;
            strategy.Initialize(part, this);
            isActive = true;
            break;
        }
    }

    public void Update() {
        if(!isActive || currentTarget == null || controller == null) return;

        Quaternion currentRotation = controller.transform.rotation;

        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        float signedAngle = Vector3.Dot(axis, currentTarget.transform.up) > 0 ? angle : -angle;

        strategy.ProcessDelta(signedAngle); 

        lastRotation = currentRotation;

        if(strategy.CheckIsComplete()) {
            strategy.Finish();
            isActive = false;
            currentTarget = null;
        }
    }

    protected override void OnDestroy() {
        var grabInteractable = GetComponentInParent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
        grabInteractable.activated.RemoveListener(OnActivated);
        base.OnDestroy();
    }
}
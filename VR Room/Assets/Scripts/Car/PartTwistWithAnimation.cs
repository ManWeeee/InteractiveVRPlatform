using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*[RequireComponent(typeof(CarPartInteractable))]
public class PartTwistWithAnimation : MonoBehaviour {
    [Header("Twist Settings")]
    public Vector3 twistAxis = Vector3.up;
    public float requiredAngle = 360f;

    [Header("Animation State Names")]
    public string disassembleStateName = "Disassemble";
    public string assembleStateName = "Assemble";

    private CarPartInteractable _interactable;
    private Animator _animator;
    private CarPart _carPart;

    private ToolInteractor _toolInteractor;
    private XRBaseInteractor _interactor;
    private FixedJoint _lockJoint;

    private float _accumulatedAngle;
    private bool _isTwisting;
    private string _currentState;
    private Quaternion _lastRotation;

    private void Awake() {
        _interactable = GetComponent<CarPartInteractable>();
        _animator = GetComponentInChildren<Animator>();
        _carPart = GetComponent<CarPart>();
    }

    private void OnEnable() {
        Debug.Log($"{this} OnEnabled");
        _interactable.activated.AddListener(OnActivated);
        _interactable.deactivated.AddListener(OnDeactivated);
    }

    private void OnDisable() {
        _interactable.activated.RemoveListener(OnActivated);
        _interactable.deactivated.RemoveListener(OnDeactivated);
    }

    private void OnActivated(ActivateEventArgs args) {
        var tool = args.interactorObject;
        if (tool == null) {
            Debug.LogWarning("Interactor is not a tool");
            return;
        }
        //Debug.Log("Interactor is a tool");
        _toolInteractor = tool as ToolInteractor;
        _interactor = tool as ToolInteractor;
        _lastRotation = tool.transform.rotation;
        _accumulatedAngle = 0f;
        _isTwisting = true;

        bool willDisassemble = !_carPart.disassembled;
        _currentState = willDisassemble ? disassembleStateName : assembleStateName;

        LockTool();

        _animator.speed = 0f;
        _animator.Play(_currentState, 0, 0f);
      
    }

    private void OnDeactivated(DeactivateEventArgs args) {
        if((Object)args.interactorObject != _interactor) return;
        _isTwisting = false;
        UnlockTool();
        _animator.speed = 1f;
        Debug.Log("We deactivated");
    }

    void Update() {
        if(!_isTwisting || _interactor == null) return;

        Quaternion current = _interactor.transform.rotation;
        Quaternion diff = current * Quaternion.Inverse(_lastRotation);
        diff.ToAngleAxis(out float angle, out Vector3 axis);
        _lastRotation = current;

        float dir = !_carPart.disassembled ? -1f : 1f;
        float sign = Vector3.Dot(axis.normalized, twistAxis.normalized) >= 0 ? 1f : -1f;
        float delta = angle * sign * dir;

        if(delta > 0f) {
            _accumulatedAngle = Mathf.Min(_accumulatedAngle + delta, requiredAngle);
            float normalized = _accumulatedAngle / requiredAngle;
            _animator.Play(_currentState, 0, normalized);

            if(normalized >= 1f) FinishTwist();
        }
        Debug.Log("We are rotating");
    }

    private void FinishTwist() {
        _isTwisting = false;
        UnlockTool();
        _animator.speed = 1f;

        if(_carPart.disassembled) _carPart.Assemble(); else _carPart.Disassemble();
        Debug.Log("We finish twist");
    }

    private void LockTool() {
        if(_lockJoint != null) return;
        var toolGrab = _toolInteractor.GetComponentInParent<XRGrabInteractable>();
        if(toolGrab == null) return;
        var toolRb = toolGrab.GetComponent<Rigidbody>();
        var partRb = GetComponent<Rigidbody>();
        if(toolRb == null || partRb == null) return;

        _lockJoint = toolRb.gameObject.AddComponent<FixedJoint>();
        _lockJoint.connectedBody = partRb;
        Debug.Log("We locked the tool");
    }

    private void UnlockTool() {
        if(_lockJoint != null) Destroy(_lockJoint);
        _lockJoint = null;
        Debug.Log("We unlocked tool");
    }
}
*/
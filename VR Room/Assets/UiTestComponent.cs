#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class UiTestComponent : MonoBehaviour {
    public Button button;

    private void Start() {
        button = GetComponent<Button>();
    }
    public void SimulateClick() {
        button?.onClick.Invoke();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UiTestComponent))]
public class UiTestComponentEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        UiTestComponent script = (UiTestComponent)target;
        if(GUILayout.Button("Simulate UI Button Click")) {
            script.SimulateClick();
        }
    }
}
#endif
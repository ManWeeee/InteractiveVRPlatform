using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public interface IUiInstance
    {
        public GameObject UiObject { get; }

        public abstract void UpdateUi();

        public abstract void ShowUi();

        public abstract void CloseUi();

        public abstract void SetPosition(Vector3 position);

        public abstract void SetTarget(Transform objectTransform);
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public interface IUiInstance
    {
        public GameObject UiObject { get; }

        public abstract void UpdateUi();

        public void SetPosition(Vector3 position);
    }
}

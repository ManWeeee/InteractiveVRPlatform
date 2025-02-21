using Cysharp.Threading.Tasks;
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

        public abstract UniTask FadeIn(float duration = 0.5f);

        public abstract UniTask FadeOut(float duration = 0.5f);

        public abstract UniTask FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration);
    }
}

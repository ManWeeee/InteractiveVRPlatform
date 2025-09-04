using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IUiInstance {
    public GameObject PrefabUiObject { get; }

    public abstract void UpdateUi();

    public abstract void ShowUi();

    public abstract void CloseUi();

    public abstract void SetPosition(Vector3 position);

    public abstract void SetTarget(Transform objectTransform);

    public abstract void SetPrefab(GameObject prefab);

    public abstract UniTask FadeIn(float duration = 0.5f);

    public abstract UniTask FadeOut(float duration = 0.5f);

    public abstract UniTask FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration);
}

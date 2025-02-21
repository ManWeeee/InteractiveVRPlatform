
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UiInstance : MonoBehaviour, IUiInstance
{
    private Transform m_target;
    private CanvasGroup m_canvasGroup;

    public Action<GameObject> DestroyAction;

    public GameObject UiObject
    {
        get;
        private set;
    }

    protected virtual void Awake()
    {
        UiObject = this.gameObject;
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void UpdateUi()
    {
        return;
    }

    public virtual void ShowUi()
    {
        FadeIn();
    }

    public virtual void CloseUi()
    {
        FadeOut();
    }

    public virtual void SetPosition(Vector3 position)
    {
        UiObject.transform.position = position;
    }

    public virtual void SetTarget(Transform objectTransform)
    {
        if (TryGetComponent<Billboard>(out Billboard billboard))
        {
            m_target = objectTransform;
            billboard.SetTarget(m_target);
        }
    }

    public async UniTask FadeIn(float duration = 0.5f)
    {
        if (m_canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup is missing!");
            return;
        }

        gameObject.SetActive(true); // Ensure UI is active before fading in
        m_canvasGroup.interactable = true;
        m_canvasGroup.blocksRaycasts = true;

        await FadeCanvasGroup(m_canvasGroup, 0f, 1f, duration);
    }

    public async UniTask FadeOut(float duration = 0.5f)
    {
        if (m_canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup is missing!");
            gameObject.SetActive(false);
            return;
        }

        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;

        await FadeCanvasGroup(m_canvasGroup, 1f, 0f, duration);
        gameObject.SetActive(false); // Disable UI after fading out
    }

    public async UniTask FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            await UniTask.Yield(PlayerLoopTiming.Update); // Non-blocking async wait
        }

        canvasGroup.alpha = targetAlpha;
    }


    protected void OnDestroy()
    {
        DestroyAction?.Invoke(UiObject);
    }
}

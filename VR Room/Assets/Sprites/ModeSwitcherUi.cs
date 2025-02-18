using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcherUi : UiInstance
{
    public override void ShowUi()
    {
        base.ShowUi();
        FadeInAnimation();
    }

    private void FadeInAnimation()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 0.5f);
        }
    }
}

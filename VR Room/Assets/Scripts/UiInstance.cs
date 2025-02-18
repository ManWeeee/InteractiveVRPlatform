
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using DG.Tweening;
using UnityEngine;

public class UiInstance : MonoBehaviour, IUiInstance
{
    private Transform m_target;

    public Action<GameObject> DestroyAction;

    public GameObject UiObject
    {
        get;
        private set;
    }

    protected virtual void Awake()
    {
        UiObject = this.gameObject;
    }

    public virtual void UpdateUi()
    {
        return;
    }

    public virtual void ShowUi()
    {
        UiObject.SetActive(true);
    }

    public virtual void CloseUi()
    {
        UiObject?.SetActive(false);
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


    private void OnDestroy()
    {
        DestroyAction?.Invoke(UiObject);
    }
}

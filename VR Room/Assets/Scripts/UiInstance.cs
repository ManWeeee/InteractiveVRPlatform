
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class UiInstance : MonoBehaviour, IUiInstance
{
    public Action<UiInstance> DestroyAction;
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

    private void OnDestroy()
    {
        DestroyAction?.Invoke(this);
    }
}

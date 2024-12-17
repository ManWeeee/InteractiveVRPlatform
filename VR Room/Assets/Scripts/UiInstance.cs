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

    private void Start()
    {
        UiObject = this.gameObject;
    }

    public void UpdateUi()
    {
        throw new System.NotImplementedException();
    }

    public void SetPosition(Vector3 position)
    {
        UiObject.transform.position = position;
    }

    private void OnDestroy()
    {
        DestroyAction?.Invoke(this);
    }
}

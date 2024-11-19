using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IXRSimpleInteractable
{
    public void OnHoverEntered(HoverEnterEventArgs args);

    public void OnHoverExited(HoverExitEventArgs args);

    public void OnSelectEntered(SelectEnterEventArgs args);

    public void OnSelectExited(SelectExitEventArgs args);
}

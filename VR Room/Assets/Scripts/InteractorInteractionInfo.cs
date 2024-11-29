using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorInteractionInfo : MonoBehaviour
{
    [SerializeField] private HoverMaterials m_materials;

    public HoverMaterials GetHoverMaterials => m_materials;
}

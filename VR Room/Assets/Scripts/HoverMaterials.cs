using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/HoverMaterials", fileName = "NewHoverMaterials", order = 1)]
[Serializable]
public class HoverMaterials : ScriptableObject {
    [SerializeField] private Material m_rightMaterial;
    [SerializeField] private Material m_wrongMaterial;
    [SerializeField] private Material m_wrongToolMaterial;

    public Material ReadonlyRightMaterial => m_rightMaterial;
    public Material ReadonlyWrongMaterial => m_wrongMaterial;
    public Material ReadonlyWrongToolMaterial => m_wrongToolMaterial;

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObject/HoverMaterials", fileName = "NewHoverMaterials", order = 1)]
    [Serializable]
    public class HoverMaterials : ScriptableObject
    {
        [SerializeField] private Material m_rightMaterial;
        [SerializeField] private Material m_wrongMaterial;

        public Material ReadonlyRightMaterial => m_rightMaterial;
        public Material ReadonlyWrongMaterial => m_wrongMaterial;

    }
}
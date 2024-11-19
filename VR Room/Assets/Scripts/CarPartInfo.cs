using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScribtableObject/CarPartInfo", fileName = "NewCarPartInfo", order = 1)]
    [Serializable]
    public class CarPartInfo : ScriptableObject
    {
        [SerializeField] private Material m_rightMaterial;
        [SerializeField] private Material m_wrongMaterial;

        public Material ReadonlyRightMaterial => m_rightMaterial;
        public Material ReadonlyWrongMaterial => m_wrongMaterial;

    }
}
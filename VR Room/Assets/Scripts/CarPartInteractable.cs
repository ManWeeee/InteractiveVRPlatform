using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class CarPartInteractable : MonoBehaviour, IXRSimpleInteractable
    {
        [SerializeField] private HoverMaterials m_materials;
        [SerializeField] private CarPart m_part;

        private Renderer m_renderer;
        private Material m_originalMaterial;
        private XRSimpleInteractable m_interactable;

        public UnityAction<HoverEnterEventArgs> HoverEntered;
        public UnityAction<HoverExitEventArgs> HoverExited;

        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            m_part = GetComponent<CarPart>();
            m_interactable = GetComponent<XRSimpleInteractable>();
            m_renderer = GetComponentInChildren<Renderer>();
            if (m_renderer != null)
            {
                m_originalMaterial = m_renderer.material;
            }
        }

        private void OnEnable()
        {
            m_interactable.hoverEntered.AddListener(OnHoverEntered);
            m_interactable.hoverExited.AddListener(OnHoverExited);
            m_interactable.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            m_interactable.hoverEntered.RemoveListener(OnHoverEntered);
            m_interactable.hoverExited.RemoveListener(OnHoverExited);
        }
        
        public void OnHoverEntered(HoverEnterEventArgs args)
        {
            if (!m_renderer)
            {
                return;
            }

            if(m_part.HasDependableParts)
            {
                m_renderer.material = m_materials.ReadonlyWrongMaterial;
                HoverEntered?.Invoke(args);
            }
            else
            {
                m_renderer.material = m_materials.ReadonlyRightMaterial;
            }
        }

        public void OnHoverExited(HoverExitEventArgs args)
        {
            if (m_renderer == null)
            {
                return;
            }
            m_renderer.material = m_originalMaterial;
            HoverExited?.Invoke(args);
        }

        public void OnSelectEntered(SelectEnterEventArgs args)
        {
            
            return;
        }

        public void OnSelectExited(SelectExitEventArgs args)
        {
            if (!m_part.HasDependableParts)
            {
                m_interactable.enabled = false;
                m_part.StartDisassemble();
            }
        }

        private void OnDestroy()
        {
            m_interactable.hoverEntered.RemoveListener(OnHoverEntered);
            m_interactable.hoverExited.RemoveListener(OnHoverExited);
        }
    }
}

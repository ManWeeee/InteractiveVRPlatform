using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class CarPartInteractable : MonoBehaviour, IXRSimpleInteractable
    {
        [SerializeField] private HoverMaterials m_materials;
        [SerializeField] private List<CarPartInteractable> m_parentParts;
        [SerializeField] private List<CarPartInteractable> m_dependableParts;

        [SerializeField] private CarPart m_part;
        private Renderer m_renderer;
        private Material m_originalMaterial;
        private XRSimpleInteractable m_interactable;
        //TODO: remove an object from dependable script when it is disabled, and again, when it is enabled
        public UnityAction<HoverEnterEventArgs> HoverEntered;
        public UnityAction<HoverExitEventArgs> HoverExited;
        public Action<CarPartInteractable> DetailDisabled;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_part = GetComponent<CarPart>();

            m_interactable = GetComponent<XRSimpleInteractable>();
            m_interactable.hoverEntered.AddListener(OnHoverEntered);
            m_interactable.hoverExited.AddListener(OnHoverExited);
            m_interactable.selectExited.AddListener(OnSelectExited);

            m_renderer = GetComponentInChildren<Renderer>();
            if (m_renderer != null)
            {
                m_originalMaterial = m_renderer.material;
            }
            if (m_dependableParts.Count == 0)
            {
                return;
            }
            foreach (var part in m_dependableParts)
            {
                part.SetParent(this);
            }
        }

        private void OnDisable()
        {
            m_interactable.hoverEntered.RemoveListener(OnHoverEntered);
            m_interactable.hoverExited.RemoveListener(OnHoverExited);
        }

        public void SetParent(CarPartInteractable parent)
        {
            m_parentParts.Add(parent);
            DetailDisabled += parent.ReleaseChildren;
            parent.HoverEntered += OnHoverEntered;
            parent.HoverExited += OnHoverExited;
        }

        public void SetChildren(CarPartInteractable partInteractable)
        {
            m_dependableParts.Add(partInteractable);
        }

        public void ReleaseChildren(CarPartInteractable partInteractable)
        {
            m_dependableParts.Remove(partInteractable);
        }
        
        public void OnHoverEntered(HoverEnterEventArgs args)
        {
            if (!m_renderer)
            {
                return;
            }

            if(m_dependableParts.Count == 0)
            {
                m_renderer.material = m_materials.ReadonlyRightMaterial;
            }
            else
            {
                m_renderer.material = m_materials.ReadonlyWrongMaterial;
                HoverEntered?.Invoke(args);
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
            if (m_dependableParts.Count == 0)
            {
                m_interactable.enabled = false;
                DetailDisabled?.Invoke(this);
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

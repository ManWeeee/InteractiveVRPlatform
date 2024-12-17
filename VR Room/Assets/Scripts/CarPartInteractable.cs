using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class CarPartInteractable : XRSimpleInteractable
    {
        [SerializeField] private CarPart m_part;

        [SerializeField] private List<Renderer> m_renderers = new List<Renderer>();
        private List<Material> m_defaultMaterials = new List<Material>();

        public Action<HoverEnterEventArgs> HoverEntered;
        public Action<HoverExitEventArgs> HoverExited;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            m_part = GetComponent<CarPart>();
            m_renderers = GetComponentsInChildren<Renderer>().ToList();
            GetMaterialsFromRenderers();
        }

        protected void Start()
        {
            SubscribeToParentEvents();
        }

        private void GetMaterialsFromRenderers()
        {
            foreach (var renderer in m_renderers)
            {
                m_defaultMaterials.Add(renderer.material);
            }
        }

        private void SubscribeToParentEvents()
        {
            foreach (var part in m_part.ReadOnlyParentPartsList)
            {
                part.GetComponent<CarPartInteractable>().HoverEntered += OnHoverEntering;
                part.GetComponent<CarPartInteractable>().HoverExited += OnHoverExiting;
            }
        }

        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            base.OnHoverEntering(args);
            if (args.interactorObject.transform.gameObject.TryGetComponent<InteractorInteractionInfo>(
                    out InteractorInteractionInfo wrapper))
            {
                OnHoverEnter(wrapper.GetHoverMaterials);
            }
            HoverEntered?.Invoke(args);
        }

        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            base.OnHoverExiting(args);
            OnHoverExit();
            HoverExited?.Invoke(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (!m_part.HasDependableParts)
            {
                //this.enabled = false;
                m_part.StartDisassemble();
            }
        }

        public void OnHoverEnter(HoverMaterials materials)
        {
            if (!materials)
            {
                return;
            }
            Material targetMaterial = m_part.HasDependableParts ? materials.ReadonlyWrongMaterial : materials.ReadonlyRightMaterial;
            ChangeMaterial(targetMaterial);
        }

        public void OnHoverExit()
        {
            if (m_renderers == null)
            {
                return;
            }
            SetRendererMaterialsToDefault();
        }

        public void ChangeMaterial(Material material)
        {
            SetRendererMaterialsTo(material);
        }

        private void SetRendererMaterialsToDefault()
        {
            int defaultMaterialsIndex = 0;
            foreach (var renderer in m_renderers)
            {
                renderer.material = m_defaultMaterials[defaultMaterialsIndex];
                ++defaultMaterialsIndex;
            }
        }

        private void SetRendererMaterialsTo(Material material)
        {
            foreach (var renderer in m_renderers)
            {
                renderer.material = material;
            }
        }

    }
}

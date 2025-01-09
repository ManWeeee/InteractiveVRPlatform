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

        public bool CanDisassemble { get; set; }
        public bool CanAssemble { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            m_part = GetComponent<CarPart>();
            m_renderers = GetComponentsInChildren<Renderer>().ToList();
            m_defaultMaterials = GetMaterialsFromRenderers(m_renderers);
        }

        protected void Start()
        {
            SubscribeToParentEvents();
        }

        private List<Material> GetMaterialsFromRenderers(List<Renderer> renderers)
        {
            foreach (var renderer in renderers)
            {
                m_defaultMaterials.Add(renderer.material);
            }
            return m_defaultMaterials;
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
            if (args.interactorObject.transform.gameObject.TryGetComponent<InteractionInfo>(
                    out InteractionInfo interactionInfo) && CanDisassemble)
            {
                OnHoverEnter(interactionInfo.GetHoverMaterials);
                HoverEntered?.Invoke(args);
            }
        }

        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            base.OnHoverExiting(args);
            //TODO: Even if on hover entering nothing changed this part works, doing unecessary opearations
            if (!CanDisassemble)
            {
                Debug.Log("Hover exiting BLOCKED as we are not in Disassembly mode");
                return;
            }
            Debug.Log("Hover exiting!! ");
            OnHoverExit();
            HoverExited?.Invoke(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (CanDisassemble)
            {
                m_part.StartDisassemble();
            }
            else if (CanAssemble)
            {
                Debug.Log("Start Assembly from Interactable");
                m_part.StartAssemble();
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
            ResetRendererMaterialsToDefault();
        }

        public void ChangeMaterial(Material material)
        {
            SetRendererMaterialsTo(material);
        }

        public void ResetRendererMaterialsToDefault()
        {
            int defaultMaterialsIndex = 0;
            foreach (var renderer in m_renderers)
            {
                renderer.material = m_defaultMaterials[defaultMaterialsIndex];
                ++defaultMaterialsIndex;
            }
        }

        public void SetRendererMaterialsTo(Material material)
        {
            foreach (var renderer in m_renderers)
            {
                renderer.material = material;
            }
        }

        public void SetRendererMaterialToSelf(Material material)
        {
            m_renderers[0].material = material;
        }

        public void SetInteraction(bool canDisassemble = false, bool canAssemble = false)
        {
            CanDisassemble = canDisassemble;
            CanAssemble = canAssemble;
        }
    }
}

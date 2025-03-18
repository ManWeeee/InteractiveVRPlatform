
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

        public Action<HoverMaterials> HoverEntered;
        public Action HoverExited;

        public bool CanBeDisassembled { get; set; }
        public bool CanBeAssembled { get; set; }

        public CarPartType CarPartType => m_part.PartInfo.GetCarPartType;

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
                part.GetComponent<CarPartInteractable>().HoverEntered += OnHoverEnter;
                part.GetComponent<CarPartInteractable>().HoverExited += OnHoverExit;
            }
        }
        // TODO: change hover enter so that when we use wrong tool it highlighted not red but some other color
        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            var interactor = args.interactorObject as ToolInteractor;
            var info = interactor.GetComponentInParent<InteractionInfo>();
            if (info != null && interactor.CarPartTypeToInteract.Contains(m_part.PartInfo.GetCarPartType) && CanBeDisassembled)
            {
                OnHoverEnter(info.GetHoverMaterials);
                HoverEntered?.Invoke(info.GetHoverMaterials);
            }
            //base.OnHoverEntering(args);
        }

        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            if (!CanBeDisassembled)
            {
                Debug.Log("Hover exiting BLOCKED as we are not in Disassembly mode");
                return;
            }
            Debug.Log("Hover exiting!! ");
            OnHoverExit();
            HoverExited?.Invoke();
            //base.OnHoverExiting(args);
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

        public void OnActivate(ActivateEventArgs args)
        {
            if (CanBeDisassembled && !m_part.HasDependableParts)
            {
                OnHoverExit();
                SetInteraction();
                m_part.StartDisassemble();
            }
            else if (CanBeAssembled && m_part.CanBeAssembled)
            {
                OnHoverExit();
                SetInteraction();
                m_part.StartAssemble();
            }
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

        public void SetInteraction(bool canDisassemble = false, bool canAssemble = false)
        {
            CanBeDisassembled = canDisassemble;
            CanBeAssembled = canAssemble;
        }
    }
}

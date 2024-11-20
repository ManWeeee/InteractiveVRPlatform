using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class CarPartInteractable : MonoBehaviour, IXRSimpleInteractable
    {
        [SerializeField]
        private HoverMaterials m_materials;

        private CarPart m_part;
        private Renderer m_renderer;
        private Material m_originalMaterial;
        private XRSimpleInteractable m_interactable;
        private List<CarPartInteractable> m_dependableParts = new List<CarPartInteractable>();

        public Action HoverEntered;
        public Action HoverExited;

        private void Start()
        {
            Debug.Log(gameObject.name + " Start Method");
            m_part = GetComponent<CarPart>();
            m_interactable = GetComponent<XRSimpleInteractable>();
            m_interactable.hoverEntered.AddListener(OnHoverEntered);
            m_interactable.hoverExited.AddListener(OnHoverExited);
            if (TryGetComponent<Renderer>(out m_renderer))
            {
                m_originalMaterial = m_renderer.material;
            }

            m_dependableParts = (GetComponentsInChildren<CarPartInteractable>()).ToList();
            Debug.Log(m_dependableParts.Count);
            //foreach (var item in m_dependableParts)
            //{
            //    AddListener(item);
            //}

        }

        private void OnDestroy()
        {
            m_interactable.hoverEntered.RemoveListener(OnHoverEntered);
            m_interactable.hoverExited.RemoveListener(OnHoverExited);
        }

        /*private void AddListener(IXRSimpleInteractable part)
        {
            HoverEntered += part.OnHoverEntered;
            HoverExited += part.OnHoverExited;
        }
        */

        public void OnHoverEntered(HoverEnterEventArgs args)
        {
            if (!m_renderer)
                return;
            if(m_dependableParts.Count == 1)
            {
                m_renderer.material = m_materials.ReadonlyRightMaterial;
            }
            else
            {
                m_renderer.material = m_materials.ReadonlyWrongMaterial;
                //HoverEntered?.Invoke();
            }
        }

        public void OnHoverExited(HoverExitEventArgs args)
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.material = m_originalMaterial;
            //HoverExited?.Invoke(args);
        }

        public void OnSelectEntered(SelectEnterEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnSelectExited(SelectExitEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}

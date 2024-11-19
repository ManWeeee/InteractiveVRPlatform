using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Animator), typeof(XRSimpleInteractable))]
    public abstract class CarPart : MonoBehaviour, IAssemblyPart, IXRSimpleInteractable
    {
        [SerializeField] private CarPartInfo m_partInfo;
        private Renderer m_renderer;
        private Material m_originalMaterial;
        //private Animator m_animator;
        private XRSimpleInteractable m_interactable;
        private List<IXRSimpleInteractable> m_dependableParts = new List<IXRSimpleInteractable>();
        public Action HoverEntered;
        public Action HoverExited;

        public virtual void Start()
        {
            Debug.Log(gameObject.name + " Start method");
            m_interactable = GetComponent<XRSimpleInteractable>();
            m_interactable.hoverEntered.AddListener(OnHoverEntered);
            m_interactable.hoverExited.AddListener(OnHoverExited);
            // TODO: Find out why it takes itself and add to the list => change line 55 to => m_dependableParts.Count == 0
            m_dependableParts = (GetComponentsInChildren<IXRSimpleInteractable>()).ToList();
            m_renderer = GetComponent<Renderer>();
            m_originalMaterial = m_renderer.material;
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

        //private void AddListener(IXRSimpleInteractable part)
        //{
        //    HoverEntered += part.OnHoverEntered;
        //    HoverExited += part.OnHoverExited;
        //}

        public virtual void OnHoverEntered(HoverEnterEventArgs args)
        {
            
            if (m_dependableParts.Count == 1)
            {
                m_renderer.material = m_partInfo.ReadonlyRightMaterial;
            }
            else
            {
                m_renderer.material = m_partInfo.ReadonlyWrongMaterial;
                //HoverEntered?.Invoke(args);
            }
        }

        public virtual void OnHoverExited(HoverExitEventArgs args)
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.material = m_originalMaterial;
            //HoverExited?.Invoke(args);
        }

        public virtual void OnSelectEntered(SelectEnterEventArgs args)
        {
            throw new NotImplementedException();
        }

        public virtual void OnSelectExited(SelectExitEventArgs args)
        {
            throw new NotImplementedException();
        }

        public abstract void StartAssemble();

        public abstract void StartDisassemble();
    }
}
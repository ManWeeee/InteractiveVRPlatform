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
    public abstract class CarPart : MonoBehaviour, IAssemblyPart
    {
        public abstract void StartAssemble();

        public abstract void StartDisassemble();
    }
}
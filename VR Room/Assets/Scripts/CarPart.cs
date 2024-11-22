using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts
{
    public abstract class CarPart : MonoBehaviour, IAssemblyPart
    {
        [SerializeField] protected AnimationHandler m_animationHandler;

        protected virtual void Start()
        {
            m_animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        protected const string DISASSEMBLE_ANIMATION_NAME = "Disassemble";
        protected const string ASSEMBLE_ANIMATION_NAME = "Assemble";

        public abstract Task StartAssemble();

        public abstract Task StartDisassemble();
    }
}
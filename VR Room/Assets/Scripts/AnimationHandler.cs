using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField]
        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        public async Task PlayAnimationAndWait(string animationName)
        {
            //m_animator.Play(animationName);
            m_animator.SetTrigger(animationName);

            await WaitForAnimation(m_animator, animationName);

            Debug.Log($"Animation {animationName} has finished.");
        }

        private async Task WaitForAnimation(Animator animator, string animationName)
        {
            await Task.Delay((int)(animator.GetCurrentAnimatorStateInfo(0).length) * 1000);
        }

    }
}
using System.Linq;
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
            m_animator.SetTrigger(animationName);

            await WaitForAnimation(m_animator, animationName);
        }

        private async Task WaitForAnimation(Animator animator, string animationName)
        {
            var animationState = animator.GetCurrentAnimatorStateInfo(0);
            if (!animationState.IsName(animationName))
            {
                while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
                {
                    await Task.Yield();
                }
            }

            float animationLength = animationState.length;

            await Task.Delay((int)(animationLength * 1000));
        }

    }
}
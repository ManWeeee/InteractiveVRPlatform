using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
    [SerializeField]
    private Animator m_animator;
    public Animator Animator => m_animator;

    private void Start() {
        m_animator = GetComponent<Animator>();
    }

    public async Task PlayAnimationAndWait(string animationName) {
        m_animator.SetTrigger(animationName);

        await WaitForAnimation(m_animator, animationName);
    }

    private async Task WaitForAnimation(Animator animator, string animationName) {
        var animationState = animator.GetCurrentAnimatorStateInfo(0);

        while(!animationState.IsName(animationName)) {
            await Task.Yield();
            animationState = animator.GetCurrentAnimatorStateInfo(0);
        }

        float animationLength = animationState.length;

        await Task.Delay((int)(animationLength * 1000));
    }

/*    public void PlayFromStart() {
        m_animator.speed = 0f; // пауза
        m_animator.Play(stateName, 0, 0f);
    }

    public void SetProgress(float t) {
        animator.Play(stateName, 0, Mathf.Clamp01(t));
    }*/

}

using UnityEngine;

namespace Entities.Player.Animation
{
    public class AnimationTransitionBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            animator.ResetTrigger("comboMove");
    }
}

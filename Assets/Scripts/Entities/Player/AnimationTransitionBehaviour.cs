using UnityEngine;

namespace Entities.Player
{
    public class AnimationTransitionBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            animator.ResetTrigger("comboMove");
    }
}

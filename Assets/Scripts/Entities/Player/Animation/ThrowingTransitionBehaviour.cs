using UnityEngine;

namespace Entities.Player.Animation
{
    public class ThrowingTransitionBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            animator.ResetTrigger("throw");
    }
}

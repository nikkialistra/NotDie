using UnityEngine;

namespace Entities.Player
{
    public class TransitionBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("comboMove");
        }
    }
}

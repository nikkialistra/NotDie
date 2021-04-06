using UnityEngine;

namespace Entities.Player.Animation
{
    public class AnimationTransitionBehaviour : StateMachineBehaviour
    {
        private static readonly int _comboMove = Animator.StringToHash("comboMove");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            animator.ResetTrigger(_comboMove);
    }
}

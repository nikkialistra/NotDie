using UnityEngine;

namespace Entities.Player.Animation
{
    public class ThrowingTransitionBehaviour : StateMachineBehaviour
    {
        private static readonly int _throw = Animator.StringToHash("throw");
        private static readonly int _cancelThrow = Animator.StringToHash("cancelThrow");
        private static readonly int _throwing = Animator.StringToHash("throwing");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(_throw);
            animator.ResetTrigger(_cancelThrow);
            animator.SetBool(_throwing, false);
        }
    }
}

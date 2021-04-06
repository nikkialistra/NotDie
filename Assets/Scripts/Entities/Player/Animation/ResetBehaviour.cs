using Entities.Player.Combat;
using UnityEngine;

namespace Entities.Player.Animation
{
    public class ResetBehaviour : StateMachineBehaviour
    {
        private static readonly int _cancelThrow = Animator.StringToHash("cancelThrow");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var _weaponAttack = animator.GetComponent<WeaponAttack>();

            _weaponAttack.ResetCombo();
            
            animator.ResetTrigger(_cancelThrow);
        }
    }
}

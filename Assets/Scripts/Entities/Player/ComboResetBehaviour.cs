using UnityEngine;

namespace Entities.Player
{
    public class ComboResetBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var _weaponAttack = animator.GetComponent<WeaponAttack>();

            _weaponAttack.ResetCombo();
        }
    }
}

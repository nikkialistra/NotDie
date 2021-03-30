using Entities.Player.Combat;
using UnityEngine;

namespace Entities.Player.Animation
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

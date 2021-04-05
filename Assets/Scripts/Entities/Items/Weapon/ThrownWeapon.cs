using Entities.Player.Animation;
using UnityEngine;
using Zenject;

namespace Entities.Items.Weapon
{
    public class ThrownWeapon : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animation _animation;
        private PlayerAnimator _playerAnimator;

        [Inject]
        public void Construct(GameObject thrownWeapon)
        {
            _spriteRenderer = thrownWeapon.GetComponent<SpriteRenderer>();
            _animation = thrownWeapon.GetComponent<Animation>();
        }

        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerAnimator.WasFlipped += Flip;
        }

        public void StartThrowing()
        {
            Flip(_playerAnimator.IsFlipped);
            _spriteRenderer.enabled = true;
            _animation.Play();
        }

        public void StopThrowing() => _spriteRenderer.enabled = false;

        private void Flip(bool value) => _spriteRenderer.flipY = value;
    }
}
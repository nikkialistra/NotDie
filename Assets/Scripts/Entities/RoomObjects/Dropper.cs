using UnityEngine;

namespace Entities.RoomObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Dropper : RoomObject
    {
        private SpriteRenderer _renderer;
        
        [SerializeField] private Sprite _activeState;
        [SerializeField] private Sprite _notActiveState;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _renderer.sprite = _activeState;
        }

        public override void Use()
        {
            _renderer.sprite = _notActiveState;
        }
    }
}
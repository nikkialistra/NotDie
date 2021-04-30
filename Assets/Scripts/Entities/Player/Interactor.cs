using System;
using System.Collections;
using Entities.Player.Combat;
using Entities.RoomObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(PlayerInput))]
    public class Interactor : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float DistanceForTaking;
        }
        
        private Settings _settings;
        
        private PlayerAttack _playerAttack;
        
        private PlayerInput _input;
        private InputAction _interactAction;
        private InputAction _specialInteractAction;

        [Inject]
        public void Construct(Settings settings) => _settings = settings;

        private void Awake()
        {
            _playerAttack = GetComponent<PlayerAttack>();
            _input = GetComponent<PlayerInput>();
            _interactAction = _input.actions.FindAction("InteractAttackThrow");
            _specialInteractAction = _input.actions.FindAction("SpecialInteractAttack");
        }
        
        private void OnEnable()
        {
            _interactAction.started += OnInteract;
            _specialInteractAction.started += OnSpecialInteract;
        }

        private void OnDisable()
        {
            _interactAction.started -= OnInteract;
            _specialInteractAction.started -= OnSpecialInteract;
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (TryFindRoomObject(out var roomObject))
            {
                roomObject.Use();
            }
            else
            {
                StartCoroutine(TransferControlToPlayerAttack());
            } 
        }

        private IEnumerator TransferControlToPlayerAttack()
        {
            while (_interactAction.ReadValue<float>() > 0)
            {
                _playerAttack.ThrowOrAttack();
                yield return null;
            }
        }

        private void OnSpecialInteract(InputAction.CallbackContext context)
        {
            if (TryFindRoomObject(out var roomObject))
            {
                roomObject.Use();
            }
        }

        private bool TryFindRoomObject(out RoomObject foundRoomObject)
        {
            var roomObjects = FindObjectsOfType<RoomObject>();
            foreach (var roomObject in roomObjects)
            {
                if (Vector3.Distance(transform.position, roomObject.transform.position) >
                    _settings.DistanceForTaking)
                {
                    continue;
                }

                foundRoomObject = roomObject;
                return true;
            }

            foundRoomObject = null;
            return false;
        }
    }
}
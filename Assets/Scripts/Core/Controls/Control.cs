// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Core/Controls/Control.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Services.Controls
{
    public class @Control : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Control()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Control"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""55d84c6e-fbb6-46cc-8435-ffdf409e9e99"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""55ed0da7-4fae-4951-9ebb-9ea792e15f33"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""93055143-3c09-49e6-81a7-0055fe3f78cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapWeapons"",
                    ""type"": ""Button"",
                    ""id"": ""6af46090-f2eb-4ec4-8d2e-76cef3e531ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TakeDropWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""52f095f1-70d7-4633-9960-e85e4d14ac10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowAttackDirection"",
                    ""type"": ""Button"",
                    ""id"": ""f9f8335e-f479-4557-8842-942ab48187b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""c8a03c0a-e2a7-4c2e-802a-e9b86614418f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4cf322f7-241d-4230-9e2a-c0947e77c53f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""82c319b8-830f-4abf-8ca1-6dbfb413c739"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b7a700e0-8ac2-438f-ab13-a789fdcf8242"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0ef2802-8b5e-4683-a88b-713bdce8a5cb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""79c3f115-d0af-451d-928e-05c89ced1934"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""72469454-2390-4ba5-a2af-df10a9e24b20"",
                    ""path"": ""<HID::GreenAsia Inc.      USB  Joystick  >/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""90978e90-5ad9-42b5-acfc-da697eec66e5"",
                    ""path"": ""<HID::GreenAsia Inc.      USB  Joystick  >/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""16172348-e15d-4454-8c31-fa5a02fb86b5"",
                    ""path"": ""<HID::GreenAsia Inc.      USB  Joystick  >/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""404a5aa2-7244-4cbe-bb31-dff34160d34c"",
                    ""path"": ""<HID::GreenAsia Inc.      USB  Joystick  >/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""053bd61b-4ceb-4a16-8a4e-0da608c72522"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9f8d6ba-917e-4e7c-82bc-2d60fda5961c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32ec1a3e-fb9f-4d69-8a52-6dd6618fc011"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeDropWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e772752-a33f-496e-b204-89c0f1b673d1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeDropWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b24d0bcf-2650-40b4-a720-beb7806fd628"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowAttackDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39e53b45-dcfd-4b3c-b019-68d6801ddc0a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowAttackDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af87625a-3ae3-4c74-934c-4f656820b4b7"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapWeapons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""870d85c6-9305-4509-bab4-e0438f23673a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapWeapons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
            m_Player_SwapWeapons = m_Player.FindAction("SwapWeapons", throwIfNotFound: true);
            m_Player_TakeDropWeapon = m_Player.FindAction("TakeDropWeapon", throwIfNotFound: true);
            m_Player_ShowAttackDirection = m_Player.FindAction("ShowAttackDirection", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_Attack;
        private readonly InputAction m_Player_SwapWeapons;
        private readonly InputAction m_Player_TakeDropWeapon;
        private readonly InputAction m_Player_ShowAttackDirection;
        public struct PlayerActions
        {
            private @Control m_Wrapper;
            public PlayerActions(@Control wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Attack => m_Wrapper.m_Player_Attack;
            public InputAction @SwapWeapons => m_Wrapper.m_Player_SwapWeapons;
            public InputAction @TakeDropWeapon => m_Wrapper.m_Player_TakeDropWeapon;
            public InputAction @ShowAttackDirection => m_Wrapper.m_Player_ShowAttackDirection;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                    @SwapWeapons.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @SwapWeapons.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @SwapWeapons.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @TakeDropWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropWeapon;
                    @TakeDropWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropWeapon;
                    @TakeDropWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropWeapon;
                    @ShowAttackDirection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowAttackDirection;
                    @ShowAttackDirection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowAttackDirection;
                    @ShowAttackDirection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowAttackDirection;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                    @SwapWeapons.started += instance.OnSwapWeapons;
                    @SwapWeapons.performed += instance.OnSwapWeapons;
                    @SwapWeapons.canceled += instance.OnSwapWeapons;
                    @TakeDropWeapon.started += instance.OnTakeDropWeapon;
                    @TakeDropWeapon.performed += instance.OnTakeDropWeapon;
                    @TakeDropWeapon.canceled += instance.OnTakeDropWeapon;
                    @ShowAttackDirection.started += instance.OnShowAttackDirection;
                    @ShowAttackDirection.performed += instance.OnShowAttackDirection;
                    @ShowAttackDirection.canceled += instance.OnShowAttackDirection;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnSwapWeapons(InputAction.CallbackContext context);
            void OnTakeDropWeapon(InputAction.CallbackContext context);
            void OnShowAttackDirection(InputAction.CallbackContext context);
        }
    }
}

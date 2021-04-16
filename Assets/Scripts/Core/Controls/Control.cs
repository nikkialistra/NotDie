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
                    ""name"": ""AttackThrow"",
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
                    ""name"": ""TakeDropThrowingWeapon"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""79c3f115-d0af-451d-928e-05c89ced1934"",
                    ""path"": ""2DVector(mode=2)"",
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
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""90978e90-5ad9-42b5-acfc-da697eec66e5"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""16172348-e15d-4454-8c31-fa5a02fb86b5"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""404a5aa2-7244-4cbe-bb31-dff34160d34c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Keyboard"",
                    ""action"": ""AttackThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9f8d6ba-917e-4e7c-82bc-2d60fda5961c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AttackThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32ec1a3e-fb9f-4d69-8a52-6dd6618fc011"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": ""Hold(duration=0.3)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TakeDropThrowingWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e772752-a33f-496e-b204-89c0f1b673d1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Hold(duration=0.3)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TakeDropThrowingWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b24d0bcf-2650-40b4-a720-beb7806fd628"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwapWeapons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""c1f65a4d-f8e2-4338-9d7e-c7604d8609e5"",
            ""actions"": [
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""3423ed84-71a0-4272-8a61-37ab6575161c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc101554-d007-4bc8-8038-17a95812b792"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_AttackThrow = m_Player.FindAction("AttackThrow", throwIfNotFound: true);
            m_Player_SwapWeapons = m_Player.FindAction("SwapWeapons", throwIfNotFound: true);
            m_Player_TakeDropThrowingWeapon = m_Player.FindAction("TakeDropThrowingWeapon", throwIfNotFound: true);
            m_Player_ShowAttackDirection = m_Player.FindAction("ShowAttackDirection", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Return = m_UI.FindAction("Return", throwIfNotFound: true);
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
        private readonly InputAction m_Player_AttackThrow;
        private readonly InputAction m_Player_SwapWeapons;
        private readonly InputAction m_Player_TakeDropThrowingWeapon;
        private readonly InputAction m_Player_ShowAttackDirection;
        public struct PlayerActions
        {
            private @Control m_Wrapper;
            public PlayerActions(@Control wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @AttackThrow => m_Wrapper.m_Player_AttackThrow;
            public InputAction @SwapWeapons => m_Wrapper.m_Player_SwapWeapons;
            public InputAction @TakeDropThrowingWeapon => m_Wrapper.m_Player_TakeDropThrowingWeapon;
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
                    @AttackThrow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackThrow;
                    @AttackThrow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackThrow;
                    @AttackThrow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttackThrow;
                    @SwapWeapons.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @SwapWeapons.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @SwapWeapons.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @TakeDropThrowingWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropThrowingWeapon;
                    @TakeDropThrowingWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropThrowingWeapon;
                    @TakeDropThrowingWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropThrowingWeapon;
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
                    @AttackThrow.started += instance.OnAttackThrow;
                    @AttackThrow.performed += instance.OnAttackThrow;
                    @AttackThrow.canceled += instance.OnAttackThrow;
                    @SwapWeapons.started += instance.OnSwapWeapons;
                    @SwapWeapons.performed += instance.OnSwapWeapons;
                    @SwapWeapons.canceled += instance.OnSwapWeapons;
                    @TakeDropThrowingWeapon.started += instance.OnTakeDropThrowingWeapon;
                    @TakeDropThrowingWeapon.performed += instance.OnTakeDropThrowingWeapon;
                    @TakeDropThrowingWeapon.canceled += instance.OnTakeDropThrowingWeapon;
                    @ShowAttackDirection.started += instance.OnShowAttackDirection;
                    @ShowAttackDirection.performed += instance.OnShowAttackDirection;
                    @ShowAttackDirection.canceled += instance.OnShowAttackDirection;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Return;
        public struct UIActions
        {
            private @Control m_Wrapper;
            public UIActions(@Control wrapper) { m_Wrapper = wrapper; }
            public InputAction @Return => m_Wrapper.m_UI_Return;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Return.started -= m_Wrapper.m_UIActionsCallbackInterface.OnReturn;
                    @Return.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnReturn;
                    @Return.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnReturn;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Return.started += instance.OnReturn;
                    @Return.performed += instance.OnReturn;
                    @Return.canceled += instance.OnReturn;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnAttackThrow(InputAction.CallbackContext context);
            void OnSwapWeapons(InputAction.CallbackContext context);
            void OnTakeDropThrowingWeapon(InputAction.CallbackContext context);
            void OnShowAttackDirection(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnReturn(InputAction.CallbackContext context);
        }
    }
}

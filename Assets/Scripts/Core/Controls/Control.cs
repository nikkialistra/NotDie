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
                    ""name"": ""InteractAttackThrow"",
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
                    ""name"": ""TakeDropThrowing"",
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
                },
                {
                    ""name"": ""SpecialInteractAttack"",
                    ""type"": ""Button"",
                    ""id"": ""68fea22e-025b-4ee7-ac07-cda07f863f44"",
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
                    ""action"": ""InteractAttackThrow"",
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
                    ""action"": ""InteractAttackThrow"",
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
                    ""action"": ""TakeDropThrowing"",
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
                    ""action"": ""TakeDropThrowing"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""092413db-df6e-4e0d-aafa-7f89ac56fbc0"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialInteractAttack"",
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
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""f768506f-1cdf-4619-988a-53291eef6384"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""4d7ac6de-190f-4f31-9ff4-6dba1a776e63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""81711a55-f38f-424f-b808-db073c93fd48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""f0aa0d63-6c6f-4f3a-8293-ede1e5acd6d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""78a2d64b-c752-43ed-9d8d-6ead34e0f5af"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""d97ad531-91ed-40ed-aa04-0914be7eac82"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02e498c4-8ec4-4190-9ee4-d3fa2d839654"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4440eec9-f85f-432a-93f1-36abc1ce4ce7"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7333f008-6101-49cf-8a0f-2d2e1828299c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fd37ac8-1168-474c-adf0-229dbe3ed005"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
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
            m_Player_InteractAttackThrow = m_Player.FindAction("InteractAttackThrow", throwIfNotFound: true);
            m_Player_SwapWeapons = m_Player.FindAction("SwapWeapons", throwIfNotFound: true);
            m_Player_TakeDropThrowing = m_Player.FindAction("TakeDropThrowing", throwIfNotFound: true);
            m_Player_ShowAttackDirection = m_Player.FindAction("ShowAttackDirection", throwIfNotFound: true);
            m_Player_SpecialInteractAttack = m_Player.FindAction("SpecialInteractAttack", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Return = m_UI.FindAction("Return", throwIfNotFound: true);
            m_UI_Left = m_UI.FindAction("Left", throwIfNotFound: true);
            m_UI_Right = m_UI.FindAction("Right", throwIfNotFound: true);
            m_UI_Select = m_UI.FindAction("Select", throwIfNotFound: true);
            m_UI_Up = m_UI.FindAction("Up", throwIfNotFound: true);
            m_UI_Down = m_UI.FindAction("Down", throwIfNotFound: true);
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
        private readonly InputAction m_Player_InteractAttackThrow;
        private readonly InputAction m_Player_SwapWeapons;
        private readonly InputAction m_Player_TakeDropThrowing;
        private readonly InputAction m_Player_ShowAttackDirection;
        private readonly InputAction m_Player_SpecialInteractAttack;
        public struct PlayerActions
        {
            private @Control m_Wrapper;
            public PlayerActions(@Control wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @InteractAttackThrow => m_Wrapper.m_Player_InteractAttackThrow;
            public InputAction @SwapWeapons => m_Wrapper.m_Player_SwapWeapons;
            public InputAction @TakeDropThrowing => m_Wrapper.m_Player_TakeDropThrowing;
            public InputAction @ShowAttackDirection => m_Wrapper.m_Player_ShowAttackDirection;
            public InputAction @SpecialInteractAttack => m_Wrapper.m_Player_SpecialInteractAttack;
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
                    @InteractAttackThrow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractAttackThrow;
                    @InteractAttackThrow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractAttackThrow;
                    @InteractAttackThrow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractAttackThrow;
                    @SwapWeapons.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @SwapWeapons.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @SwapWeapons.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapWeapons;
                    @TakeDropThrowing.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropThrowing;
                    @TakeDropThrowing.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropThrowing;
                    @TakeDropThrowing.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeDropThrowing;
                    @ShowAttackDirection.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowAttackDirection;
                    @ShowAttackDirection.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowAttackDirection;
                    @ShowAttackDirection.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowAttackDirection;
                    @SpecialInteractAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialInteractAttack;
                    @SpecialInteractAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialInteractAttack;
                    @SpecialInteractAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialInteractAttack;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @InteractAttackThrow.started += instance.OnInteractAttackThrow;
                    @InteractAttackThrow.performed += instance.OnInteractAttackThrow;
                    @InteractAttackThrow.canceled += instance.OnInteractAttackThrow;
                    @SwapWeapons.started += instance.OnSwapWeapons;
                    @SwapWeapons.performed += instance.OnSwapWeapons;
                    @SwapWeapons.canceled += instance.OnSwapWeapons;
                    @TakeDropThrowing.started += instance.OnTakeDropThrowing;
                    @TakeDropThrowing.performed += instance.OnTakeDropThrowing;
                    @TakeDropThrowing.canceled += instance.OnTakeDropThrowing;
                    @ShowAttackDirection.started += instance.OnShowAttackDirection;
                    @ShowAttackDirection.performed += instance.OnShowAttackDirection;
                    @ShowAttackDirection.canceled += instance.OnShowAttackDirection;
                    @SpecialInteractAttack.started += instance.OnSpecialInteractAttack;
                    @SpecialInteractAttack.performed += instance.OnSpecialInteractAttack;
                    @SpecialInteractAttack.canceled += instance.OnSpecialInteractAttack;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Return;
        private readonly InputAction m_UI_Left;
        private readonly InputAction m_UI_Right;
        private readonly InputAction m_UI_Select;
        private readonly InputAction m_UI_Up;
        private readonly InputAction m_UI_Down;
        public struct UIActions
        {
            private @Control m_Wrapper;
            public UIActions(@Control wrapper) { m_Wrapper = wrapper; }
            public InputAction @Return => m_Wrapper.m_UI_Return;
            public InputAction @Left => m_Wrapper.m_UI_Left;
            public InputAction @Right => m_Wrapper.m_UI_Right;
            public InputAction @Select => m_Wrapper.m_UI_Select;
            public InputAction @Up => m_Wrapper.m_UI_Up;
            public InputAction @Down => m_Wrapper.m_UI_Down;
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
                    @Left.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Left.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Left.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Right.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRight;
                    @Right.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRight;
                    @Right.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRight;
                    @Select.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                    @Select.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                    @Select.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                    @Up.started -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Up.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Up.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Down.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Down.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Down.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Return.started += instance.OnReturn;
                    @Return.performed += instance.OnReturn;
                    @Return.canceled += instance.OnReturn;
                    @Left.started += instance.OnLeft;
                    @Left.performed += instance.OnLeft;
                    @Left.canceled += instance.OnLeft;
                    @Right.started += instance.OnRight;
                    @Right.performed += instance.OnRight;
                    @Right.canceled += instance.OnRight;
                    @Select.started += instance.OnSelect;
                    @Select.performed += instance.OnSelect;
                    @Select.canceled += instance.OnSelect;
                    @Up.started += instance.OnUp;
                    @Up.performed += instance.OnUp;
                    @Up.canceled += instance.OnUp;
                    @Down.started += instance.OnDown;
                    @Down.performed += instance.OnDown;
                    @Down.canceled += instance.OnDown;
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
            void OnInteractAttackThrow(InputAction.CallbackContext context);
            void OnSwapWeapons(InputAction.CallbackContext context);
            void OnTakeDropThrowing(InputAction.CallbackContext context);
            void OnShowAttackDirection(InputAction.CallbackContext context);
            void OnSpecialInteractAttack(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnReturn(InputAction.CallbackContext context);
            void OnLeft(InputAction.CallbackContext context);
            void OnRight(InputAction.CallbackContext context);
            void OnSelect(InputAction.CallbackContext context);
            void OnUp(InputAction.CallbackContext context);
            void OnDown(InputAction.CallbackContext context);
        }
    }
}

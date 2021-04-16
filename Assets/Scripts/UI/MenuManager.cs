using System;
using UI.MenuViews;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(PlayerInput))]
    public class MenuManager : MonoBehaviour, IMenuView
    {
        public event Action Return;
        
        private VisualElement _rootVisualElement;

        private VisualElement _screen;
        
        private PlayerInput _input;
        private InputAction _returnAction;
        
        public void ShowSelf() => _screen.style.display = DisplayStyle.Flex;

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _screen = _rootVisualElement.Q<VisualElement>("screen");

            _input = GetComponent<PlayerInput>();
            _returnAction = _input.actions.FindAction("Return");
        }

        private void Start()
        {
            var menu = new MainMenuView(_rootVisualElement, this, this);
            menu.ShowSelf();
        }

        private void OnEnable() => _returnAction.started += OnReturn;
        
        private void OnDisable() => _returnAction.started -= OnReturn;

        private void OnReturn(InputAction.CallbackContext obj) => Return?.Invoke();
    }
}
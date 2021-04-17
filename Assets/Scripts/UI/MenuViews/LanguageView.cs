using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class LanguageView : MenuView
    {
        private Label _languageChoice;
        
        private string[] _languages =
        {
            "English",
            "Russian"
        };

        private int _index;
        
        
        private PlayerInput _input;
        private InputAction _leftAction;
        private InputAction _rightAction;

        public LanguageView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Language");
            _tree = template.CloneTree();
        }

        protected override void SetUpBindings()
        {
            _languageChoice = _tree.Q<Label>("language__choice");
            
            _input = _menuManager.Input;
            _leftAction = _input.actions.FindAction("Left");
            _rightAction = _input.actions.FindAction("Right");

            _leftAction.started += ChangeLanguageLeft;
            _rightAction.started += ChangeLanguageRight;
        }

        private void ChangeLanguageLeft(InputAction.CallbackContext context)
        {
            _index = (_index - 1) % _languages.Length;

            if (_index == -1)
                _index = _languages.Length - 1;
            
            _languageChoice.text = _languages[_index];
        }
        
        private void ChangeLanguageRight(InputAction.CallbackContext context)
        {
            _index = (_index + 1) % _languages.Length;
            _languageChoice.text = _languages[_index];
        }

        protected override void Enable()
        {
        }
    }
}
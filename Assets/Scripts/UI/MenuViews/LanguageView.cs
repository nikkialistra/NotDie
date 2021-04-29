using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class LanguageView : MenuView
    {
        private Label _languageChoice;

        private List<Locale> _languages;

        private int _index;

        private PlayerInput _input;
        private InputAction _leftAction;
        private InputAction _rightAction;
        private InputAction _selectAction;

        public LanguageView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Menus/Settings/Language");
            _tree = template.CloneTree();
        }

        protected override void SetUp()
        {
            _languages = LocalizationSettings.AvailableLocales.Locales;

            _languageChoice = _tree.Q<Label>("language__choice");

            InitializeLanguageIndex();
            SetLanguageText();
            
            _input = _menuManager.Input;
            _leftAction = _input.actions.FindAction("Left");
            _rightAction = _input.actions.FindAction("Right");
            _selectAction = _input.actions.FindAction("Select");
        }

        protected override void Enable()
        {
            _leftAction.started += ChangeLanguageLeft;
            _rightAction.started += ChangeLanguageRight;
            _selectAction.started += SetLanguage;
        }

        protected override void Disable()
        {
            _menuManager.GameSettings.Save();
            
            _leftAction.started -= ChangeLanguageLeft;
            _rightAction.started -= ChangeLanguageRight;
            _selectAction.started -= SetLanguage;
        }

        private void InitializeLanguageIndex()
        {
            if (!_menuManager.GameSettings.Loaded)
            {
                return;
            }
            
            for (var i = 0; i < _languages.Count; i++)
            {
                if (_languages[i].ToString() == _menuManager.GameSettings.Language)
                {
                    _index = i;
                }
            }
        }

        private void SetLanguage(InputAction.CallbackContext context)
        {
            _menuManager.GameSettings.Language = _languages[_index].ToString();
            
            LocalizationSettings.SelectedLocale = _languages[_index];
        }

        private void ChangeLanguageLeft(InputAction.CallbackContext context)
        {
            _index = (_index - 1) % _languages.Count;

            if (_index == -1)
            {
                _index = _languages.Count - 1;
            }

            SetLanguageText();
        }

        private void ChangeLanguageRight(InputAction.CallbackContext context)
        {
            _index = (_index + 1) % _languages.Count;
            
            SetLanguageText();
        }

        private void SetLanguageText()
        {
            var locale = _languages[_index].Formatter.ToString();
            
            _languageChoice.viewDataKey = locale switch
            {
                "en-US" => "_english",
                "ru-RU" => "_russian",
                _ => "_unknown"
            };
            
            _menuManager.Localize(_languageChoice);
        }
    }
}
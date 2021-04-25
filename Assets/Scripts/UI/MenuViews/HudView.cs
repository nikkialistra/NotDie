using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class HudView : MenuView
    {
        private Label _fontStyleChoice;
        private Label _fontSizeChoice;
        private Label _showTimerChoice;
        
        private readonly string[] _fontStyles = {"_pixelated", "_regular"};
        private readonly string[] _fontSizes = {"_small", "_medium", "_big"};
        private readonly string[] _showTimerChoices = {"_true", "_false"};

        private int _fontStyleIndex;
        private int _fontSizeIndex;
        private int _showTimerIndex;
        
        private enum ActiveParameter
        {
            FontStyle,
            FontSize,
            ShowTimer,
            Length
        }

        private ActiveParameter _activeParameter;

        private PlayerInput _input;
        private InputAction _upAction;
        private InputAction _downAction;
        private InputAction _leftAction;
        private InputAction _rightAction;
        private InputAction _selectAction;

        protected override void SetUp()
        {
            _fontStyleChoice = _tree.Q<Label>("font_style__choice");
            _fontSizeChoice = _tree.Q<Label>("font_size__choice");
            _showTimerChoice = _tree.Q<Label>("show_timer__choice");
            
            SetHudValues();

            _input = _menuManager.Input;
            _upAction = _input.actions.FindAction("Up");
            _downAction = _input.actions.FindAction("Down");
            _leftAction = _input.actions.FindAction("Left");
            _rightAction = _input.actions.FindAction("Right");
            _selectAction = _input.actions.FindAction("Select");
        }

        public HudView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Menus/Settings/Hud");
            _tree = template.CloneTree();
        }

        protected override void Enable()
        {
            _activeParameter = ActiveParameter.FontStyle;
            _fontStyleChoice.RemoveFromClassList("unfocused");
            _fontSizeChoice.AddToClassList("unfocused");
            _showTimerChoice.AddToClassList("unfocused");
            
            _upAction.started += OnUp;
            _downAction.started += OnDown;
            _leftAction.started += OnLeftChange;
            _rightAction.started += OnRightChange;
            _selectAction.started += OnSelect;
        }

        protected override void Disable()
        {
            _upAction.started -= OnUp;
            _downAction.started -= OnDown;
            _leftAction.started -= OnLeftChange;
            _rightAction.started -= OnRightChange;
            _selectAction.started -= OnSelect;
        }

        private void OnUp(InputAction.CallbackContext context)
        {
            _activeParameter = (ActiveParameter) (((int) _activeParameter - 1) % (int) ActiveParameter.Length);

            if ((int) _activeParameter == -1)
                _activeParameter = ActiveParameter.Length - 1;
            
            UpdateFocusedLabel();
        }

        private void OnDown(InputAction.CallbackContext context)
        {
            _activeParameter = (ActiveParameter) (((int) _activeParameter + 1) % (int) ActiveParameter.Length);
            
            UpdateFocusedLabel();
        }

        private void UpdateFocusedLabel()
        {
            switch (_activeParameter)
            {
                case ActiveParameter.FontStyle:
                    _fontStyleChoice.RemoveFromClassList("unfocused");
                    _fontSizeChoice.AddToClassList("unfocused");
                    _showTimerChoice.AddToClassList("unfocused");
                    break;
                case ActiveParameter.FontSize:
                    _fontStyleChoice.AddToClassList("unfocused");
                    _fontSizeChoice.RemoveFromClassList("unfocused");
                    _showTimerChoice.AddToClassList("unfocused");
                    break;
                case ActiveParameter.ShowTimer:
                    _fontStyleChoice.AddToClassList("unfocused");
                    _fontSizeChoice.AddToClassList("unfocused");
                    _showTimerChoice.RemoveFromClassList("unfocused");
                    break;
            }
        }

        private void SetHudValues()
        {
            SetFontStyleText();
            SetFontSizeText();
            SetShowTimerText();
        }

        private void OnSelect(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.FontStyle)
                SelectFontStyle();
            if (_activeParameter == ActiveParameter.FontSize)
                SelectFontSize();
            if (_activeParameter == ActiveParameter.ShowTimer)
                SelectShowTimer();
        }

        private void SelectFontStyle()
        {
            
        }

        private void SelectFontSize()
        {
            
        }

        private void SelectShowTimer()
        {
            
        }

        private void OnLeftChange(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.FontStyle)
                ChangeElementLeft(_fontStyles, ref _fontStyleIndex, SetFontStyleText);
            if (_activeParameter == ActiveParameter.FontSize)
                ChangeElementLeft(_fontSizes, ref _fontSizeIndex, SetFontSizeText);
            if (_activeParameter == ActiveParameter.ShowTimer)
                ChangeElementLeft(_showTimerChoices, ref _showTimerIndex, SetShowTimerText);
        }
        
        private void OnRightChange(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.FontStyle)
                ChangeElementRight(_fontStyles, ref _fontStyleIndex, SetFontStyleText);
            if (_activeParameter == ActiveParameter.FontSize)
                ChangeElementRight(_fontSizes, ref _fontSizeIndex, SetFontSizeText);
            if (_activeParameter == ActiveParameter.ShowTimer)
                ChangeElementRight(_showTimerChoices, ref _showTimerIndex, SetShowTimerText);
        }

        private void ChangeElementLeft(string[] elements, ref int elementIndex, Action setText)
        {
            elementIndex = (elementIndex - 1) % elements.Length;

            if (elementIndex == -1)
                elementIndex = elements.Length - 1;

            setText();
        }

        private void ChangeElementRight(string[] elements, ref int elementIndex, Action setText)
        {
            elementIndex = (elementIndex + 1) % elements.Length;

            setText();
        }

        private void SetFontStyleText()
        {
            _fontStyleChoice.viewDataKey = _fontStyles[_fontStyleIndex];
            _menuManager.Localize(_fontStyleChoice);
        }

        private void SetFontSizeText()
        {
            _fontSizeChoice.viewDataKey = _fontSizes[_fontSizeIndex];
            _menuManager.Localize(_fontSizeChoice);
        }

        private void SetShowTimerText()
        {
            _showTimerChoice.viewDataKey = _showTimerChoices[_showTimerIndex];
            _menuManager.Localize(_showTimerChoice);
        }
    }
}
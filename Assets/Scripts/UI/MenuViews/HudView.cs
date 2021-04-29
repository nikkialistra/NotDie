using System;
using Core.Saving;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using FontStyle = Core.Saving.FontStyle;

namespace UI.MenuViews
{
    public class HudView : MenuView
    {
        private Label _fontStyleChoice;
        private Label _fontSizeChoice;
        private Label _showTimerChoice;

        private FontStyle _fontStyle;
        private FontSize _fontSize;
        private ShowTimer _showTimer;

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

        protected override void SetUp()
        {
            _fontStyleChoice = _tree.Q<Label>("font_style__choice");
            _fontSizeChoice = _tree.Q<Label>("font_size__choice");
            _showTimerChoice = _tree.Q<Label>("show_timer__choice");
            
            InitializeHudValues();
            SetHudValues();

            _input = _menuManager.Input;
            _upAction = _input.actions.FindAction("Up");
            _downAction = _input.actions.FindAction("Down");
            _leftAction = _input.actions.FindAction("Left");
            _rightAction = _input.actions.FindAction("Right");
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
        }

        protected override void Disable()
        {
            _upAction.started -= OnUp;
            _downAction.started -= OnDown;
            _leftAction.started -= OnLeftChange;
            _rightAction.started -= OnRightChange;
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

        private void InitializeHudValues()
        {
            if (!_menuManager.GameSettings.Loaded)
                return;
            
            _fontStyle = _menuManager.GameSettings.FontStyle;
            _fontSize = _menuManager.GameSettings.FontSize;
            _showTimer = _menuManager.GameSettings.ShowTimer;
        }

        private void SetHudValues()
        {
            SetFontStyleText();
            SetFontSizeText();
            SetShowTimerText();
        }

        private void OnLeftChange(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.FontStyle)
                ChangeFontStyle();
            if (_activeParameter == ActiveParameter.FontSize)
                ChangeFontSizeLeft();
            if (_activeParameter == ActiveParameter.ShowTimer)
                ChangeShowTimer();
        }

        private void OnRightChange(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.FontStyle)
                ChangeFontStyle();
            if (_activeParameter == ActiveParameter.FontSize)
                ChangeFontSizeRight();
            if (_activeParameter == ActiveParameter.ShowTimer)
                ChangeShowTimer();
        }

        private void ChangeFontStyle()
        {
            _fontStyle = _fontStyle switch
            {
                FontStyle.Pixelated => FontStyle.Regular,
                FontStyle.Regular => FontStyle.Pixelated,
                _ => throw new ArgumentOutOfRangeException(nameof(_fontStyle))
            };
            
            _menuManager.GameSettings.FontStyle = _fontStyle;

            SetFontStyleText();
            _menuManager.GameSettings.Save();
        }

        private void ChangeFontSizeLeft()
        {
            _fontSize = _fontSize switch
            {
                FontSize.Small => FontSize.Big,
                FontSize.Medium => FontSize.Small,
                FontSize.Big => FontSize.Medium,
                _ => throw new ArgumentOutOfRangeException(nameof(_fontSize))
            };
            
            _menuManager.GameSettings.FontSize = _fontSize;

            SetFontSizeText();
            _menuManager.GameSettings.Save();
        }

        private void ChangeFontSizeRight()
        {
            _fontSize = _fontSize switch
            {
                FontSize.Small => FontSize.Medium,
                FontSize.Medium => FontSize.Big,
                FontSize.Big => FontSize.Small,
                _ => throw new ArgumentOutOfRangeException(nameof(_fontSize))
            };
            
            _menuManager.GameSettings.FontSize = _fontSize;

            SetFontSizeText();
            _menuManager.GameSettings.Save();
        }

        private void ChangeShowTimer()
        {
            _showTimer = _showTimer switch
            {
                ShowTimer.False => ShowTimer.True,
                ShowTimer.True => ShowTimer.False,
                _ => throw new ArgumentOutOfRangeException(nameof(_showTimer))
            };
            
            _menuManager.GameSettings.ShowTimer = _showTimer;

            SetShowTimerText();
            _menuManager.GameSettings.Save();
        }

        private void SetFontStyleText()
        {
            _fontStyleChoice.viewDataKey = _fontStyle switch
            {
                FontStyle.Pixelated => "_pixelated",
                FontStyle.Regular => "_regular",
                _ => throw new ArgumentOutOfRangeException(nameof(_fontStyle))
            };
            _menuManager.Localize(_fontStyleChoice);
        }

        private void SetFontSizeText()
        {
            _fontSizeChoice.viewDataKey = _fontSize switch
            {
                FontSize.Small => "_small",
                FontSize.Medium => "_medium",
                FontSize.Big => "_big",
                _ => throw new ArgumentOutOfRangeException(nameof(_fontSize))
            };
            _menuManager.Localize(_fontSizeChoice);
        }

        private void SetShowTimerText()
        {
            _showTimerChoice.viewDataKey = _showTimer switch
            {
                ShowTimer.True => "_true",
                ShowTimer.False => "_false",
                _ => throw new ArgumentOutOfRangeException(nameof(_showTimer))
            };
            _menuManager.Localize(_showTimerChoice);
        }
    }
}
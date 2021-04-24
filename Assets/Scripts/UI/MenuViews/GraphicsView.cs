using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class GraphicsView : MenuView
    {
        private Label _resolutionChoice;
        private Label _displayModeChoice;

        private readonly Resolution[] _resolutions = Screen.resolutions;
        private readonly string[] _displayModes = {"Full Screen", "Windowed"};
        private readonly string[] _displayModeKeys = {"_fullscreen", "_windowed"};

        private int _resolutionIndex;
        private int _displayModeIndex;
        
        private enum ActiveParameter
        {
            Resolution,
            DisplayMode
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
            _resolutionChoice = _tree.Q<Label>("resolution__choice");
            
            _displayModeChoice = _tree.Q<Label>("display_mode__choice");
            
            SetGraphicsValues();

            _input = _menuManager.Input;
            _upAction = _input.actions.FindAction("Up");
            _downAction = _input.actions.FindAction("Down");
            _leftAction = _input.actions.FindAction("Left");
            _rightAction = _input.actions.FindAction("Right");
            _selectAction = _input.actions.FindAction("Select");
        }

        public GraphicsView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Menus/Settings/Graphics");
            _tree = template.CloneTree();
        }

        protected override void Enable()
        {
            _activeParameter = ActiveParameter.Resolution;
            _displayModeChoice.AddToClassList("unfocused");
            
            _upAction.started += OnUpDown;
            _downAction.started += OnUpDown;
            _leftAction.started += OnLeftChange;
            _rightAction.started += OnRightChange;
            _selectAction.started += OnSelect;
        }

        protected override void Disable()
        {
            _upAction.started -= OnUpDown;
            _downAction.started -= OnUpDown;
            _leftAction.started -= OnLeftChange;
            _rightAction.started -= OnRightChange;
            _selectAction.started -= OnSelect;
        }

        private void OnUpDown(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.Resolution)
            {
                _activeParameter = ActiveParameter.DisplayMode;
                _displayModeChoice.RemoveFromClassList("unfocused");
                _resolutionChoice.AddToClassList("unfocused");
            }
            else
            {
                _activeParameter = ActiveParameter.Resolution;
                _resolutionChoice.RemoveFromClassList("unfocused");
                _displayModeChoice.AddToClassList("unfocused");
            }
        }

        private void SetGraphicsValues()
        {
            SetResolutionText();
            SetDisplayModeText();
        }

        private void OnSelect(InputAction.CallbackContext context) => Screen.SetResolution(
            _resolutions[_resolutionIndex].width, _resolutions[_resolutionIndex].height, GetDisplayMode());

        private FullScreenMode GetDisplayMode()
        {
            var displayMode = _displayModes[_displayModeIndex] switch
            {
                "Full Screen" => FullScreenMode.FullScreenWindow,
                "Windowed" => FullScreenMode.Windowed,
                _ => throw new ArgumentException()
            };

            return displayMode;
        }

        private void OnLeftChange(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.Resolution)
                ChangeResolutionLeft();
            if (_activeParameter == ActiveParameter.DisplayMode)
                ChangeDisplayModeLeft();
        }

        private void OnRightChange(InputAction.CallbackContext context)
        {
            if (_activeParameter == ActiveParameter.Resolution)
                ChangeResolutionRight();
            if (_activeParameter == ActiveParameter.DisplayMode)
                ChangeDisplayModeRight();
        }

        private void ChangeResolutionLeft()
        {
            _resolutionIndex = (_resolutionIndex - 1) % _resolutions.Length;

            if (_resolutionIndex == -1)
                _resolutionIndex = _resolutions.Length - 1;

            SetResolutionText();
        }

        private void ChangeResolutionRight()
        {
            _resolutionIndex = (_resolutionIndex + 1) % _resolutions.Length;

            SetResolutionText();
        }

        private void ChangeDisplayModeLeft()
        {
            _displayModeIndex = (_displayModeIndex - 1) % _displayModes.Length;

            if (_displayModeIndex == -1)
                _displayModeIndex = _displayModes.Length - 1;

            SetDisplayModeText();
        }

        private void ChangeDisplayModeRight()
        {
            _displayModeIndex = (_displayModeIndex + 1) % _displayModes.Length;

            SetDisplayModeText();
        }

        private void SetResolutionText() => _resolutionChoice.text = _resolutions[_resolutionIndex].ToString();

        private void SetDisplayModeText()
        {
            _displayModeChoice.text = _displayModes[_displayModeIndex];
            _displayModeChoice.viewDataKey = _displayModeKeys[_displayModeIndex];
            
            _menuManager.Localize(_displayModeChoice);
        }
    }
}
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
        [SerializeField] private bool _loadMainMenu;
        [SerializeField] private bool _loadGameMenu;
        
        
        private MainMenuView _mainMenu;
        private GameMenuView _gameMenu;
        
        public event Action Return;
        
        private VisualElement _rootVisualElement;

        private VisualElement _screen;
        
        private PlayerInput _input;
        private InputAction _returnAction;

        private void OnValidate()
        {
            if (_loadMainMenu && _loadGameMenu)
            {
                _loadGameMenu = false;
                _loadMainMenu = false;
            }
        }

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _screen = _rootVisualElement.Q<VisualElement>("screen");

            _input = GetComponent<PlayerInput>();
            _returnAction = _input.actions.FindAction("Return");
        }

        private void Start()
        {
            if (_loadMainMenu)
            {
                _mainMenu = new MainMenuView(_rootVisualElement, this, this);
                _mainMenu.ShowSelf();
            }

            if (_loadGameMenu)
            {
                _gameMenu = new GameMenuView(_rootVisualElement, this, this);
                Return += ShowMenu;
            }
        }

        private void OnEnable() => _returnAction.started += OnReturn;
        
        private void OnDisable() => _returnAction.started -= OnReturn;
        
        public void ShowSelf() => _screen.style.display = DisplayStyle.Flex;

        private void ShowMenu()
        {
            if (_gameMenu.Shown)
                return;
            
            _gameMenu.ShowSelf();
        }

        private void OnReturn(InputAction.CallbackContext obj) => Return?.Invoke();
    }
}
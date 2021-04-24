using System;
using UI.MenuViews;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(PlayerInput))]
    public class MenuManager : MonoBehaviour, IMenuView
    {
        [SerializeField] private LocalizedStringTable _table;
        
        [SerializeField] private bool _loadMainMenu;
        [SerializeField] private bool _loadGameMenu;

        private StringTable _localTable;

        public PlayerInput Input => _input;

        private MainMenuView _mainMenu;
        private GameMenuView _gameMenu;
        
        public event Action Return;
        
        private VisualElement _rootVisualElement;

        private bool _loaded;
        
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

            _input = GetComponent<PlayerInput>();
            _returnAction = _input.actions.FindAction("Return");
        }

        private void OnEnable()
        {
            _table.TableChanged += OnTableChanged;
            _returnAction.started += OnReturn;
        }

        private void OnDisable()
        {
            _table.TableChanged -= OnTableChanged;
            _returnAction.started -= OnReturn;
        }

        public void ShowSelf()
        {
        }

        public void HideSelf()
        {
        }

        public void LocalizeRecursively(VisualElement element)
        {
            var elementHierarchy = element.hierarchy;
            var numChildren = elementHierarchy.childCount;
            for (var i = 0; i < numChildren; i++)
            {
                var child = elementHierarchy.ElementAt(i);
                Localize(child);
            }

            for (var i = 0; i < numChildren; i++)
            {
                var child = elementHierarchy.ElementAt(i);
                var childHierarchy = child.hierarchy;
                var numGrandChildren = childHierarchy.childCount;
                if (numGrandChildren != 0)
                    LocalizeRecursively(child);
            }
        }

        public void Localize(VisualElement element)
        {
            if (!(element is TextElement))
                return;

            var textElement = (TextElement) element;
            var key = textElement.viewDataKey;
            
            if (string.IsNullOrEmpty(key) || key[0] != '_') 
                return;
            
            key = key.TrimStart('_');
            var entry = _localTable[key];
            if (entry != null)
                textElement.text = entry.LocalizedValue;
            else
                Debug.LogWarning($"No {_localTable.LocaleIdentifier.Code} translation for key: '{key}'");
        }

        private void OnTableChanged(StringTable table)
        {
            var handle = _table.GetTable();
            
            if (handle.IsDone)
                OnLocalTableLoaded(handle);
            else
                handle.Completed += OnLocalTableLoaded;
        }

        private void OnLocalTableLoaded(AsyncOperationHandle<StringTable> handle)
        {
            _localTable = handle.Result;
            LoadMenu();
            
            LocalizeRecursively(_rootVisualElement);
            _rootVisualElement.MarkDirtyRepaint();
        }

        private void LoadMenu()
        {
            if (_loaded)
                return;
            
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

            _loaded = true;
        }

        private void ShowMenu()
        {
            if (_gameMenu.Shown)
                return;
            
            _gameMenu.ShowSelf();
        }

        private void OnReturn(InputAction.CallbackContext context) => Return?.Invoke();
    }
}
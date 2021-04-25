using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class CreditsView : MenuView
    {
        private Image _authorImage;
        private Label _authorLabel;
        
        private readonly Sprite[] _authorSprites =
        {
            Resources.Load<Sprite>("UI/AuthorImages/Ii"),
            Resources.Load<Sprite>("UI/AuthorImages/Nikki"),
            Resources.Load<Sprite>("UI/AuthorImages/Rutik")
        };
        private readonly string[] _authorLabels = {"Ii", "Nikki", "Rutik"};
        private readonly string[] _authorKeys = {"_ii", "_nikki", "_rutik"};

        private int _index;

        private PlayerInput _input;
        private InputAction _leftAction;
        private InputAction _rightAction;

        public CreditsView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Menus/Settings/Credits");
            _tree = template.CloneTree();
        }

        protected override void SetUp()
        {
            _authorImage = _tree.Q<Image>("author__image");
            _authorLabel = _tree.Q<Label>("author__label");
            SetAuthors();
            
            _input = _menuManager.Input;
            _leftAction = _input.actions.FindAction("Left");
            _rightAction = _input.actions.FindAction("Right");
        }

        protected override void Enable()
        {
            _leftAction.started += ChangeAuthorsLeft;
            _rightAction.started += ChangeAuthorsRight;
        }

        protected override void Disable()
        {
            _leftAction.started -= ChangeAuthorsLeft;
            _rightAction.started -= ChangeAuthorsRight;
        }

        private void ChangeAuthorsLeft(InputAction.CallbackContext context)
        {
            _index = (_index - 1) % _authorSprites.Length;

            if (_index == -1)
                _index = _authorSprites.Length - 1;

            SetAuthors();
        }

        private void ChangeAuthorsRight(InputAction.CallbackContext context)
        {
            _index = (_index + 1) % _authorSprites.Length;
            
            SetAuthors();
        }

        private void SetAuthors()
        {
            _authorImage.sprite = _authorSprites[_index];
            _authorLabel.text = _authorLabels[_index];
            _authorLabel.viewDataKey = _authorKeys[_index];
            
            _menuManager.Localize(_authorLabel);
        }
    }
}
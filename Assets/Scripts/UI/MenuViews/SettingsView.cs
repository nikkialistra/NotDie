using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class SettingsView : MenuView
    {
        private Button _audio;
        private Button _graphics;
        private Button _controls;
        private Button _hud;
        private Button _language;
        private Button _credits;

        private AudioView _audioView;
        private GraphicsView _graphicsView;
        private LanguageView _languageView;

        public SettingsView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Menus/Settings/Settings");
            _tree = template.CloneTree();
        }

        protected override void SetUp()
        {
            _audio = _tree.Q<Button>("audio");
            _graphics = _tree.Q<Button>("graphics");
            _controls = _tree.Q<Button>("controls");
            _hud = _tree.Q<Button>("hud");
            _language = _tree.Q<Button>("language");
            _credits = _tree.Q<Button>("credits");
        }

        protected override void Enable()
        {
            _audio.clicked += OnAudioClicked;
            _graphics.clicked += OnGraphicsClicked;
            _language.clicked += OnLanguageClicked;
            
            ResetFocus();
        }

        protected override void Disable()
        {
            _audio.clicked -= OnAudioClicked;
            _graphics.clicked -= OnGraphicsClicked;
            _language.clicked -= OnLanguageClicked;
        }

        private void ResetFocus()
        {
            _graphics.Focus();
            _controls.Focus();
            _hud.Focus();
            _language.Focus();
            _credits.Focus();

            _audio.Focus();
        }

        private void OnGraphicsClicked()
        {
            Focused = _graphics;
            HideSelf();
            _graphicsView ??= new GraphicsView(_root, this, _menuManager);
            _graphicsView.ShowSelf();
        }

        private void OnAudioClicked()
        {
            Focused = _audio;
            HideSelf();
            _audioView ??= new AudioView(_root, this, _menuManager);
            _audioView.ShowSelf();
        }

        private void OnLanguageClicked()
        {
            Focused = _language;
            HideSelf();
            _languageView ??= new LanguageView(_root, this, _menuManager);
            _languageView.ShowSelf();
        }
    }
}
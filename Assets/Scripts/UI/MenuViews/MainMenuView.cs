using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class MainMenuView : MenuView
    {
        private VisualElement _screen;
        private Button _play;
        private Button _settings;
        private Button _vault;
        private Button _profiles;
        private Button _quit;

        private SettingsView _settingsView;

        public MainMenuView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/MainMenu");
            _tree = template.CloneTree();
        }

        protected override void SetUp()
        {
            _screen = _tree.Q<VisualElement>("screen");
            _play = _tree.Q<Button>("play");
            _settings = _tree.Q<Button>("settings");
            _vault = _tree.Q<Button>("vault");
            _profiles = _tree.Q<Button>("profiles");
            _quit = _tree.Q<Button>("quit");

            _play.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(Play));
            _settings.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(Settings));
            _quit.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(Quit));
        }

        protected override void Enable()
        {
            _menuManager.Return -= ShowParent;
            _play.Focus();
        }

        private static IEnumerator WaitForButtonPress(Action action)
        {
            yield return new WaitForSeconds(0.15f);
            action();
        }

        private static void Play() => SceneManager.LoadScene("FirstStage", LoadSceneMode.Single);

        private void Settings()
        {
            Focused = _settings;
            ShowParent();
            _settingsView ??= new SettingsView(_root, this, _menuManager);
            _settingsView.ShowSelf();
        }

        private static void Quit() => Application.Quit();
    }
}
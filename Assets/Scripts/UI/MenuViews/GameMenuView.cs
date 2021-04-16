using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class GameMenuView : MenuView
    {
        public bool Shown { get; private set; }
        
        private Button _resumeGame;
        private Button _restart;
        private Button _settings;
        private Button _saveAndExit;
        private Button _quitGame;

        private SettingsView _settingsView;

        public GameMenuView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/GameMenu.uxml");
            _tree = template.CloneTree();
        }

        protected override void SetUpBindings()
        {
            _resumeGame = _tree.Q<Button>("resume_game");
            _restart = _tree.Q<Button>("restart");
            _settings = _tree.Q<Button>("settings");
            _saveAndExit = _tree.Q<Button>("save_and_exit");
            _quitGame = _tree.Q<Button>("quit_game");

            _resumeGame.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(ResumeGame));
            _restart.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(Restart));
            _settings.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(Settings));
            _saveAndExit.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(SaveAndExit));
            _quitGame.clicked += () => _menuManager.StartCoroutine(WaitForButtonPress(Quit));
        }

        protected override void Enable()
        {
            Time.timeScale = 0;

            Shown = true;
            
            _menuManager.Return -= HideSelf;
            _menuManager.Return += ResumeGame;
                
            ResetFocus();
        }

        private void ResetFocus()
        {
            _restart.Focus();
            _settings.Focus();
            _saveAndExit.Focus();
            _quitGame.Focus();

            _resumeGame.Focus();
        }

        private static IEnumerator WaitForButtonPress(Action action)
        {
            yield return new WaitForSecondsRealtime(0.15f);
            Time.timeScale = 1;
            action();
        }

        private void ResumeGame()
        {
            _menuManager.Return -= ResumeGame;
            
            Shown = false;
            Focused = _resumeGame;
            
            Time.timeScale = 1;
            HideSelf();
        }

        private static void Restart() => SceneManager.LoadScene("FirstStage", LoadSceneMode.Single);

        private void SaveAndExit() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

        private void Settings()
        {
            _menuManager.Return -= ResumeGame;
            Focused = _settings;
            HideSelf();
            _settingsView ??= new SettingsView(_root, this, _menuManager);
            _settingsView.ShowSelf();
        }

        private static void Quit() => Application.Quit();
    }
}
﻿using UnityEditor;
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

        public SettingsView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/Settings.uxml");
            _tree = template.CloneTree();
        }

        protected override void SetUpBindings()
        {
            _audio = _tree.Q<Button>("audio");
            _graphics = _tree.Q<Button>("graphics");
            _controls = _tree.Q<Button>("controls");
            _hud = _tree.Q<Button>("hud");
            _language = _tree.Q<Button>("language");
            _credits = _tree.Q<Button>("credits");

            _language.clicked += Language;
        }

        protected override void Enable() => ResetFocus();

        private void ResetFocus()
        {
            _graphics.Focus();
            _controls.Focus();
            _hud.Focus();
            _language.Focus();
            _credits.Focus();

            _audio.Focus();
        }

        private static void Language() => Debug.Log("Language");
    }
}
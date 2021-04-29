using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MenuViews
{
    public class AudioView : MenuView
    {
        private Slider _masterVolumeSlider;
        private Label _masterVolumeValue;
        
        private Slider _effectsVolumeSlider;
        private Label _effectsVolumeValue;
        
        private Slider _musicVolumeSlider;
        private Label _musicVolumeValue;

        public AudioView(VisualElement root, IMenuView parent, MenuManager menuManager) : base(root, parent, menuManager)
        {
            var template = Resources.Load<VisualTreeAsset>("UI/Menus/Settings/Audio");
            _tree = template.CloneTree();
        }

        protected override void SetUp()
        {
            _masterVolumeSlider = _tree.Q<Slider>("master_volume__slider");
            _masterVolumeValue = _tree.Q<Label>("master_volume__value");
            
            _effectsVolumeSlider = _tree.Q<Slider>("effects_volume__slider");
            _effectsVolumeValue = _tree.Q<Label>("effects_volume__value");
            
            _musicVolumeSlider = _tree.Q<Slider>("music_volume__slider");
            _musicVolumeValue = _tree.Q<Label>("music_volume__value");
            
            InitializeSliders();
            SetSliderValues();

            _masterVolumeSlider.RegisterCallback<ChangeEvent<float>>(OnMasterVolumeChange);
            _effectsVolumeSlider.RegisterCallback<ChangeEvent<float>>(OnEffectsVolumeChange);
            _musicVolumeSlider.RegisterCallback<ChangeEvent<float>>(OnMusicVolumeChange);
        }


        private void InitializeSliders()
        {
            if (!_menuManager.GameSettings.Loaded)
                return;
            
            _masterVolumeSlider.value = _menuManager.GameSettings.MasterVolume;
            _effectsVolumeSlider.value = _menuManager.GameSettings.EffectsVolume;
            _musicVolumeSlider.value = _menuManager.GameSettings.MusicVolume;
        }

        private void SetSliderValues()
        {
            ShowSliderValueNormalized(_masterVolumeValue, _masterVolumeSlider.value);
            ShowSliderValueNormalized(_effectsVolumeValue, _effectsVolumeSlider.value);
            ShowSliderValueNormalized(_musicVolumeValue, _musicVolumeSlider.value);
        }

        protected override void Enable()
        {
            _effectsVolumeSlider.Focus();
            _musicVolumeSlider.Focus();
            
            _masterVolumeSlider.Focus();
        }

        protected override void Disable()
        {
            _menuManager.GameSettings.MasterVolume = _masterVolumeSlider.value;
            _menuManager.GameSettings.EffectsVolume = _effectsVolumeSlider.value;
            _menuManager.GameSettings.MusicVolume = _musicVolumeSlider.value;
            
            _menuManager.GameSettings.Save();
        }

        private void OnMasterVolumeChange(ChangeEvent<float> slider) => ShowSliderValueNormalized(_masterVolumeValue, slider.newValue);

        private void OnEffectsVolumeChange(ChangeEvent<float> slider) => ShowSliderValueNormalized(_effectsVolumeValue, slider.newValue);

        private void OnMusicVolumeChange(ChangeEvent<float> slider) => ShowSliderValueNormalized(_musicVolumeValue, slider.newValue);

        private static void ShowSliderValueNormalized(Label label, float value) => label.text = Mathf.RoundToInt(value * 20).ToString();
    }
}
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
            
            _masterVolumeSlider.RegisterCallback<ChangeEvent<float>, Label>(OnVolumeChange, _masterVolumeValue);
            _effectsVolumeSlider.RegisterCallback<ChangeEvent<float>, Label>(OnVolumeChange, _effectsVolumeValue);
            _musicVolumeSlider.RegisterCallback<ChangeEvent<float>, Label>(OnVolumeChange, _musicVolumeValue);

            SetAudioValues();
        }

        private void OnVolumeChange(ChangeEvent<float> slider, Label label) => ShowSliderValueNormalized(label, slider.newValue);

        private void SetAudioValues()
        {
            ShowSliderValueNormalized(_masterVolumeValue, _masterVolumeSlider.value);
            ShowSliderValueNormalized(_effectsVolumeValue, _effectsVolumeSlider.value);
            ShowSliderValueNormalized(_musicVolumeValue, _musicVolumeSlider.value);
        }

        private void ShowSliderValueNormalized(Label label, float value) => label.text = Mathf.RoundToInt(value * 20).ToString();

        protected override void Enable()
        {
            _effectsVolumeSlider.Focus();
            _musicVolumeSlider.Focus();
            
            _masterVolumeSlider.Focus();
        }
    }
}
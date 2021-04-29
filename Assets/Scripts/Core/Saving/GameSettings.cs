using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Core.Saving
{
    public enum FontStyle
    {
        Pixelated,
        Regular
    }

    public enum FontSize
    {
        Small,
        Medium,
        Big
    }

    public enum ShowTimer
    {
        True,
        False
    }

    public class GameSettings : MonoBehaviour
    {
        public event Action Change;
        
        public bool Loaded { get; private set; }

        public float MasterVolume;
        public float EffectsVolume;
        public float MusicVolume;
        
        public string Resolution;
        public string DisplayMode;

        public FontStyle FontStyle;
        public FontSize FontSize;
        public ShowTimer ShowTimer;

        public string Language;

        private string _savePath;

        private void Awake()
        {
            _savePath = Path.Combine(Application.persistentDataPath, "settings.json");
            Load();
        }
        
        public void Save()
        {
            var saveData = JsonUtility.ToJson(this);
            
            try
            {
                File.WriteAllText(_savePath, saveData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {_savePath} with exception {e}");
            }
            
            Change?.Invoke();
        }

        public void Apply()
        {
            foreach (var resolution in Screen.resolutions.Where(resolution => resolution.ToString() == Resolution))
                Screen.SetResolution(resolution.width, resolution.height, GetDisplayMode());
            
            foreach (var language in LocalizationSettings.AvailableLocales.Locales.Where(language => language.ToString() == Language))
                LocalizationSettings.SelectedLocale = language;
        }
        
        private FullScreenMode GetDisplayMode()
        {
            var displayMode = DisplayMode switch
            {
                "_fullscreen" => FullScreenMode.FullScreenWindow,
                "_windowed" => FullScreenMode.Windowed,
                _ => throw new ArgumentException()
            };

            return displayMode;
        }

        private void Load()
        {
            if (!File.Exists(_savePath))
            {
                CreateDefaultSettings();
                
                Loaded = true;
                return;
            }

            try
            {
                var saveData = File.ReadAllText(_savePath);
                JsonUtility.FromJsonOverwrite(saveData, this);
                
                Loaded = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {_savePath} with exception {e}");
            }
        }

        private void CreateDefaultSettings()
        {
            MasterVolume = 5;
            EffectsVolume = 5;
            MusicVolume = 5;

            Resolution = Screen.resolutions[0].ToString();
            DisplayMode = "_windowed";

            FontStyle = FontStyle.Pixelated;
            FontSize = FontSize.Medium;
            ShowTimer = ShowTimer.True;

            Language = "English";
        }
    }
}
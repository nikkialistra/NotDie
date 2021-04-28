using System;
using System.IO;
using UnityEngine;

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

    public class Settings : MonoBehaviour
    {
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
        }

        private void Load()
        {
            if (!File.Exists(_savePath))
                return;
            
            try
            {
                var saveData = File.ReadAllText(_savePath);
                JsonUtility.FromJsonOverwrite(saveData, this);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {_savePath} with exception {e}");
            }

            Loaded = true;
        }
    }
}
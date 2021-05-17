using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views
{
    [RequireComponent(typeof(UIDocument))]
    public class GameOverView : MonoBehaviour
    {
        private VisualElement _rootVisualElement;

        private Label _gameOverText;

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            
            _gameOverText = _rootVisualElement.Q<Label>("gameover");
        }

        public void GameOver()
        {
            _gameOverText.style.display = DisplayStyle.Flex;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI.Views
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenuView : MonoBehaviour
    {
        private VisualElement _rootVisualElement;

        private Button _play;
        private Button _options;
        private Button _vault;
        private Button _profiles;
        private Button _quit;

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            
            _play = _rootVisualElement.Q<Button>("play");
            _options = _rootVisualElement.Q<Button>("options");
            _vault = _rootVisualElement.Q<Button>("vault");
            _profiles = _rootVisualElement.Q<Button>("profiles");
            _quit = _rootVisualElement.Q<Button>("quit");

            _play.clicked += Play;
            _quit.clicked += Quit;
        }

        private void Start() => _play.Focus();

        private static void Play() => SceneManager.LoadScene("FirstStage", LoadSceneMode.Single);

        private static void Quit() => Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private Text _timerLevelText;
        private Text _informationLevelText;
        private Text _amountTinaHintText;

        private GameObject _pausePage;
        private GameObject _winPage;
        private GameObject _gameOverPage;
        private GameObject _hintPage;
        private GameObject _bellAnimation;

        private Button _pauseMenuButton;
        private Button _xRayButton;

        private void Awake()
        {
            _pausePage = GameObject.Find("PausePage");
            _winPage = GameObject.Find("WinPage");
            _gameOverPage = GameObject.Find("GameOverPage");
            _hintPage = GameObject.Find("HintPage");
            _bellAnimation = GameObject.Find("Bell");

            _pauseMenuButton = GameObject.Find("MenuButton").GetComponent<Button>();
            _timerLevelText = GameObject.Find("TimerText").GetComponent<Text>();
            _informationLevelText = GameObject.Find("InformationLevelText").GetComponent<Text>();
            _amountTinaHintText = GameObject.Find("AmoutTinaHintText").GetComponent<Text>();

            _xRayButton = GameObject.Find("XRayButton").GetComponent<Button>();
        }

        private void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);
            _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedXRay);

        }

        public void OnInitializedLevel()
        {
            _pausePage.SetActive(false);
            _winPage.SetActive(false);
            _gameOverPage.SetActive(false);
            _hintPage.SetActive(false);
            _bellAnimation.SetActive(false);

        }

        public void OnGetInformationLevel(string levelName, int levelNumber, int amountObjects)
        {
            _informationLevelText.text = string.Concat("Fase ", levelNumber, ": ", levelName, " Objetos Restantes: ",amountObjects);
        }

        public void OnCountdownPerfomed(float currentTime)
        {
            string minSec = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
            _timerLevelText.text = minSec;
        }

        public void OnPausedGame() => _pausePage.SetActive(true);

        public void OnUnPausedGame() => _pausePage.SetActive(false);
    }
}
using Objects;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private Text _timerLevelText;
        private Text _informationLevelText;
        private Text _amountTinaHintText;
        private Text _amountItemsLeftText;

        private Animator _barsAnimation;

        private GameObject _pausePage;
        private GameObject _winPage;
        private GameObject _gameOverPage;
        private GameObject _hintPage;
        private GameObject _bellAnimation;
        private GameObject _guessingPage;

        private GameObject _errorIcon;
        private GameObject _errorIcon2;

        [SerializeField] private Button[] _answersButton;
        private Button _pauseMenuButton;
        private Button _closeButton;
        private Button _returnButton;
        private Button _xRayButton;

        private void Awake()
        {
            _pausePage = GameObject.Find("PausePage");
            _winPage = GameObject.Find("WinPage");
            _gameOverPage = GameObject.Find("GameOverPage");
            _hintPage = GameObject.Find("HintPage");
            _bellAnimation = GameObject.Find("Bell");
            _guessingPage = GameObject.Find("GuessingPage");


            _errorIcon = _guessingPage.transform.Find("Panel").transform.Find("ErrorIcon").gameObject;
            _errorIcon2 = _guessingPage.transform.Find("Panel").transform.Find("ErrorIcon2").gameObject;

            _barsAnimation = GameObject.Find("Canvas").GetComponent<Animator>();


            _answersButton = _guessingPage.GetComponentsInChildren<Button>();
            _pauseMenuButton = GameObject.Find("MenuButton").GetComponent<Button>();
            _closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
            _returnButton = GameObject.Find("ReturnButton").GetComponent<Button>();
            _xRayButton = GameObject.Find("XRayButton").GetComponent<Button>();


            _timerLevelText = GameObject.Find("TimerText").GetComponent<Text>();
            _informationLevelText = GameObject.Find("InformationLevelText").GetComponent<Text>();
            _amountTinaHintText = GameObject.Find("AmoutTinaHintText").GetComponent<Text>();
            _amountItemsLeftText = GameObject.Find("AmountItemsLeft").GetComponent<Text>();

        }

        private void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);
            _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedXRay);
            _xRayButton.onClick.AddListener(PlayHudAnimation);

            _closeButton.onClick.AddListener(FindObjectOfType<EventManager>().OnUnPausedGame);
            _returnButton.onClick.AddListener(FindObjectOfType<EventManager>().OnUnPausedGame);

        }



        public void PlayHudAnimation()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            _barsAnimation.enabled = true;

            if (gameManager.CurrentSkill == SkillState.XRay)
                _barsAnimation.SetBool("OnXray", true);
            else
                _barsAnimation.SetBool("OnXray", false);


        }

        internal void ShowAmoutItemsLeft(int amountItemsLeft)
        {
            _amountItemsLeftText.text = string.Concat("Objetos Restantes: ", amountItemsLeft.ToString());
        }

        internal void ShowCountdownPerfomedText(float currentTime)
        {
            string minSec = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
            _timerLevelText.text = minSec;
        }

        internal void ChosenCorrect()
        {
            Invoke(nameof(OnCollected), 0f);
            _guessingPage.SetActive(false);

        }


        internal void OverChancesChose()
        {
            _errorIcon.gameObject.SetActive(false);
            _errorIcon2.gameObject.SetActive(false);
            _guessingPage.SetActive(false);
        }

        // -------------------- OBSERVERS ------------


        public void OnInitializedLevel()
        {
            _pausePage.SetActive(false);
            _winPage.SetActive(false);
            _gameOverPage.SetActive(false);
            _hintPage.SetActive(false);
            _bellAnimation.SetActive(false);
            _guessingPage.SetActive(false);
            _barsAnimation.enabled = false;

            _informationLevelText.text = string.Concat("Fase ", 3, ": ", "Sala de aula");

        }

        internal void OnPausedGame() => _pausePage.SetActive(true);

        internal void OnUnPausedGame() => _pausePage.SetActive(false);

        internal void OnGotQuestion(string nameObject, Sprite itemSprite, Sprite normalModal, Sprite correctModalName, Sprite incorrectModal)
        {
            Text nameObjectText = _answersButton[0].GetComponentInChildren<Text>();
            nameObjectText.text = nameObject;

            int[] randomNumbers = new int[3] { UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2) , UnityEngine.Random.Range(0, 2) };
            
            for(int i = 0; i < 3; i++)
            {
                _answersButton[i].gameObject.transform.SetSiblingIndex(randomNumbers[i]);
                _answersButton[i].interactable = true;

            }
   
            Image itemImage = _guessingPage.transform.Find("Panel").transform.Find("ItemImage").GetComponent<Image>();

            itemImage.sprite = itemSprite;

            _errorIcon.gameObject.SetActive(false);
            _errorIcon2.gameObject.SetActive(false);

            _guessingPage.SetActive(true);

        }

        internal void OnChosenIncorrect()
        {
            
            if (!_errorIcon.activeSelf)
            {
                _errorIcon.SetActive(true);
                _errorIcon2.SetActive(false);
            }
            else
            {
                _errorIcon2.SetActive(true);
                Invoke(nameof(OverChancesChose), 0.5f);
            }
        }

        internal void OnCollected(Collectable coll)
        {
            _guessingPage.SetActive(false);
        }
    }
}
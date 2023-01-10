using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Statics;
using Random = System.Random;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private Text _timerLevelText;
        private Text _informationLevelText;
        private Text _amountTinaHintText;
        private Text _amountItemsLeftText;
        private Text _currentHintText;

        private Animator _barsAnimation;

        private GameObject _pausePage;
        private GameObject _winPage;
        private GameObject _gameOverPage;
        private GameObject _hintPage;
        private GameObject _bellAnimation;
        private GameObject _guessingPage;


        [SerializeField] protected Image[] _errorIcons;
        private Image _redImage;
        private Image _greenImage;
        private Image _xRayBarImage;
        private Image _xRayCooldownImage;
        private Image _xRayTimerImage;
        private Image _fingerprintTimerImage;
        private Image _fingerprintCooldownImage;
        private Image _nightVisionTimerImage;
        private Image _nightVisionCooldownImage;

        protected Button[] _answersButton;
        protected Button _hintButton;
        protected Button _pauseMenuButton;
        private Button _closeButton;
        private Button _returnButton;
        protected Button _xRayButton;
        protected Button _fingerprintButton;
        protected Button _nightVisionButton;
        private Button _phasesButton;
        private Button _mainMenuButton;
        protected Button _closeHintButton;

        protected virtual void Awake()
        {
            _pausePage = GameObject.Find("PausePage");
            _winPage = GameObject.Find("WinPage");
            _gameOverPage = GameObject.Find("GameOverPage");
            _hintPage = GameObject.Find("HintPage");
            _bellAnimation = GameObject.Find("Bell");
            _guessingPage = GameObject.Find("GuessingPage");


            _barsAnimation = GameObject.Find("Canvas").GetComponent<Animator>();

            _errorIcons = _guessingPage.transform.Find("Panel").transform.Find("ErrorIcons").GetComponentsInChildren<Image>();
            _redImage = _guessingPage.transform.Find("RedImage").GetComponent<Image>();
            _greenImage = _guessingPage.transform.Find("GreenImage").GetComponent<Image>();
            _xRayBarImage = GameObject.Find("XRayBar").GetComponent<Image>();
            _xRayCooldownImage = GameObject.Find("XRayCooldownImage").GetComponent<Image>();
            _xRayTimerImage = GameObject.Find("XRayTimerImage").GetComponent<Image>();
            _fingerprintTimerImage = GameObject.Find("FingerprintTimerImage").GetComponent<Image>();
            _fingerprintCooldownImage = GameObject.Find("FingerprintCooldownImage").GetComponent<Image>();
            _nightVisionTimerImage = GameObject.Find("NightVisionTimerImage").GetComponent<Image>();
            _nightVisionCooldownImage = GameObject.Find("NightVisionCooldownImage").GetComponent<Image>();

            _answersButton = _guessingPage.GetComponentsInChildren<Button>();
            _pauseMenuButton = GameObject.Find("MenuButton").GetComponent<Button>();
            _closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
            _returnButton = GameObject.Find("ReturnButton").GetComponent<Button>();
            _xRayButton = GameObject.Find("XRayButton").GetComponent<Button>();
            _fingerprintButton = GameObject.Find("FingerprintButton").GetComponent<Button>();
            _nightVisionButton = GameObject.Find("NightVisionButton").GetComponent<Button>();
            _hintButton = GameObject.Find("HintButton").GetComponent<Button>();
            _closeHintButton = GameObject.Find("ClosingHintButton").GetComponent<Button>();

            _timerLevelText = GameObject.Find("TimerText").GetComponent<Text>();
            _informationLevelText = GameObject.Find("InformationLevelText").GetComponent<Text>();
            _amountTinaHintText = GameObject.Find("AmoutTinaHintText").GetComponent<Text>();
            _amountItemsLeftText = GameObject.Find("AmountItemsLeft").GetComponent<Text>();
            _currentHintText = GameObject.Find("HintText").GetComponent<Text>();


        }





        protected virtual void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);
            _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedXRay);
            _xRayButton.onClick.AddListener(PlayHudAnimation);

            _fingerprintButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedFingerprint);
            _fingerprintButton.onClick.AddListener(PlayHudAnimation);

            _nightVisionButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedNightVision);
            _nightVisionButton.onClick.AddListener(PlayHudAnimation);

            _closeButton.onClick.AddListener(FindObjectOfType<EventManager>().OnUnPausedGame);
            _returnButton.onClick.AddListener(FindObjectOfType<EventManager>().OnUnPausedGame);

            _answersButton[0].onClick.AddListener(FindObjectOfType<EventManager>().OnChosenCorrect);
            _answersButton[1].onClick.AddListener(FindObjectOfType<EventManager>().OnChosenIncorrect);
            _answersButton[2].onClick.AddListener(FindObjectOfType<EventManager>().OnChosenIncorrect);

            _hintButton.onClick.AddListener(FindObjectOfType<EventManager>().OnGotHint);
            _closeHintButton.onClick.AddListener(CloseHintPage);
        }

        protected virtual void PlayHudAnimation()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            switch (gameManager.CurrentSkill)
            {
                case SkillState.Normal:
                    _barsAnimation.SetBool("OnXRay", false);
                    _barsAnimation.SetBool("OnFingerprint", false);
                    _barsAnimation.SetBool("OnNightVision", false);

                    break;
                case SkillState.XRay:
                    _barsAnimation.SetBool("OnXRay", true);
                    _barsAnimation.SetBool("OnFingerprint", false);
                    _barsAnimation.SetBool("OnNightVision", false);
                    break;
                case SkillState.NightVision:
                    _barsAnimation.SetBool("OnNightVision", true);
                    _barsAnimation.SetBool("OnFingerprint", false);
                    _barsAnimation.SetBool("OnXRay", false);
                    break;
                case SkillState.Fingerprint:
                    _barsAnimation.SetBool("OnFingerprint", true);
                    _barsAnimation.SetBool("OnXRay", false);
                    _barsAnimation.SetBool("OnNightVision", false);

                    break;
            }


        }

        protected void CloseHintPage() => StartCoroutine(nameof(WaitToCloseHintPage));

        internal void ShowAmountItemsLeft(int amountItemsLeft) => _amountItemsLeftText.text = string.Concat("Objetos Restantes: ", amountItemsLeft.ToString());

        internal void ShowCountdownPerfomedText(float currentTime)
        {
            string minSec = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
            _timerLevelText.text = minSec;
        }

        internal void ShowGameOverPage()
        {
            var eventManager = FindObjectOfType<EventManager>();

            _gameOverPage.SetActive(true);

            Button playAgainButton = GameObject.Find("PlayAgainButton").GetComponent<Button>();
            playAgainButton.onClick.AddListener(eventManager.RestartScene);

            Button phasesButton = GameObject.Find("PhasesButton").GetComponent<Button>();
            phasesButton.onClick.AddListener(eventManager.LoadLevelSelectScene);

            Button mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
            mainMenuButton.onClick.AddListener(eventManager.LoadMainMenuScene);

            if (!eventManager.CheckLastLevel())
            {
                Button nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
                nextLevelButton.onClick.AddListener(eventManager.LoadNextLevelScene);
            }

        }

        #region OBSERVERS


        internal void OnInitializedLevel()
        {
            _pausePage.SetActive(false);
            _winPage.SetActive(false);
            _gameOverPage.SetActive(false);
            _hintPage.SetActive(false);
            _bellAnimation.SetActive(false);
            _guessingPage.SetActive(false);

            _xRayBarImage.gameObject.SetActive(false);

            _xRayTimerImage.gameObject.SetActive(false);
            _xRayCooldownImage.gameObject.SetActive(false);
            _fingerprintTimerImage.gameObject.SetActive(false);
            _fingerprintCooldownImage.gameObject.SetActive(false);
            _nightVisionTimerImage.gameObject.SetActive(false);
            _nightVisionCooldownImage.gameObject.SetActive(false);


        }

        internal void OnGotLevelInformation(string namePhase)
        {
            string numberLevelString = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            int correctNumberLevel = int.Parse(numberLevelString) + 1;

            _informationLevelText.text = string.Concat("Fase ", correctNumberLevel, ": ", namePhase);

        }

        internal virtual void OnLoadedNextScene()
        {
            var transitionGameObject = GameObject.Find("Transition").GetComponent<Animator>();

            transitionGameObject.gameObject.SetActive(true);
            transitionGameObject.SetTrigger("FadeIn");
        }

        internal void OnFinishedTimerSkill() => PlayHudAnimation();

        internal void OnPausedGame()
        {
            var eventManager = FindObjectOfType<EventManager>();

            _pausePage.SetActive(true);

            Button mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
            mainMenuButton.onClick.AddListener(eventManager.LoadMainMenuScene);

            Button phasesButton = GameObject.Find("PhasesButton").GetComponent<Button>();
            phasesButton.onClick.AddListener(eventManager.LoadLevelSelectScene);

            Button restartSceneButton = GameObject.Find("RestartButton").GetComponent<Button>();
            restartSceneButton.onClick.AddListener(eventManager.RestartScene);
        }

        internal void OnUnPausedGame() => _pausePage.SetActive(false);

        internal void OnActivedXRay()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            Animator xRayBarAnimation = _xRayBarImage.GetComponent<Animator>();

            Debug.Log("Distancia atual: " + (int)gameManager.CurrentDistance);

            xRayBarAnimation.gameObject.SetActive(true);
            xRayBarAnimation.SetInteger("XRayDistance", (int)gameManager.CurrentDistance);
        }

        internal virtual void OnGotQuestion(string nameObject, Sprite itemSprite)
        {
            if (_guessingPage.activeSelf)
                return;

            string[] nameObjectsGeneral = GeneralTexts.Instance.NameObjects;

            Text nameObjectText = _answersButton[0].GetComponentInChildren<Text>();
            nameObjectText.text = nameObject.ToUpper();
            Text fakeNameA = _answersButton[1].GetComponentInChildren<Text>();
            Text fakeNameB = _answersButton[2].GetComponentInChildren<Text>();

            Random rng = new Random();
            var nameObjectsGeneralRandomOrder = nameObjectsGeneral.OrderBy(a => rng.Next()).ToList();

            // Fill the names wich lack.

            for (int i = 0; i < nameObjectsGeneral.Length; i++)
            {
                if (!string.Equals(nameObjectText.text, nameObjectsGeneralRandomOrder[i].ToUpper()))
                {
                    fakeNameA.text = nameObjectsGeneralRandomOrder[i].ToUpper();
                    break;
                }
                else
                {
                    if (i == nameObjectsGeneral.Length - 1)
                    {
                        i = 0;
                    }

                }
            }

            nameObjectsGeneralRandomOrder = nameObjectsGeneral.OrderBy(a => rng.Next()).ToList();

            for (int i = 0; i < nameObjectsGeneral.Length; i++)
            {
                if (!string.Equals(nameObjectText.text, nameObjectsGeneralRandomOrder[i].ToUpper()))
                {
                    if (!string.Equals(fakeNameA.text, nameObjectsGeneralRandomOrder[i].ToUpper()))
                    {
                        fakeNameB.text = nameObjectsGeneralRandomOrder[i].ToUpper();
                        break;
                    }
                }
                else
                {
                    if (i == nameObjectsGeneral.Length - 1)
                    {
                        i = 0;
                    }

                }
            }

            int[] randomNumbers = new int[3] { UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2) };

            for (int i = 0; i < 3; i++)
            {
                _answersButton[i].GetComponent<Animator>().Play("Normal");
                _answersButton[i].gameObject.transform.SetSiblingIndex(randomNumbers[i]);
                _answersButton[i].interactable = true;
            }

            Image itemImage = _guessingPage.transform.Find("Panel").transform.Find("ItemImage").GetComponent<Image>();

            itemImage.sprite = itemSprite;
            itemImage.SetNativeSize();

            for (int i = 0; i < _errorIcons.Length; i++)
                _errorIcons[i].gameObject.SetActive(false);

            _redImage.gameObject.SetActive(false);
            _greenImage.gameObject.SetActive(false);
            _guessingPage.SetActive(true);

        }

        internal virtual void OnChosenIncorrect()
        {
            _redImage.gameObject.SetActive(true);
            StartCoroutine(TimerForOpenOrClose(1f, _redImage.gameObject, false));

            if (_errorIcons[0].gameObject.activeSelf)
                _errorIcons[1].gameObject.SetActive(true);

            _errorIcons[0].gameObject.SetActive(true);

            if (_errorIcons[^1].gameObject.activeSelf)
            {
                _guessingPage.GetComponent<Animator>().SetTrigger("Closing");
                StartCoroutine(TimerForOpenOrClose(1f, _guessingPage, false));
            }
        }

        internal virtual void OnCollected()
        {
            _greenImage.gameObject.SetActive(true);
            StartCoroutine(TimerForOpenOrClose(1f, _greenImage.gameObject, false));

            StartCoroutine(TimerForOpenOrClose(1f, _guessingPage, false));
        }

        internal void OnGotHint(string hintText, int amountHints)
        {
            _currentHintText.text = hintText;
            _amountTinaHintText.text = amountHints.ToString();

            _hintPage.SetActive(true);
        }

        internal void OnUpgradeXRayVision()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            Animator xRayBarAnimation = _xRayBarImage.GetComponent<Animator>();

            Debug.Log("Distancia atual: " + (int)gameManager.CurrentDistance);

            xRayBarAnimation.gameObject.SetActive(true);
            xRayBarAnimation.SetInteger("XRayDistance", (int)gameManager.CurrentDistance);

        }

        internal void OnCountdownXrayTimer(float timer, float initialTimer)
        {
            //_xRayButton.interactable = false;
            _xRayButton.onClick.RemoveAllListeners();
            _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnUpgradeVision);

            if (timer > 0)
            {
                _xRayTimerImage.gameObject.SetActive(true);
                _xRayCooldownImage.gameObject.SetActive(true);
                _xRayBarImage.gameObject.SetActive(true);


                _xRayTimerImage.fillAmount = timer / initialTimer;
                _xRayCooldownImage.fillAmount = 1f;
            }
            else
            {
                _xRayTimerImage.fillAmount = 0;
                _xRayTimerImage.gameObject.SetActive(false);
                _xRayBarImage.gameObject.SetActive(false);
            }


        }

        internal void OnCountdownCooldownXray(float timer, float initialTimer)
        {
            _xRayButton.onClick.RemoveAllListeners();
            _xRayButton.interactable = false;

            if (timer > 0)
            {
                _xRayCooldownImage.fillAmount = timer / initialTimer;
            }
            else
            {
                _xRayCooldownImage.fillAmount = 0f;
                _xRayCooldownImage.gameObject.SetActive(false);
                _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedXRay);
                _xRayButton.onClick.AddListener(PlayHudAnimation);
                _xRayButton.interactable = true;
            }
        }

        internal void OnCountdownFingerprintTimer(float timer, float initialTimer)
        {
            _fingerprintButton.interactable = false;

            if (timer > 0)
            {
                _fingerprintTimerImage.gameObject.SetActive(true);
                _fingerprintCooldownImage.gameObject.SetActive(true);
                _fingerprintTimerImage.fillAmount = timer / initialTimer;
                _fingerprintCooldownImage.fillAmount = 1f;
            }
            else
            {
                _fingerprintTimerImage.fillAmount = 0;
                _fingerprintTimerImage.gameObject.SetActive(false);
            }
        }

        internal void OnCountdownFingerprintCooldown(float timer, float initialTimer)
        {
            if (timer > 0)
            {
                _fingerprintCooldownImage.fillAmount = timer / initialTimer;
            }
            else
            {
                _fingerprintCooldownImage.fillAmount = 0;
                _fingerprintCooldownImage.gameObject.SetActive(false);
                _fingerprintButton.interactable = true;
            }
        }

        internal void OnCountdownNightVisionTimer(float timer, float initialTimer)
        {
            _nightVisionButton.interactable = false;

            if (timer > 0)
            {
                _nightVisionTimerImage.gameObject.SetActive(true);
                _nightVisionCooldownImage.gameObject.SetActive(true);
                _nightVisionTimerImage.fillAmount = timer / initialTimer;
                _nightVisionCooldownImage.fillAmount = 1f;
            }
            else
            {
                _nightVisionTimerImage.fillAmount = 0;
                _nightVisionTimerImage.gameObject.SetActive(false);
            }
        }

        internal void OnCountdownNightVisionCooldown(float timer, float initialTimer)
        {
            if (timer > 0)
            {
                _nightVisionCooldownImage.fillAmount = timer / initialTimer;
            }
            else
            {
                _nightVisionCooldownImage.fillAmount = 0;
                _nightVisionCooldownImage.gameObject.SetActive(false);
                _nightVisionButton.interactable = true;
            }
        }

        internal void OnWonGame()
        {
            var eventManager = FindObjectOfType<EventManager>();

            _pausePage.SetActive(false);
            _guessingPage.SetActive(false);
            _gameOverPage.SetActive(false);
            _winPage.SetActive(true);

            Button playAgainButton = GameObject.Find("PlayAgainButton").GetComponent<Button>();
            playAgainButton.onClick.AddListener(eventManager.RestartScene);

            Button phasesButton = GameObject.Find("PhasesButton").GetComponent<Button>();
            phasesButton.onClick.AddListener(eventManager.LoadLevelSelectScene);

            Button mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
            mainMenuButton.onClick.AddListener(eventManager.LoadMainMenuScene);

            Button nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
            nextLevelButton.onClick.AddListener(eventManager.LoadNextLevelScene);

            //if (!eventManager.CheckLastLevel())
            //{
            //    Button nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
            //    nextLevelButton.onClick.AddListener(eventManager.LoadNextLevelScene);

            //}
        }

        internal void OnLosedGame()
        {
            _pausePage.SetActive(false);
            _guessingPage.SetActive(false);
            _winPage.SetActive(false);
            _bellAnimation.SetActive(true);
            Invoke(nameof(ShowGameOverPage), 2f);
        }

        internal void OnEarnedStars(int amountStars)
        {
            var winPageStars = _winPage.GetComponent<Animator>();

            winPageStars.SetInteger("NumberStars", amountStars);
        }

        #endregion


        #region COROUTINES

        protected IEnumerator TimerForOpenOrClose(float time, GameObject page, bool open)
        {
            yield return new WaitForSeconds(time);
            page.SetActive(open);
        }

        private IEnumerator WaitToCloseHintPage()
        {
            if (!_hintPage.activeSelf)
                yield return null;

            var animationHintPage = _hintPage.GetComponent<Animator>();

            animationHintPage.SetTrigger("Closing");

            yield return new WaitForSeconds(1f);
            _hintPage.SetActive(false);

        }

        #endregion
    }
}
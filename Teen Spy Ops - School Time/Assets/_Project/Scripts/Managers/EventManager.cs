using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using Objects;
using Controllers;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {
        protected GameManager _gameManager;
        protected SceneryManager _sceneryManager;
        protected UIManager _uiManager;
        protected LevelManager _levelManager;
        protected AudioManager _audioManager;
        protected AchievementManager _achievementManager;

        protected GuessController _guessController;
        protected HintController _hintController;
        protected Quest _questPlayer;
        protected Skills _skills;

        // -- System -- //
        public Action InitializedGame;
        public Action CountdownPerfomed;

        public Action PausedGame;
        public Action UnPausedGame;

        public Action WonGame;
        public Action LosedGame;

        public Action<int> EarnedStars;

        // -- Player Skills -- //
        public Action ActivedXRay;
        public Action UpgradeXRayVision;

        public Action ActivedFingerprint;
        public Action ActivedNightVision;

        // -- Items -- //
        public int AmountItems { get; private set; }
        public Action ItemCollected;

        public Action InstantiatedCollectables;

        public Action ChosenCorrect;
        public Action ChosenIncorrect;
        public Action DecreasedStars;

        // -- Hint -- //
        public Action<string, int> GotHint;

        
        protected virtual void Awake()
        {
            // -- Managers -- //
            _gameManager = FindObjectOfType<GameManager>();
            _uiManager = FindObjectOfType<UIManager>();
            _sceneryManager = FindObjectOfType<SceneryManager>();
            _levelManager = FindObjectOfType<LevelManager>();
            _audioManager = FindObjectOfType<AudioManager>();
            _achievementManager = FindObjectOfType<AchievementManager>();

            // -- Controllers -- //
            _guessController = FindObjectOfType<GuessController>();
            _hintController = FindObjectOfType<HintController>();

            // -- Player -- //
            _questPlayer = FindObjectOfType<Quest>();
            _skills = FindObjectOfType<Skills>();

        }

        protected virtual void Start()
        {
            // -- Events -- //
            InitializedGame += _gameManager.OnInitializedLevel;
            InitializedGame += _uiManager.OnInitializedLevel;
            InitializedGame += _skills.OnInitializedLevel;
            InitializedGame += _levelManager.OnInitializedLevel;
            InitializedGame += _sceneryManager.OnInitializedLevel;
            InitializedGame += OnInitialized;

            CountdownPerfomed += _levelManager.OnCountdownTimerLevel;
            CountdownPerfomed += _questPlayer.OnCountdownPerfomed;

            GotHint += _hintController.OnGotHint;
            GotHint += _uiManager.OnGotHint;

            PausedGame += _gameManager.OnPausedGame;
            PausedGame += _uiManager.OnPausedGame;

            UnPausedGame += _gameManager.OnUnPausedGame;
            UnPausedGame += _uiManager.OnUnPausedGame;

            LosedGame += _gameManager.OnLosedGame;
            LosedGame += _questPlayer.OnLosedGame;
            LosedGame += _uiManager.OnLosedGame;

            ActivedXRay += _gameManager.OnActivedXRay;
            ActivedXRay += _sceneryManager.OnActivedXRay;
            ActivedXRay += _skills.OnActivedXRay;
            ActivedXRay += _uiManager.OnActivedXRay;

            UpgradeXRayVision += _gameManager.OnUpgradeXRayVision;
            UpgradeXRayVision += _sceneryManager.OnUpgradeXRayVision;
            UpgradeXRayVision += _skills.OnUpgradeXRayVision;
            UpgradeXRayVision += _uiManager.OnUpgradeXRayVision;

            ActivedFingerprint += _gameManager.OnActivedFingerprint;
            ActivedFingerprint += _sceneryManager.OnActivedFingerprint;
            ActivedFingerprint += _skills.OnActivedFingerprint;

            ActivedNightVision += _gameManager.OnActivedNightVision;
            ActivedNightVision += _sceneryManager.OnActivedNightVision;
            ActivedNightVision += _skills.OnActivedNightVision;

            ChosenIncorrect += _guessController.OnChosenIncorrect;
            ChosenIncorrect += _questPlayer.OnChosenIncorrect;
            ChosenIncorrect += _uiManager.OnChosenIncorrect;

            ItemCollected += _levelManager.OnCollected;
            ItemCollected += _uiManager.OnCollected;

            EarnedStars += _levelManager.OnEarnedStars;
            EarnedStars += _uiManager.OnEarnedStars;

            WonGame += _gameManager.OnWonGame;
            WonGame += _uiManager.OnWonGame;
            WonGame += _levelManager.OnWonGame;


            _skills.CountdownXrayTimer += _uiManager.OnCountdownXrayTimer;

            _skills.CountdownFingerprintTimer += _uiManager.OnCountdownFingerprintTimer;

            _skills.CountdownNightVisionTimer += _uiManager.OnCountdownNightVisionTimer;

            _skills.FinishedTimerSkill += _gameManager.OnFinishedTimerSkill;
            _skills.FinishedTimerSkill += _sceneryManager.FinishedTimerSkill;
            _skills.FinishedTimerSkill += _uiManager.OnFinishedTimerSkill;

            _skills.CountdownXrayCooldown += _uiManager.OnCountdownCooldownXray;

            _skills.CountdownFingerprintCooldown += _uiManager.OnCountdownFingerprintCooldown;

            _skills.CountdownNightVisionCooldown += _uiManager.OnCountdownNightVisionCooldown;

            
            InitializedGame?.Invoke();
        }

        protected virtual void Update()
        {
            if (_levelManager.TimerLevel > 0)
                CountdownPerfomed?.Invoke();
            else
            {
                LosedGame?.Invoke();
                EarnedStars?.Invoke(_questPlayer.CurrentNumberStars);
            }

            _uiManager.ShowCountdownPerfomedText(_levelManager.TimerLevel);
            _uiManager.ShowAmoutItemsLeft(_levelManager.ItemsLeft);
        }

        internal void OnInitialized()
        {
            foreach (Collectable coll in _levelManager.ItemsCollectable)
            {
                coll.GotQuestion += _uiManager.OnGotQuestion;
                coll.CheckedItemOnList += _levelManager.OnCheckedItemOnList;

                ActivedXRay += coll.OnActivatedXray;
                ActivedNightVision += coll.OnActivedNightVision;
                //ActivedFingerprint += coll.OnActivedFingerprint;

                UpgradeXRayVision += coll.OnUpgradeXRayVision;
                _skills.FinishedTimerSkill += coll.OnFinishedTimerSkill;


            }
        }

        internal void OnGotHint()
        {
            if (_hintController.AmountHint <= 0)
                return;

            //_hintController.AmountHint--;
            
            if (PlayerPrefs.GetInt("AMOUNT_HINTS_USED") < 10)
                PlayerPrefs.SetInt("AMOUNT_HINTS_USED", PlayerPrefs.GetInt("AMOUNT_HINTS_USED")+1);

            _achievementManager.UnlockedHackerman?.Invoke();
            GotHint?.Invoke(_hintController.CurrentHint, _hintController.AmountHint);
        }
        internal void OnActivedXRay() => ActivedXRay?.Invoke();
        internal void OnUpgradeVision() => UpgradeXRayVision?.Invoke();
        internal void OnActivedNightVision() => ActivedNightVision?.Invoke();
        internal void OnActivedFingerprint() => ActivedFingerprint?.Invoke();
        public void OnChosenCorrect()
        {
            ItemCollected?.Invoke();
            if (_levelManager.ItemsCollectable.Count <= 0)
            {
                WonGame?.Invoke();

                if (_levelManager.TimerLevel <= 30)
                    _achievementManager.UnlockedInTime?.Invoke();

                EarnedStars?.Invoke(_questPlayer.CurrentNumberStars);

                if (SceneManager.GetActiveScene().name == "LEVEL7" && _questPlayer.CurrentNumberStars >= 5 && _hintController.AmountHint >= 2)
                    _achievementManager.UnlockedSoloAgent?.Invoke();
            }
        }

        public void OnChosenIncorrect()
        {
            ChosenIncorrect?.Invoke();
        }


        internal void OnPausedGame()
        {
            if (_gameManager.CurrentGameState == GameState.Running)
                PausedGame?.Invoke();
        }

        internal void OnUnPausedGame()
        {
            if (_gameManager.CurrentGameState == GameState.Paused)
                UnPausedGame?.Invoke();
        }

        internal void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        internal void LoadLevelSelectScene() => SceneManager.LoadScene("LEVELSELECT");
        internal void LoadMainMenuScene() => SceneManager.LoadScene("MAINMENU");
        internal void LoadNextLevelScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
using Manager;
using System;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {
        
        protected internal Action SkippedTutorialLine;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            CountdownPerfomed += _levelManager.OnCountdownTimerLevel;

            PausedGame += _gameManager.OnPausedGame;
            PausedGame += _uiManager.OnPausedGame;

            UnPausedGame += _gameManager.OnUnPausedGame;
            UnPausedGame += _uiManager.OnUnPausedGame;

            ItemCollected += _levelManager.OnCollected;
            ItemCollected += _uiManager.OnCollected;

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
        }

        protected override void Update()
        {
            if (_levelManager.TimerLevel > 0)
                CountdownPerfomed?.Invoke();

            _uiManager.ShowCountdownPerfomedText(_levelManager.TimerLevel);
            _uiManager.ShowAmoutItemsLeft(_levelManager.ItemsLeft);
        }


        private void ExecuteTutorial()
        {
            /*
             TINA_FALA()
             
             FOCAR_RAIO-X()

              TINA_FALA()

             */
        }
    }
}
using Manager;
using System;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {
        private int _currentTinaLineTutorial;
        
        private TutorialLevelManager _tutorialLevelManager;
        private TutorialUIManager _tutorialUIManager;

        protected internal Action SkippedTutorialLine;
        protected internal Action<string> CalledTinaLine;

        protected override void Awake()
        {
            base.Awake();
            CalledTinaLine += _tutorialUIManager.OnCalledTinaLine;
            _tutorialLevelManager = FindObjectOfType<TutorialLevelManager>();
        }

        protected override void Start()
        {
            base.Start();
            ExecuteTutorial();
        }

        protected override void Update()
        {
            if (_tutorialLevelManager.TimerLevel > 0)
                CountdownPerfomed?.Invoke();

            _uiManager.ShowCountdownPerfomedText(_tutorialLevelManager.TimerLevel);
            _uiManager.ShowAmoutItemsLeft(_tutorialLevelManager.ItemsLeft);
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
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
            base.Start();
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
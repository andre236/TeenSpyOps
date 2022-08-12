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
        internal Action<string> CalledTinaLine;

        protected override void Awake()
        {
            base.Awake();
            _tutorialUIManager = FindObjectOfType<TutorialUIManager>();
            _tutorialLevelManager = FindObjectOfType<TutorialLevelManager>();
        }

        protected override void Start()
        {
            base.Start();
            CalledTinaLine += _tutorialUIManager.OnCalledTinaLine;
            Invoke(nameof(ExecuteTutorial), 2f);
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
            CalledTinaLine?.Invoke("Teste");
            /*
             * Surge o bal�o de fala da Tina()
            
             * A tela � focada ao bot�o de raio-x e o restante se escurece ao fundo()

             * Surge o bal�o de fala da Tina()

             * Ainda em foco, o raio-x se ativa fazendo o c�rculo de vis�o surgir.
              
             * Nesse momento, o Em�lio(jogador) n�o poder� ainda coletar ou clicar em quaisquer objeto.
             */
        }

        
    }
}
using Manager;
using Statics;
using System;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {
        private int _currentTinaLineTutorial;

        private TutorialLevelManager _tutorialLevelManager;
        private TutorialUIManager _tutorialUIManager;

        protected internal Action SkippedTutorialLine;
        internal Action<bool> CalledTinaTutorialPage;
        internal Action<string[]> CalledTinaLine;

        protected override void Awake()
        {
            base.Awake();
            _tutorialUIManager = FindObjectOfType<TutorialUIManager>();
            _tutorialLevelManager = FindObjectOfType<TutorialLevelManager>();
        }

        protected override void Start()
        {
            base.Start();

            SkippedTutorialLine += _tutorialUIManager.OnSkippedTutorialLine;

            //CalledTinaTutorialPage += _tutorialUIManager.OnCalledTinaTutorialPage;

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
            CalledTinaLine?.Invoke(GeneralTexts.Instance.TinaSectionLinesTutorialsList[0].TinaLines);
            /*
             * Surge o balão de fala da Tina()
            
             * A tela é focada ao botão de raio-x e o restante se escurece ao fundo()

             * Surge o balão de fala da Tina()

             * Ainda em foco, o raio-x se ativa fazendo o círculo de visão surgir.
              
             * Nesse momento, o Emílio(jogador) não poderá ainda coletar ou clicar em quaisquer objeto.
             */
        }

        public void OnSkippedTutorialLine()
        {
            CalledTinaLine?.Invoke(GeneralTexts.Instance.TinaSectionLinesTutorialsList[0].TinaLines);
        }


    }
}
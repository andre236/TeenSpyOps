using Manager;
using Statics;
using System;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {
        private int _currentTinaLineTutorial;
        private int _currentSectionLine;

        private TutorialLevelManager _tutorialLevelManager;
        private TutorialUIManager _tutorialUIManager;

        internal Action<bool> CalledTinaTutorialPage;
        internal Action<string[]> CalledTinaLine;
        internal Action<float, float> FocusedObject;

        private TutorialStage _currentStage;

        public int CurrentSectionLine { get => _currentSectionLine; set => _currentSectionLine = value; }

        protected override void Awake()
        {
            base.Awake();
            _tutorialUIManager = FindObjectOfType<TutorialUIManager>();
            _tutorialLevelManager = FindObjectOfType<TutorialLevelManager>();
        }

        protected override void Start()
        {
            base.Start();

            //CalledTinaTutorialPage += _tutorialUIManager.OnCalledTinaTutorialPage;

            CalledTinaLine += _tutorialUIManager.OnCalledTinaLine;
            FocusedObject += _tutorialUIManager.OnFocusedObject;

            CurrentSectionLine = 0;
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
            CalledTinaLine?.Invoke(GeneralTexts.Instance.TinaSectionLinesTutorialsList[CurrentSectionLine].TinaLines);

            FocusedObject?.Invoke(240.7f, -188.2f);
            /*
             * Surge o balão de fala da Tina()
            
             * A tela é focada ao botão de raio-x e o restante se escurece ao fundo()

             * Surge o balão de fala da Tina()

             * Ainda em foco, o raio-x se ativa fazendo o círculo de visão surgir.
              
             * Nesse momento, o Emílio(jogador) não poderá ainda coletar ou clicar em quaisquer objeto.
             */
        }

        public void SkippedTutorialLine()
        {
            CalledTinaLine?.Invoke(GeneralTexts.Instance.TinaSectionLinesTutorialsList[CurrentSectionLine].TinaLines);
        }
    }
}
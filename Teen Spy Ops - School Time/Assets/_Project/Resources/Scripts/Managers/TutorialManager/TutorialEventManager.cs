using Manager;
using Statics;
using System;
using System.Collections;
using UnityEngine;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {
        private int _currentTinaLineTutorial;
        private int _currentSectionLine;
        private bool _canNextStep;

        private TutorialLevelManager _tutorialLevelManager;
        private TutorialUIManager _tutorialUIManager;

        internal Action<string[]> CalledTinaLine;
        internal Action<string> FocusedObject;
        internal Action CalledNextAction;
        [SerializeField] private TutorialStage _currentTutorialStage;
        [SerializeField] private TinaSectionLinesTutorialStage _currentTutorialSection;


        protected override void Awake()
        {
            base.Awake();
            _tutorialUIManager = FindObjectOfType<TutorialUIManager>();
            _tutorialLevelManager = FindObjectOfType<TutorialLevelManager>();
        }

        protected override void Start()
        {
            base.Start();


            Invoke(nameof(ExecuteTutorial), 2f);
        }

        protected override void Update()
        {
            if (_tutorialLevelManager.TimerLevel > 0)
                CountdownPerfomed?.Invoke();

            _uiManager.ShowCountdownPerfomedText(_tutorialLevelManager.TimerLevel);
            _uiManager.ShowAmountItemsLeft(_tutorialLevelManager.ItemsLeft);
        }

        internal void ExecuteTutorial()
        {
            _canNextStep = false;
            StartCoroutine(ExecutingInCube());
            //switch (_currentTutorialStage)
            //{
            //    case TutorialStage.ATO_1:
            //        _tutorialUIManager.OnCalledTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            //        _tutorialUIManager.OnFocusedObject("OnXRayButton");
            //        break;
            //    case TutorialStage.ATO_2:
            //        _tutorialUIManager.OnCalledTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            //        _tutorialUIManager.HighlightOneGameObject(GameObject.Find("XRayButton"));
                    
            //        break;
            //    case TutorialStage.ATO_3:
                    
            //        break;
            //    case TutorialStage.ATO_4:
            //        break;
            //    case TutorialStage.ATO_5:
            //        break;
            //    case TutorialStage.ATO_6:
            //        break;
            //    case TutorialStage.ATO_7:
            //        break;
            //    case TutorialStage.ATO_8:
            //        break;
            //    case TutorialStage.ATO_9:
            //        break;
            //    case TutorialStage.ATO_10:
            //        break;
            //    case TutorialStage.ATO_11:
            //        break;
            //    case TutorialStage.ATO_12:
            //        break;
            //    case TutorialStage.ATO_13:
            //        break;
            //    case TutorialStage.ATO_14:
            //        break;
            //    case TutorialStage.ATO_15:
            //        break;
            //}

        }

        internal IEnumerator ExecutingInCube()
        {
            _tutorialUIManager.OnCalledTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines, _canNextStep);
            yield return new WaitUntil(() => _canNextStep == true);
            _tutorialUIManager.OnFocusedObject("OnXRayButton");
            
            yield return new WaitUntil(() => _canNextStep == true);

        }

        internal IEnumerator SkipTutorialStage(float timer)
        {
            yield return new WaitForSeconds(timer);
            _canNextStep = true;
            _currentTutorialStage++;
            _currentTutorialSection++;
        }

        internal IEnumerator SkipTutorialStage(float timer, GameObject deactive)
        {
            yield return new WaitForSeconds(timer);
            deactive.SetActive(false);
            _currentTutorialStage++;
            ExecuteTutorial();
        }

        internal void CanGoNextStage()
        {
            _canNextStep = true;
        }

        public void SkipTutorialLine()
        {
            _tutorialUIManager.OnCalledTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialStage].TinaLines, _canNextStep);
        }
    }
}
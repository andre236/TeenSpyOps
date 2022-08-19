using Manager;
using Objects;
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
        private bool _onGuessingPage;
        private bool _onCollectedFirstObject;
        private bool _onFirstFailed;

        private TutorialLevelManager _tutorialLevelManager;
        private TutorialUIManager _tutorialUIManager;

        internal Action<string[]> CalledTinaLine;
        internal Action<string> FocusedObject;
        internal Action CalledNextAction;

        [SerializeField] private TutorialStage _currentTutorialStage;

        [SerializeField] private TinaSectionLinesTutorialStage _currentTutorialSection;

        public bool OnGuessingPage { get => _onGuessingPage; set => _onGuessingPage = value; }
        public bool OnFirstFailed { get => _onFirstFailed; set => _onFirstFailed = value; }
        public bool OnCollectedFirstObject { get => _onCollectedFirstObject; set => _onCollectedFirstObject = value; }

        protected override void Awake()
        {
            base.Awake();
            _tutorialUIManager = FindObjectOfType<TutorialUIManager>();
            _tutorialLevelManager = FindObjectOfType<TutorialLevelManager>();
        }

        protected override void Start()
        {
            base.Start();

            _canNextStep = false;
            StartCoroutine(ExecutingInCube());
        }

        protected override void Update()
        {
            if (_tutorialLevelManager.TimerLevel > 0)
                CountdownPerfomed?.Invoke();

            _uiManager.ShowCountdownPerfomedText(_tutorialLevelManager.TimerLevel);
            _uiManager.ShowAmountItemsLeft(_tutorialLevelManager.ItemsLeft);
        }

        internal override void OnInitialized()
        {
            foreach (Collectable coll in _levelManager.ItemsCollectable)
            {
                ActivedXRay += coll.OnActivatedXray;
                ActivedNightVision += coll.OnActivedNightVision;

                UpgradeXRayVision += coll.OnUpgradeXRayVision;
                _skills.FinishedTimerSkill += coll.OnFinishedTimerSkill;
            }

        }

        internal void AllowClickingSchoolObject()
        {
            foreach (Collectable coll in _levelManager.ItemsCollectable)
            {
                coll.GotQuestion += _uiManager.OnGotQuestion;
                coll.CheckedItemOnList += _levelManager.OnCheckedItemOnList;

            }
        }

        internal IEnumerator ExecutingInCube()
        {
            yield return new WaitForSeconds(2f);
            // ATO 1
            _currentTutorialSection = 0;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);

            _tutorialUIManager.FocusedObject("OnXRayButton");

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);

            // ATO 2
            _currentTutorialSection = (TinaSectionLinesTutorialStage)1;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            _canNextStep = false;
            yield return new WaitUntil(() => _canNextStep == true);

            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("XRayButton"));
            _tutorialUIManager.RayButtonReceiveSkill(); // Aqui ta dando true no nextStage apenas no click
            _canNextStep = false;
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);


            // ATO 3
            _tutorialUIManager.FocusedObject("OnXRayBar");
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)2;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            _canNextStep = false;

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);

            _tutorialUIManager.SetOnOffFocus(false);
            AllowClickingSchoolObject();

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => OnGuessingPage == true);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)3;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);
            _tutorialUIManager.AllowToGuessObject();
            StartCoroutine(FirstFailed());
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => OnCollectedFirstObject == true);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)5;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => _canNextStep == true);

        }

        internal IEnumerator SkipTutorialStage(float timer)
        {
            yield return new WaitForSeconds(timer);
            _canNextStep = true;

        }

        internal IEnumerator FirstFailed()
        {
            yield return new WaitUntil(() => OnFirstFailed);
            yield return new WaitForSeconds(1f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)4;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
        }

        internal void CanGoNextStage() => _canNextStep = true;

        internal void ExplainGuessingPage()
        {
            OnGuessingPage = true;
        }

        internal void CanGoNextTinaSectionLine()
        {
            _currentTutorialStage++;
        }

        public void SkipTutorialLine()
        {
            Debug.Log("A section está em: " + (int)_currentTutorialStage);
            _tutorialUIManager.NextTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
        }

        public void CloseTutorialLine()
        {
            _tutorialUIManager.CloseTinaPageTutorial();
        }
    }
}
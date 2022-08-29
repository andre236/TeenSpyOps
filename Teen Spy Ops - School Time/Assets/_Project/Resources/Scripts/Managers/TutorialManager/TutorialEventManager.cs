using Manager;
using System;
using System.Collections;
using UnityEngine;
using Objects;
using Statics;
using Player;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {
        private int _currentTinaLineTutorial;
        private int _currentSectionLine;
        private int _amountSchoolObjectsCollected;

        private bool _canNextStep;
        private bool _onGuessingPage;
        private bool _onCollectedFirstObject;
        private bool _onFirstFailed;
        private bool _onUsedFingerprintFirstTime;
        private bool _onUsedNightVisionFirstTime;
        private bool _onNeededHint;
        [SerializeField] private bool _isTinaExplaining;

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
        public int AmountSchoolObjectsCollected { get => _amountSchoolObjectsCollected; set => _amountSchoolObjectsCollected = value; }
        public bool OnUsedFingerprintFirstTime { get => _onUsedFingerprintFirstTime; set => _onUsedFingerprintFirstTime = value; }
        public bool OnNeededHint { get => _onNeededHint; internal set => _onNeededHint = value; }

        public bool IsTinaExplaining { get => _isTinaExplaining; set => _isTinaExplaining = value; }
        public bool OnUsedNightVisionFirstTime { get => _onUsedNightVisionFirstTime; set => _onUsedNightVisionFirstTime = value; }

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

        internal override void OnGotHint()
        {
            if (_hintController.AmountHint <= 0)
                return;

            _hintController.AmountHint--;

            GotHint?.Invoke(_hintController.CurrentHint, _hintController.AmountHint);
        }

        internal void AllowClickingSchoolObject()
        {
            foreach (Collectable coll in _levelManager.ItemsCollectable)
            {
                coll.GotQuestion += _uiManager.OnGotQuestion;
                coll.CheckedItemOnList += _levelManager.OnCheckedItemOnList;

            }
        }


        internal void CanGoNextStep()
        {
            _canNextStep = true;
        }

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
            _tutorialUIManager.NextTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);

        }

        public void CloseTutorialLine()
        {
            _tutorialUIManager.CloseTinaPageTutorial();
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
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(2f);

            // ATO 2
            _currentTutorialSection = (TinaSectionLinesTutorialStage)1;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            _canNextStep = false;
            yield return new WaitUntil(() => _canNextStep == true);

            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("XRayButton"), false);
            _tutorialUIManager.RayButtonReceiveSkill(); // Aqui ta dando true no nextStage apenas no click
            _canNextStep = false;
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);


            // ATO 3
            _tutorialUIManager.FocusedObject("OnXRayBar");
            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("XRayButton"), true);

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)2;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            _canNextStep = false;

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);

            _tutorialUIManager.SetOnOffFocus(false);
            AllowClickingSchoolObject();

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => _isTinaExplaining == false);
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
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)5;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => _canNextStep == true);
            _tutorialUIManager.ShowHintButton();
            _tutorialUIManager.AllowRequestHint();
            yield return new WaitUntil(() => AmountSchoolObjectsCollected >= 2);
            StartCoroutine(NeededHintShowMessage());
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);

            _skills.Reset = true;
            _tutorialUIManager.TurnOffFocus();
            _tutorialUIManager.RemoveXRaySkill();

            yield return new WaitUntil(() => OnNeededHint == false);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)6;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(1f);
            _skills.Reset = false;

            // -- Foco no botão de fingerprint

            _tutorialUIManager.FocusedObject("OnFingerprint");

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)7;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);
            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("FingerprintButton"), false);
            _tutorialUIManager.FingerprintButtonReceiveSkill();

            yield return new WaitUntil(() => OnUsedFingerprintFirstTime == true);
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);

            _tutorialUIManager.RemoveFingerprintSkill();
            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("FingerprintButton"), true);


            yield return new WaitUntil(() => _isTinaExplaining == false);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)8;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => IsTinaExplaining == false);
            yield return new WaitForSeconds(2f);
            _tutorialUIManager.FocusedObject("OnNightVision");

            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)9;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            yield return new WaitForSeconds(2f);
            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("NightVisionButton"), false);
            _tutorialUIManager.NightVisionButtonReceiveSkill();
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitUntil(() => OnUsedNightVisionFirstTime == true);
            yield return new WaitForSeconds(2f);
            _tutorialUIManager.HighlightOneGameObject(GameObject.Find("NightVisionButton"), true);
            yield return new WaitUntil(() => _isTinaExplaining == false);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)10;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
            yield return new WaitUntil(() => AmountSchoolObjectsCollected >= 3);
            yield return new WaitUntil(() => _canNextStep == true);
            yield return new WaitForSeconds(2f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)12;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);

        }

        internal void UsedNightVisionFirstTime()
        {
            OnUsedNightVisionFirstTime = true;
        }

        internal IEnumerator OnWaitingUsedFingerprintFirstTime()
        {
            yield return new WaitForSeconds(15f);
            OnUsedFingerprintFirstTime = true;
        }

        internal IEnumerator SkipTutorialStage(float timer)
        {
            yield return new WaitForSeconds(timer);
            _canNextStep = true;

        }

        internal IEnumerator NeededHintShowMessage()
        {
            yield return new WaitUntil(() => OnNeededHint == true);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => _isTinaExplaining == false);

            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[13].TinaLines);
            

        }

        internal IEnumerator FirstFailed()
        {
            yield return new WaitUntil(() => OnFirstFailed);
            yield return new WaitForSeconds(1f);
            _currentTutorialSection = (TinaSectionLinesTutorialStage)4;
            _tutorialUIManager.CallTinaLine(GeneralTexts.Instance.TinaSectionLinesTutorialsList[(int)_currentTutorialSection].TinaLines);
        }


    }
}
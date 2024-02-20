using System;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using System.Collections;

namespace Tutorial
{
    public class TutorialUIManager : UIManager
    {
        [SerializeField] private int _numberLines;
        [SerializeField] private int _currentLine;
        private Text _tinaText;

        private GameObject _tinaPageTutorial;
        private GameObject _highlightObject;
        private GameObject _canvasForFocus;


        private Transform _previousGameObjectParent;

        protected override void Awake()
        {
            base.Awake();
            _tinaPageTutorial = GameObject.Find("TinaPageTutorial");
            _tinaText = GameObject.Find("TinaText").GetComponent<Text>();
            //_canvasForFocus = GameObject.Find("CanvasForFocus").GetComponent<Canvas>();
            _canvasForFocus = GameObject.Find("CanvasForFocus");

            _highlightObject = GameObject.Find("HighlightObject");
        }

        protected override void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);
            _closeHintButton.onClick.AddListener(CloseHintPage);

            // For late, reference skill buttons.
            _hintButton.gameObject.SetActive(false);
            _tinaPageTutorial.SetActive(false);
            _canvasForFocus.gameObject.SetActive(false);
        }

        internal void CallTinaLine(string[] lines)
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();
            


            _numberLines = lines.Length;

            _currentLine = 0;
            _tinaText.text = lines[_currentLine];

            _tinaPageTutorial.SetActive(true);
            eventManagerTutorial.IsTinaExplaining = _tinaPageTutorial.activeSelf;

            if (lines.Length > 1)
                _tinaPageTutorial.GetComponent<Button>().onClick.AddListener(FindObjectOfType<TutorialEventManager>().SkipTutorialLine);
            else
                _tinaPageTutorial.GetComponent<Button>().onClick.AddListener(FindObjectOfType<TutorialEventManager>().CloseTutorialLine);

        }

        internal void CloseTinaPageTutorial()
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            _tinaPageTutorial.GetComponent<Animator>().SetTrigger("Closing");
            StartCoroutine(DelayToDeactiveGameObject(_tinaPageTutorial, 2f));
            _tinaPageTutorial.GetComponent<Button>().onClick.RemoveAllListeners();
            eventManagerTutorial.CanGoNextStep();
        }

        internal void NextTinaLine(string[] lines)
        {
            if (!_tinaPageTutorial.activeSelf)
                return;

            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();



            if (_currentLine >= lines.Length)
            {
                _tinaPageTutorial.GetComponent<Animator>().SetTrigger("Closing");
                StartCoroutine(DelayToDeactiveGameObject(_tinaPageTutorial, 2f));
                _tinaPageTutorial.GetComponent<Button>().onClick.RemoveAllListeners();
                eventManagerTutorial.CanGoNextStep();
            }

            if (_currentLine <= lines.Length - 1)
                _tinaText.text = lines[_currentLine];

            _currentLine++;

        }

        internal void FocusedObject(string triggerAnimation)
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            _canvasForFocus.gameObject.SetActive(true);
            _canvasForFocus.GetComponent<Animator>().SetTrigger(triggerAnimation);
            eventManagerTutorial.CanGoNextStep();
        }

        internal void TurnOffFocus()
        {
            _canvasForFocus.gameObject.SetActive(false);
        }

        internal void HighlightOneGameObject(GameObject gameObject, bool backGameObject)
        {
            _previousGameObjectParent = GameObject.Find("LayoutButtons").transform;

            gameObject.transform.SetParent(_highlightObject.transform);

            if (backGameObject)
            {
                gameObject.transform.SetParent(_previousGameObjectParent);
                if (gameObject.name == "XRayButton")
                    gameObject.transform.SetSiblingIndex(0);
                else if (gameObject.name == "FingerprintButton")
                    gameObject.transform.SetSiblingIndex(1);
                else
                    gameObject.transform.SetSiblingIndex(2);
            }

        }


        internal void RayButtonReceiveSkill()
        {
            _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedXRay);
            _xRayButton.onClick.AddListener(PlayHudAnimation);
        }

        internal void FingerprintButtonReceiveSkill()
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            _fingerprintButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedFingerprint);
            _fingerprintButton.onClick.AddListener(PlayHudAnimation);
            _fingerprintButton.onClick.AddListener(WaitingForFingerprint);

        }

        internal void WaitingForFingerprint()
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            StartCoroutine(eventManagerTutorial.OnWaitingUsedFingerprintFirstTime());
        }

        internal void NightVisionButtonReceiveSkill()
        {
            _nightVisionButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedNightVision);
            _nightVisionButton.onClick.AddListener(PlayHudAnimation);
            _nightVisionButton.onClick.AddListener(FindObjectOfType<TutorialEventManager>().UsedNightVisionFirstTime);

        }

        internal void RemoveXRaySkill()
        {
            _xRayButton.onClick.RemoveAllListeners();
            _xRayButton.interactable = false;
            _resetNightVisionButton.onClick.RemoveAllListeners();

            //if (_xRayButton.onClick.GetPersistentEventCount() > 0)
            //{
            //    Debug.Log("Ainda há Eventos aqui!");
            //}
            //else
            //{
            //    Debug.Log("Não há mais eventos!");

            //}
        }

        internal void RemoveFingerprintSkill()
        {
            _fingerprintButton.onClick.RemoveAllListeners();
        }

        internal override void OnGotQuestion(string nameObject, Sprite itemSprite)
        {
            base.OnGotQuestion(nameObject, itemSprite);
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            if (eventManagerTutorial.OnGuessingPage)
                return;


            if (!eventManagerTutorial.OnGuessingPage)
                AllowToGuessObject();


            eventManagerTutorial.ExplainGuessingPage();

            for (int i = 0; i < 3; i++)
            {
                _answersButton[i].GetComponent<Animator>().enabled = false;
                _answersButton[i].interactable = false;
            }


        }

        internal void AllowToGuessObject()
        {
            _answersButton[0].onClick.RemoveAllListeners();
            _answersButton[1].onClick.RemoveAllListeners();
            _answersButton[2].onClick.RemoveAllListeners();

            _answersButton[0].onClick.AddListener(FindObjectOfType<EventManager>().OnChosenCorrect);
            _answersButton[1].onClick.AddListener(FindObjectOfType<EventManager>().OnChosenIncorrect);
            _answersButton[2].onClick.AddListener(FindObjectOfType<EventManager>().OnChosenIncorrect);

            for (int i = 0; i < 3; i++)
            {
                _answersButton[i].interactable = true;
                _answersButton[i].GetComponent<Animator>().enabled = true;
            }
        }

        internal void AllowRequestHint()
        {
            _hintButton.onClick.AddListener(FindObjectOfType<TutorialEventManager>().OnGotHint);
            _hintButton.onClick.AddListener(ShowNeededHintMessage);

        }

        internal void ShowNeededHintMessage()
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();
            eventManagerTutorial.OnNeededHint = true;
        }

        internal override void OnChosenIncorrect()
        {
            base.OnChosenIncorrect();
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            if (_errorIcons[^1].gameObject.activeSelf)
                eventManagerTutorial.OnFirstFailed = true;

        }

        internal void ShowHintButton()
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            eventManagerTutorial.OnNeededHint = false;
            _hintButton.gameObject.SetActive(true);
        }



        internal override void OnCollected()
        {
            base.OnCollected();
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            eventManagerTutorial.OnCollectedFirstObject = true;
            eventManagerTutorial.AmountSchoolObjectsCollected++;
        }

        protected override void PlayHudAnimation()
        {
            base.PlayHudAnimation();

            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            _canvasForFocus.gameObject.SetActive(false);
            StartCoroutine(eventManagerTutorial.SkipTutorialStage(0f));

        }

        internal void SetOnOffFocus(bool active)
        {
            _canvasForFocus.gameObject.SetActive(active);
        }

        IEnumerator DelayToDeactiveGameObject(GameObject gameObject, float timer)
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();
            yield return new WaitForSeconds(timer);
            gameObject.SetActive(false);
            if (gameObject == _tinaPageTutorial)
            {
                eventManagerTutorial.IsTinaExplaining = false;
                eventManagerTutorial.OnNeededHint = false;
            }
        }


    }
}
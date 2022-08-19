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
        private Canvas _canvasForFocus;

        protected override void Awake()
        {
            base.Awake();
            _tinaPageTutorial = GameObject.Find("TinaPageTutorial");
            _tinaText = GameObject.Find("TinaText").GetComponent<Text>();
            _canvasForFocus = GameObject.Find("CanvasForFocus").GetComponent<Canvas>();
        }

        protected override void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);

            // For late, reference skill buttons.
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
            eventManagerTutorial.CanGoNextStage();
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
                eventManagerTutorial.CanGoNextStage();
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
            eventManagerTutorial.CanGoNextStage();
        }

        internal void HighlightOneGameObject(GameObject gameObject)
        {

            var highlightObject = GameObject.Find("HighlightObject");

            gameObject.transform.SetParent(highlightObject.transform);
        }

        internal void RayButtonReceiveSkill()
        {
            var highlightObject = GameObject.Find("HighlightObject");

            _xRayButton.onClick.AddListener(FindObjectOfType<EventManager>().OnActivedXRay);
            _xRayButton.onClick.AddListener(PlayHudAnimation);

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

        internal override void OnChosenIncorrect()
        {
            base.OnChosenIncorrect();
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            if (_errorIcons[^1].gameObject.activeSelf)
                eventManagerTutorial.OnFirstFailed = true;

        }

        internal override void OnCollected()
        {
            base.OnCollected();
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            eventManagerTutorial.OnCollectedFirstObject = true;
        }

        protected override void PlayHudAnimation()
        {
            base.PlayHudAnimation();

            var canvasForFocus = GameObject.Find("CanvasForFocus");
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            canvasForFocus.SetActive(false);
            StartCoroutine(eventManagerTutorial.SkipTutorialStage(0f));

        }

        internal void SetOnOffFocus(bool active)
        {
            _canvasForFocus.gameObject.SetActive(active);
        }

        IEnumerator DelayToDeactiveGameObject(GameObject gameObject, float timer)
        {
            yield return new WaitForSeconds(timer);
            gameObject.SetActive(false);
        }


    }
}
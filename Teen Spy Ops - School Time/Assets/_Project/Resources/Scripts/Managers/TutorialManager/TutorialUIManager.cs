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
            _tinaPageTutorial.GetComponent<Button>().onClick.AddListener(FindObjectOfType<TutorialEventManager>().SkipTutorialLine);
            _tinaPageTutorial.SetActive(false);
            _canvasForFocus.gameObject.SetActive(false);
        }

        internal void OnCalledTinaLine(string[] lines, bool canNextStep)
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            _numberLines = lines.Length;

            if (_currentLine < lines.Length - 1)
                _tinaText.text = lines[_currentLine];
            else
                _tinaText.text = lines[^1];
            
            if (_tinaPageTutorial.activeSelf)
            {
                if (_currentLine >= _numberLines)
                {
                    _tinaPageTutorial.GetComponent<Animator>().SetTrigger("Closing");
                    StartCoroutine(DelayToDeactiveGameObject(_tinaPageTutorial, 2f, canNextStep));
                    eventManagerTutorial.CanGoNextStage();
                }
                else
                    _currentLine++;
            }
            else
            {
                _currentLine = 0;
                _tinaPageTutorial.SetActive(true);
                canNextStep = false;
            }

        }

        internal void OnFocusedObject(string triggerAnimation)
        {
            var eventManagerTutorial = FindObjectOfType<TutorialEventManager>();

            _canvasForFocus.gameObject.SetActive(true);
            _canvasForFocus.GetComponent<Animator>().SetTrigger(triggerAnimation);
            StartCoroutine(eventManagerTutorial.SkipTutorialStage(2f));
        }

        internal void HighlightOneGameObject(GameObject gameObject)
        {

            var highlightObject = GameObject.Find("HighlightObject");

            gameObject.transform.SetParent(highlightObject.transform);
        }

        IEnumerator DelayToDeactiveGameObject(GameObject gameObject, float timer, bool canNextStep)
        {
            yield return new WaitForSeconds(timer);
            canNextStep = true;
            gameObject.SetActive(false);
        }
    }
}
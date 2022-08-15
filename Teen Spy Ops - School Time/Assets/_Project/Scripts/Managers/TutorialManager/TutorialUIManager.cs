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
            _tinaPageTutorial.GetComponent<Button>().onClick.AddListener(FindObjectOfType<TutorialEventManager>().SkippedTutorialLine);
            _tinaPageTutorial.SetActive(false);
            _canvasForFocus.gameObject.SetActive(false);
        }

        internal void OnCalledTinaLine(string[] lines)
        {
            var tutorialEventManager = FindObjectOfType<TutorialEventManager>();

            _numberLines = lines.Length;

            if (_currentLine < lines.Length - 1)
                _tinaText.text = lines[_currentLine];
            else
                _tinaText.text = lines[^1];
            
            if (_tinaPageTutorial.activeSelf)
            {
                if (_currentLine >= _numberLines)
                {
                    tutorialEventManager.CurrentSectionLine++;
                    _tinaPageTutorial.GetComponent<Animator>().SetTrigger("Closing");
                    StartCoroutine(DelayToDeactiveGameObject(_tinaPageTutorial, 1f));
                }
                else
                    _currentLine++;
            }
            else
            {
                _currentLine = 0;
                _tinaPageTutorial.SetActive(true);
            }


        }

        internal void OnFocusedObject(float xPosition, float yPosition)
        {
            if (!_canvasForFocus.gameObject.activeSelf)
                return;
            
            GameObject circle = _canvasForFocus.gameObject.transform.Find("Circle").gameObject;

            circle.transform.position = new Vector3(xPosition, yPosition);
            

        }

        IEnumerator DelayToDeactiveGameObject(GameObject gameObject, float timer)
        {
            yield return new WaitForSeconds(timer);
            gameObject.SetActive(false);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using System.Collections;

namespace Tutorial
{
    public class TutorialUIManager : UIManager
    {
        private int _numberLines;
        private int _currentLine;
        private Text _tinaText;

        private GameObject _tinaPageTutorial;

        protected override void Awake()
        {
            base.Awake();
            _tinaPageTutorial = GameObject.Find("TinaPageTutorial");
            _tinaText = GameObject.Find("TinaText").GetComponent<Text>();
        }

        protected override void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);

            // For late, reference skill buttons.
            _tinaPageTutorial.SetActive(false);

        }



        internal void OnCalledTinaLine(string[] lines)
        {
            _numberLines = lines.Length;

            if (_currentLine < lines.Length - 1)
                _tinaText.text = lines[_currentLine];
            else
                _tinaText.text = lines[^1];
            
            Debug.Log("Fuichamado");
            
            if (_tinaPageTutorial.activeSelf)
            {
                if (_currentLine >= _numberLines)
                {
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

        internal void OnSkippedTutorialLine()
        {

        }

        internal void OnCalledTinaTutorialPage(bool active)
        {
            _tinaPageTutorial.SetActive(active);
        }

        IEnumerator DelayToDeactiveGameObject(GameObject gameObject, float timer)
        {
            yield return new WaitForSeconds(timer);
            gameObject.SetActive(false);
        }
    }
}
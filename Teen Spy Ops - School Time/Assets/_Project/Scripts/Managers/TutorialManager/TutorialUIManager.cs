using System;
using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace Tutorial
{
    public class TutorialUIManager : UIManager
    {
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

        internal void OnCalledTinaLine(string line)
        {
            _tinaText.text = line;
            _tinaPageTutorial.SetActive(true);
        }



    }
}
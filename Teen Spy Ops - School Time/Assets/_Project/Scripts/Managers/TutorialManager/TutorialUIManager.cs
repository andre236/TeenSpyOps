using UnityEngine;
using Manager;

namespace Tutorial
{
    public class TutorialUIManager : UIManager
    {
        private GameObject _tinaPageTutorial;

        protected override void Awake()
        {
            base.Awake();
            _tinaPageTutorial = GameObject.Find("TinaPageTutorial");
        }

        protected override void Start()
        {
            _pauseMenuButton.onClick.AddListener(FindObjectOfType<EventManager>().OnPausedGame);

            // For late, reference skill buttons.
            _tinaPageTutorial.SetActive(false);

        }

        internal void OnSkippedTutorialLine()
        {

        }

    }
}
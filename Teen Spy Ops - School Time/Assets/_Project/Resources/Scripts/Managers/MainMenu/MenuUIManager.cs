using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class MenuUIManager : UIManager
    {
        private GameObject _pedagogicPage;
        private GameObject _transition;

        private Button _playButton;
        private Button _pedagogicButton;
        private Button _closePedagogicButton;

        protected override void Awake()
        {
            _pedagogicPage = GameObject.Find("PedagogicPage");
            _transition = GameObject.Find("Transition");

            _playButton = GameObject.Find("PlayButton").GetComponent<Button>();
            _pedagogicButton = GameObject.Find("PedagogicButton").GetComponent<Button>();
            _closePedagogicButton = GameObject.Find("ClosePedagogicButton").GetComponent<Button>();
        }

        protected override void Start()
        {
            _pedagogicPage.SetActive(false);

            _playButton.onClick.AddListener(FindObjectOfType<EventMenuManager>().LoadNextLevelScene);
            _pedagogicButton.onClick.AddListener(OpenPedagogicPage);
            _closePedagogicButton.onClick.AddListener(ClosePedagogicPage);
        }

        internal override void OnLoadedNextScene()
        {
            _transition.GetComponent<Animator>().SetTrigger("FadeIn");

        }

        internal void OpenPedagogicPage()
        {
            _pedagogicPage.GetComponent<Animator>().SetTrigger("Opening");
            StartCoroutine(TimerForOpenOrClose(0.2f, _pedagogicPage, true));
        }
        
        internal void ClosePedagogicPage()
        {
            _pedagogicPage.GetComponent<Animator>().SetTrigger("Closing");
            StartCoroutine(TimerForOpenOrClose(1f, _pedagogicPage, false));
        }

    }
}
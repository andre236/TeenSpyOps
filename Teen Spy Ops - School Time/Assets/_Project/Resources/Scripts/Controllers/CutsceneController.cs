using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Controllers
{
    public class CutsceneController : MonoBehaviour
    {
        private bool _canSkipVideo = true;
        [SerializeField] private bool _needSkip = false;
        [SerializeField] private bool _isFinishedVideo = false;

        [SerializeField] private int _currentVideoClipIndex = 0;
        [SerializeField] private int _currentCutsceneLineIndex = 0;
        [SerializeField] private int _currentSectionCutsceneLinesIndex = 0;
        [Space]

        [SerializeField] private int _currentStep = 0;
        [Space]
        [SerializeField] private int _currentFrameVideoPlayer;
        [SerializeField] private int _totalFramesVideoPlayer;
        [Space]
        [SerializeField] private float _delayToSkipVideo;


        private Image _arrowIndicatingCanJump;
        private TextMeshProUGUI _currentCutsceneLineText;
        private Button _invokeNextStepInvisible;
        private Button _skipCutsceneLineButton;

        [SerializeField] private VideoPlayer[] _allVideoClips;
        [SerializeField] private VideoPlayer _currentVideoClip;
        [SerializeField] private UnityEvent[] _allStepsCutscene;

        private Animator _transitionAnimator;
        private Animator _dialogBoxAnimator;

        [Serializable]
        private class SectionsCutsceneLines
        {
            public string NameArray;
            public string[] CutsceneLines;
        }

        [SerializeField] private List<SectionsCutsceneLines> _sectionCutsceneLinesList = new List<SectionsCutsceneLines>();

        private void Awake()
        {
            _allVideoClips = GameObject.Find("Frameworks").GetComponentsInChildren<VideoPlayer>();
            // Assets/_Project/Resources/StreamingAssets/Videos/Cutcenes/CutsceneInicial
            //GetAllVideosInnitialCutscene();
            // https://sistemashomologacao.suafaculdade.com.br/Jogos/unity/TeenSpyOps1/
            //_allVideoClips[0].url = System.IO.Path.Combine(Application.streamingAssetsPath, "INICIAL_0.mp4");


            _arrowIndicatingCanJump = GameObject.Find("ArrowIndicateCanJump").GetComponent<Image>();

            _transitionAnimator = GameObject.Find("TransitionCutscene").GetComponent<Animator>();
            _dialogBoxAnimator = GameObject.Find("BlackDialogBox").GetComponent<Animator>();

            _currentCutsceneLineText = GameObject.Find("CurrentCutsceneLineText").GetComponent<TextMeshProUGUI>();

            _skipCutsceneLineButton = GameObject.Find("SkipToNextLineOrFramkeworkButton").GetComponent<Button>();
            _invokeNextStepInvisible = GameObject.Find("InvokeNextStepInvisible").GetComponent<Button>();
        }

        private void FixedUpdate()
        {
            RefreshFrames();
        }


        private void Start()
        {
            InnitialStep();
        }

        public void InvokeNextStep(VideoPlayer video)
        {
            _allStepsCutscene[_currentStep]?.Invoke();

            if (_currentStep < _allStepsCutscene.Length - 1)
                _currentStep++;

        }

        public void InvokeNextStep()
        {
            if (!_canSkipVideo)
                return;

            _allStepsCutscene[_currentStep]?.Invoke();

            if (_currentStep < _allStepsCutscene.Length - 1)
                _currentStep++;

            _isFinishedVideo = false;
            _canSkipVideo = false;
            StartCoroutine(CountdownDelaySkipVideo());

        }

        public void SetNeedSkip(bool need)
        {
            _needSkip = need;
        }

        public void InvokeLastStep()
        {
            if (_currentStep == 26)
                return;

            _currentStep = 26;
            _allStepsCutscene[_currentStep]?.Invoke();

        }

        public void PlayCurrentVideo()
        {
            _currentVideoClip.Play();
        }

        public void PauseCurrentVideo()
        {
            _currentVideoClip.Pause();

        }

        public void PlayVideoByIndex(int currentVideoIndex)
        {
            _allVideoClips[currentVideoIndex].gameObject.SetActive(true);

            _currentVideoClip = _allVideoClips[currentVideoIndex];

            _currentVideoClip.Play();

            for (int indexVideo = 0; indexVideo < currentVideoIndex; indexVideo++)
            {
                if (indexVideo != currentVideoIndex)
                {
                    _allVideoClips[indexVideo].Pause();
                    _allVideoClips[indexVideo].gameObject.SetActive(false);

                }
            }



        }

        public void GoNextStepInTheEnd()
        {
            StartCoroutine(WaitingFinishVideoPlayer());
        }

        public void CanInvokeNextStep(float seconds)
        {
            StartCoroutine(WaitingForJump(seconds));

        }

        private void RefreshFrames()
        {
            _currentFrameVideoPlayer = (int)_currentVideoClip.frame;
            _totalFramesVideoPlayer = (int)(_currentVideoClip.frameCount);

            if (Input.GetKeyDown(KeyCode.P))
                InvokeLastStep();

            NextStep();
        }

        public void NextStep()
        {
            _currentVideoClip.loopPointReached += ChangeBoolFinished;

            if (_isFinishedVideo)
                InvokeNextStep();
        }

        public void ChangeBoolFinished(VideoPlayer video)
        {

            _isFinishedVideo = true;
            Debug.Log("O " + _currentVideoClip.name + " Finalizou!");
        }

        // h ttps://sistemashomologacao.suafaculdade.com.br/Jogos/unity/TeenSpyOps1/Videos/CutsceneInicial
        // h ttps://sistemashomologacao.suafaculdade.com.br/Jogos/unity/TeenSpyOps1/Videos/CutsceneInicial/INICIAL_1.mp4

        public void LoadLevelSelectScene()
        {
            LoadNextScene();
        }

        private void InnitialStep()
        {
            _arrowIndicatingCanJump.gameObject.SetActive(false);

            _transitionAnimator.gameObject.SetActive(false);

            _currentVideoClip = _allVideoClips[_currentVideoClipIndex];
            StartCoroutine(CountdownDelaySkipVideo());
            InvokeNextStep();
        }



        private IEnumerator WaitingForJump(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            _arrowIndicatingCanJump.gameObject.SetActive(true);
            _invokeNextStepInvisible.gameObject.SetActive(true);
        }

        private IEnumerator WaitingFinishVideoPlayer()
        {
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => _currentFrameVideoPlayer >= _totalFramesVideoPlayer - 10);
            InvokeNextStep();
        }

        public void ShowCutsceneLine()
        {
            var blackDialogBox = GameObject.Find("BlackDialogBox").GetComponent<Animator>();
            var amountLines = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines.Length - 1;


            blackDialogBox.SetBool("CanFadeIn", true);

            _currentCutsceneLineText.text = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines[_currentCutsceneLineIndex];


        }

        public void ShowNextLine()
        {
            var amountLines = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines.Length - 1;

            _currentCutsceneLineText.text = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines[_currentCutsceneLineIndex];

            if (_currentCutsceneLineIndex >= amountLines)
            {

                _currentSectionCutsceneLinesIndex++;
                _currentCutsceneLineIndex = 0;


            }
            else
                _currentCutsceneLineIndex++;
        }

        public void ShowNextLine(int indexSectionCutscene)
        {
            var amountLines = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines.Length - 1;

            _currentSectionCutsceneLinesIndex = indexSectionCutscene;

            _currentCutsceneLineText.text = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines[_currentCutsceneLineIndex];

            if (_currentCutsceneLineIndex >= amountLines)
            {
                _currentSectionCutsceneLinesIndex++;
                _currentCutsceneLineIndex = 0;
            }
            else
                _currentCutsceneLineIndex++;
        }

        public void HideCutsceneLine()
        {
            _dialogBoxAnimator.SetBool("CanFadeIn", false);

        }



        private IEnumerator SetFadeOutBlackScreen()
        {
            _transitionAnimator.SetBool("CanFadeOut", true);
            yield return new WaitForSeconds(1.4f);
        }

        private void SetFadeInBlackScreen()
        {
            _transitionAnimator.SetBool("CanFadeOut", false);
        }

        [ContextMenu("Invoke: " + nameof(SetOffAllVideoPlayer))]

        private void SetOffAllVideoPlayer()
        {
            _skipCutsceneLineButton.interactable = false;

            for (int frameIndex = 0; frameIndex < _allVideoClips.Length; frameIndex++)
                _allVideoClips[frameIndex].gameObject.SetActive(false);

            _allVideoClips[_currentVideoClipIndex].gameObject.SetActive(true);

            StartCoroutine(SetFadeOutBlackScreen());
        }

        private void GetAllVideosInnitialCutscene()
        {
            var currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "CUTSCENE")
            {

                for (int videoIndex = 0; videoIndex < 17; videoIndex++)
                {
                    //_allVideoClips[videoIndex].url = System.IO.Path.Combine(
                    //    "https://sistemashomologacao.suafaculdade.com.br/Jogos/unity/TeenSpyOps1/Videos/CutsceneInicial/",
                    //    "INICIAL_" 
                    //    + (videoIndex + 1) + ".mp4");
                }
            }
            else
            {
                for (int videoIndex = 0; videoIndex < 9; videoIndex++)
                {
                    //_allVideoClips[videoIndex].url = System.IO.Path.Combine(
                    //    "https://sistemashomologacao.suafaculdade.com.br/Jogos/unity/TeenSpyOps1/Videos/CutsceneFinal/",
                    //    "FINAL_" +
                    //    (videoIndex + 1) + ".mp4");
                }
            }

        }





        [ContextMenu("Invoke: " + nameof(LoadNextScene))]
        private void LoadNextScene() => StartCoroutine(StartLoadNextScene());


        private IEnumerator StartLoadNextScene()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("LEVELSELECT");
        }

        private IEnumerator CountdownDelaySkipVideo()
        {
            yield return new WaitForSeconds(_delayToSkipVideo);
            _canSkipVideo = true;
        }

        private IEnumerator ShowNextFrame()
        {
            var lastFrameworkIndex = _allVideoClips.Length - 1;

            var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SetFadeInBlackScreen();
            _dialogBoxAnimator.SetBool("CanFadeIn", false);

            if (_currentVideoClipIndex < lastFrameworkIndex)
            {
                _currentVideoClipIndex++;
                yield return new WaitForSeconds(1.6f);
                SetOffAllVideoPlayer();
            }

            yield return null;
        }



    }
}
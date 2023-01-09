using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Controllers
{
    public class CutsceneController : MonoBehaviour
    {
        [SerializeField] private int _currentVideoClipIndex = 0;
        [SerializeField] private int _currentCutsceneLineIndex = 0;
        [SerializeField] private int _currentSectionCutsceneLinesIndex = 0;
        [Space]

        [SerializeField] private int _currentStep = 0;
        [Space]
        [SerializeField] private int _currentFrameVideoPlayer;
        [SerializeField] private int _totalFramesVideoPlayer;

        private Image _arrowIndicatingCanJump;
        private Text _currentCutsceneLineText;
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

            _arrowIndicatingCanJump = GameObject.Find("ArrowIndicateCanJump").GetComponent<Image>();

            _transitionAnimator = GameObject.Find("TransitionCutscene").GetComponent<Animator>();
            _dialogBoxAnimator = GameObject.Find("BlackDialogBox").GetComponent<Animator>();

            _currentCutsceneLineText = GameObject.Find("CurrentCutsceneLineText").GetComponent<Text>();

            _skipCutsceneLineButton = GameObject.Find("SkipToNextLineOrFramkeworkButton").GetComponent<Button>();
            _invokeNextStepInvisible = GameObject.Find("InvokeNextStepInvisible").GetComponent<Button>();
        }

        private void FixedUpdate()
        {
            RefreshFrames();
        }

        private void RefreshFrames()
        {
            _currentFrameVideoPlayer = (int)_currentVideoClip.frame;
            _totalFramesVideoPlayer = (int)(_currentVideoClip.frameCount);
        }

        private void Start()
        {
            // 1 - Fade out.
            // 2 - Show FrameWork.
            // 3 - Show Cutscene Line.
            // 4 - Show next Cutscene Line * If have other Cutscene Line.
            // 5 - Fade in with next scene and Repeat.

            InnitialStep();
        }

        private void InnitialStep()
        {
            _arrowIndicatingCanJump.gameObject.SetActive(false);

            _transitionAnimator.gameObject.SetActive(false);

            _currentVideoClip = _allVideoClips[_currentVideoClipIndex];
            InvokeNextStep();
        }

        public void InvokeNextStep()
        {
            //if (CheckHaveMoreLines())
            //{
            //    ShowNextLine();
            //    return;
            //}

            _allStepsCutscene[_currentStep]?.Invoke();

            if (_currentStep < _allStepsCutscene.Length - 1)
                _currentStep++;

        }

        public void PlayCurrentVideo()
        {
            _currentVideoClip.Play();
        }

        public void PauseCurrentVideo()
        {
            _currentVideoClip.Pause();

        }

        public void PlayVideoByIndex(int videoIndex)
        {
            _currentVideoClip = _allVideoClips[videoIndex];

            _currentVideoClip.Play();

        }

        public void GoNextStepInTheEnd()
        {
            StartCoroutine(WaitingFinishVideoPlayer());
        }

        public void CanInvokeNextStep(float seconds)
        {
            StartCoroutine(WaitingForJump(seconds));

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
            var blackDialogBox = GameObject.Find("BlackDialogBox").GetComponent<Animator>();

            blackDialogBox.SetBool("CanFadeIn", false);
        }

        public void LoadLevelSelectScene()
        {
            LoadNextScene();
        }

        private bool CheckHaveMoreLines()
        {
            var amountLines = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex].CutsceneLines.Length - 1;

            if (_currentCutsceneLineIndex >= amountLines)
            {
                print("Não há mais falas na seção: " + _currentSectionCutsceneLinesIndex);
                return false;
            }
            else
            {
                print("Há mais falas na seção: " + _currentSectionCutsceneLinesIndex);
                return true;
            }
        }

        private IEnumerator SetFadeOutBlackScreen()
        {
            _transitionAnimator.SetBool("CanFadeOut", true);
            yield return new WaitForSeconds(1.4f);
            //StartCoroutine(ShowCutsceneLine());
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



        private void ShowNextSectionCutsceneLine()
        {
            var lastSectionCutsceneLinesIndex = _sectionCutsceneLinesList.Count - 1;

            if (_currentSectionCutsceneLinesIndex < lastSectionCutsceneLinesIndex)
            {
                _dialogBoxAnimator.SetBool("CanFadeIn", false);
                _skipCutsceneLineButton.onClick.RemoveAllListeners();
                _currentSectionCutsceneLinesIndex++;
                _currentCutsceneLineIndex = 0;
                StartCoroutine(ShowNextFrame());
            }

        }

        private void ShowNextFrameworkLine()
        {
            _skipCutsceneLineButton.onClick.RemoveAllListeners();
            _skipCutsceneLineButton.interactable = false;
            _currentCutsceneLineIndex++;
            //StartCoroutine(ShowCutsceneLine());
        }

        [ContextMenu("Invoke: " + nameof(LoadNextScene))]
        private void LoadNextScene() => StartCoroutine(StartLoadNextScene());

        //private IEnumerator ShowCutsceneLine()
        //{
        //    var lastCutsceneLineIndex = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex]._allCutsceneLines.Length - 1;
        //    var lastFrameworkIndex = _allFrames.Length - 1;

        //    _currentCutsceneLineText.text = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex]._allCutsceneLines[_currentCutsceneLineIndex];
        //    _dialogBoxAnimator.SetBool("CanFadeIn", true);

        //    _skipCutsceneLineButton.interactable = true;

        //    if (_currentFrameIndex < lastFrameworkIndex)
        //    {
        //        if (_currentCutsceneLineIndex >= lastCutsceneLineIndex)
        //        {
        //            _skipCutsceneLineButton.onClick.AddListener(ShowNextSectionCutsceneLine);
        //        }
        //        else
        //        {
        //            _skipCutsceneLineButton.onClick.AddListener(ShowNextFrameworkLine);
        //        }
        //    }
        //    else
        //    {
        //        _skipCutsceneLineButton.onClick.AddListener(LoadNextScene);
        //    }

        //    yield return null;

        //}

        private IEnumerator StartLoadNextScene()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("LEVELSELECT");
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CutsceneController : MonoBehaviour
    {
        [SerializeField] private int _currentFrameIndex = 0;
        [SerializeField] private int _currentCutsceneLineIndex = 0;
        [SerializeField] private int _currentSectionCutsceneLinesIndex = 0;

        private Text _currentCutsceneLineText;
        private Button _skipCutsceneLineButton;

        private Image[] _allFrames;
        private Animator _transitionAnimator;
        private Animator _dialogBoxAnimator;

        [Serializable]
        private class SectionsCutsceneLines
        {
            public string NameArray;
            public string[] _allCutsceneLines;
        }

        [SerializeField] private List<SectionsCutsceneLines> _sectionCutsceneLinesList = new List<SectionsCutsceneLines>();

        private void Awake()
        {
            _allFrames = GameObject.Find("Frameworks").GetComponentsInChildren<Image>();

            _transitionAnimator = GameObject.Find("TransitionCutscene").GetComponent<Animator>();
            _dialogBoxAnimator = GameObject.Find("BlackDialogBox").GetComponent<Animator>();

            _currentCutsceneLineText = GameObject.Find("CurrentCutsceneLineText").GetComponent<Text>();

            _skipCutsceneLineButton = GameObject.Find("SkipToNextLineOrFramkeworkButton").GetComponent<Button>();
        }

        private void Start()
        {
            // 1 - Fade out.
            // 2 - Show FrameWork.
            // 3 - Show Cutscene Line.
            // 4 - Show next Cutscene Line * If have other Cutscene Line.
            // 5 - Fade in with next scene and Repeat.

            SetOffAllFrames();
        }

        private IEnumerator SetFadeOutBlackScreen()
        {
            _transitionAnimator.SetBool("CanFadeOut", true);
            yield return new WaitForSeconds(1.4f);
            StartCoroutine(ShowCutsceneLine());
        }

        private void SetFadeInBlackScreen()
        {
            _transitionAnimator.SetBool("CanFadeOut", false);
        }

        private void SetOffAllFrames()
        {
            _skipCutsceneLineButton.interactable = false;

            for (int frameIndex = 0; frameIndex < _allFrames.Length; frameIndex++)
                _allFrames[frameIndex].gameObject.SetActive(false);

            _allFrames[_currentFrameIndex].gameObject.SetActive(true);

            StartCoroutine(SetFadeOutBlackScreen());
        }

        private IEnumerator ShowCutsceneLine()
        {
            var lastCutsceneLineIndex = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex]._allCutsceneLines.Length - 1;

            _currentCutsceneLineText.text = _sectionCutsceneLinesList[_currentSectionCutsceneLinesIndex]._allCutsceneLines[_currentCutsceneLineIndex];
            _dialogBoxAnimator.SetBool("CanFadeIn", true);


            _skipCutsceneLineButton.interactable = true;

            if (_currentCutsceneLineIndex >= lastCutsceneLineIndex)
            {
                _skipCutsceneLineButton.onClick.AddListener(ShowNextSectionCutsceneLine);
            }
            else
            {
                _skipCutsceneLineButton.onClick.AddListener(ShowNextFrameworkLine);
            }

            yield return null;

        }

        private void ShowNextSectionCutsceneLine()
        {
            if (_currentSectionCutsceneLinesIndex < _sectionCutsceneLinesList.Count - 1)
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
            StartCoroutine(ShowCutsceneLine());
        }

        private IEnumerator ShowNextFrame()
        {
            var lastFrameworkIndex = _allFrames.Length - 1;

            SetFadeInBlackScreen();
            _dialogBoxAnimator.SetBool("CanFadeIn", false);

            if (_currentFrameIndex < lastFrameworkIndex)
                _currentFrameIndex++;

            yield return new WaitForSeconds(1.6f);

            SetOffAllFrames();
        }

    }
}
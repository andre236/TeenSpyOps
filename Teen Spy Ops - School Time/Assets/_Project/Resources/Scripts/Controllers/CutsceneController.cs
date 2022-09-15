using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CutsceneController : MonoBehaviour
    {
        private int _currentSceneIndex = 0;
        [SerializeField] private string[] _allCutsceneLines;

        private Text _currentCutsceneLineText;

        private Image[] _allFrameworks;
        private Animator _transitionAnimator;

        private void Awake()
        {
            _allFrameworks = GameObject.Find("Frameworks").GetComponentsInChildren<Image>();
            _transitionAnimator = GameObject.Find("TransitionCutscene").GetComponent<Animator>();
            _currentCutsceneLineText = GameObject.Find("CurrentCutsceneLineText").GetComponent<Text>();
        }

        private void Start()
        {
            SetOffAllFrameworks();
        }

        private void StartFadeOut()
        {
            // 1 - Fade out.
            // 2 - Show FrameWork.
            // 3 - Show Cutscene Line.
            // 4 - Show next Cutscene Line * If have other Cutscene Line.
            // 5 - Fade in with next scene and Repeat.
            _transitionAnimator.SetBool("CanFadeOut", true);
        }

        private void SetOffAllFrameworks()
        {
            for (int indexFramework = 0; indexFramework < _allFrameworks.Length; indexFramework++)
                _allFrameworks[indexFramework].gameObject.SetActive(false);

            _allFrameworks[_currentSceneIndex].gameObject.SetActive(true);
            StartFadeOut();
        }

        private void ShowCutsceneLine()
        {
            if(_currentSceneIndex >= _allCutsceneLines.Length)
            {
                StartFadeIn();
                StartNextFramework();
                return;
            }

            ShowNextFrameworkLine();
        }

        private void ShowNextFrameworkLine()
        {
            
        }

        private void StartNextFramework()
        {
            _currentSceneIndex++;
            SetOffAllFrameworks();
        }

        private void StartFadeIn()
        {
            _transitionAnimator.SetBool("CanFadeOut", false);
        }
    }
}
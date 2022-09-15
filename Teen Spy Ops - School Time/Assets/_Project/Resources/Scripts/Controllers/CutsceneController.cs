using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CutsceneController : MonoBehaviour
    {
        private int _currentSceneIndex = 0;

        private Image[] _allFrameworks;
        private Animator _transitionAnimator;

        private void Awake()
        {
            _allFrameworks = GameObject.Find("Frameworks").GetComponentsInChildren<Image>();
            _transitionAnimator = GameObject.Find("TransitionCutscene").GetComponent<Animator>();
        }

        private void Start()
        {
            SetOffAllFrameworks();
        }

        private void StartFadeOut()
        {

        }

        private void SetOffAllFrameworks()
        {
            for (int indexFramework = 0; indexFramework < _allFrameworks.Length; indexFramework++)
                _allFrameworks[indexFramework].gameObject.SetActive(false);

            _allFrameworks[_currentSceneIndex].gameObject.SetActive(true);
        }

        


    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Manager
{
    public class SceneryManager : MonoBehaviour
    {
        private GameObject _normalScene;
        private GameObject _xRayScene;
        private GameObject _thirdDistance;
        private GameObject _secondDistance;
        private GameObject _firstDistace;
        private GameObject _nightVisionScene;
        private GameObject _fingerprintScene;

        private Animator _xRayAnimator;
        private Animator _nightVisionAnimator;

        private GameManager _gameManager;

        public Action<GameObject> DefinedActivedScenery;
        public Action DefinedStandardActivedScenery;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();

            _normalScene = GameObject.FindGameObjectWithTag("Normal");
            _xRayScene = GameObject.FindGameObjectWithTag("XRay");
            _xRayAnimator = _xRayScene.GetComponent<Animator>();

            _firstDistace = GameObject.Find("FirstDistance");
            _secondDistance = GameObject.Find("SecondDistance");
            _thirdDistance = GameObject.Find("ThirdDistance");

            _nightVisionScene = GameObject.FindGameObjectWithTag("NightVision");
            _nightVisionAnimator = _nightVisionScene.GetComponent<Animator>();

            _fingerprintScene = GameObject.FindGameObjectWithTag("Fingerprint");
        }

        private void Start()
        {
            DefinedActivedScenery += OnDefinedActiveScenery;
            DefinedStandardActivedScenery += OnDefinedStandardActivedScenery;
        }

        internal void OnInitializedLevel()
        {
            _firstDistace.SetActive(false);
            _secondDistance.SetActive(false);
            _thirdDistance.SetActive(false);

            _normalScene.SetActive(true);
            _nightVisionScene.SetActive(false);
            _xRayScene.SetActive(false);
            _fingerprintScene.SetActive(false);
        }

        private void OnDefinedStandardActivedScenery() => StartCoroutine(nameof(ActiveDefaultSceneWithDelay));

        private void OnDefinedActiveScenery(GameObject gameObj)
        {
            if (gameObj.activeSelf)
                return;

            _normalScene.SetActive(false);
            _nightVisionScene.SetActive(false);
            _xRayScene.SetActive(false);
            _fingerprintScene.SetActive(false);
            gameObj.SetActive(true);

        }

        internal void OnActivedXRay()
        {
            if (_gameManager.CurrentSkill != SkillState.XRay)
            {
                DefinedStandardActivedScenery?.Invoke();
                _xRayAnimator.SetBool("OnXRay", false);
                return;
            }

            DefinedActivedScenery?.Invoke(_xRayScene);
            _xRayAnimator.SetBool("OnXRay", true);

        }

        internal void OnUpgradeXRayVision()
        {
            switch (_gameManager.CurrentDistance)
            {
                case XRayDistance.First:
                    _firstDistace.SetActive(true);
                    _secondDistance.SetActive(true);
                    _thirdDistance.SetActive(true);
                    break;
                case XRayDistance.Second:
                    _firstDistace.SetActive(false);
                    _secondDistance.SetActive(true);
                    _thirdDistance.SetActive(true);
                    break;
                case XRayDistance.Third:
                    _firstDistace.SetActive(false);
                    _secondDistance.SetActive(false);
                    _thirdDistance.SetActive(true);
                    break;
                case XRayDistance.None:
                    _firstDistace.SetActive(false);
                    _secondDistance.SetActive(false);
                    _thirdDistance.SetActive(false);
                    break;
            }
        }

        internal void OnActivedFingerprint()
        {
            if (_gameManager.CurrentSkill != SkillState.Fingerprint)
            {
                DefinedStandardActivedScenery?.Invoke();
                return;
            }

            DefinedActivedScenery(_fingerprintScene);
        }

        internal void OnActivedNightVision()
        {
            if (_gameManager.CurrentSkill != SkillState.NightVision)
            {
                DefinedStandardActivedScenery?.Invoke();
                _nightVisionAnimator.SetBool("OnNightVision", false);
                return;
            }

            DefinedActivedScenery?.Invoke(_nightVisionScene);

            _nightVisionAnimator.SetBool("OnNightVision", true);
        }

        internal void FinishedTimerSkill()
        {
            DefinedStandardActivedScenery?.Invoke();
            _xRayAnimator.SetBool("OnXRay", false);
        }

        private IEnumerator ActiveDefaultSceneWithDelay()
        {
            yield return new WaitForSeconds(2f);

            _firstDistace.SetActive(false);
            _secondDistance.SetActive(false);
            _thirdDistance.SetActive(false);
            _nightVisionScene.SetActive(false);
            _xRayScene.SetActive(false);
            _fingerprintScene.SetActive(false);

            _normalScene.SetActive(true);
        }
    }
}
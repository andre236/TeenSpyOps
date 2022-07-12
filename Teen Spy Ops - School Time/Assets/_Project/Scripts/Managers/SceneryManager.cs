using System;
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
        
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();

            _normalScene = GameObject.FindGameObjectWithTag("Normal");
            _xRayScene = GameObject.FindGameObjectWithTag("XRay");
            
            _firstDistace = GameObject.Find("FirstDistance");
            _secondDistance = GameObject.Find("SecondDistance");
            _thirdDistance = GameObject.Find("ThirdDistance");

            _nightVisionScene = GameObject.FindGameObjectWithTag("NightVision");
            _fingerprintScene = GameObject.FindGameObjectWithTag("Fingerprint");
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

        internal void OnActivedXRay()
        {
            if(_gameManager.CurrentSkill != SkillState.XRay)
            {
                _normalScene.SetActive(true);
                _xRayScene.SetActive(false);
                _nightVisionScene.SetActive(false);
                _fingerprintScene.SetActive(false);
                return;
            }

            _xRayScene.SetActive(true);            
            _normalScene.SetActive(false);
            _nightVisionScene.SetActive(false);
            _fingerprintScene.SetActive(false);

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
                _normalScene.SetActive(true);
                _xRayScene.SetActive(false);
                _nightVisionScene.SetActive(false);
                _fingerprintScene.SetActive(false);

                return;
            }

            _fingerprintScene.SetActive(true);
            _xRayScene.SetActive(false);
            _normalScene.SetActive(false);
            _nightVisionScene.SetActive(false);
        }

        internal void OnActivedNightVision()
        {
            if (_gameManager.CurrentSkill != SkillState.NightVision)
            {
                _normalScene.SetActive(true);
                _xRayScene.SetActive(false);
                _nightVisionScene.SetActive(false);
                _fingerprintScene.SetActive(false);
                return;
            }

            _nightVisionScene.SetActive(true);
            _xRayScene.SetActive(false);
            _normalScene.SetActive(false);
            _fingerprintScene.SetActive(false);
        }
    }
}
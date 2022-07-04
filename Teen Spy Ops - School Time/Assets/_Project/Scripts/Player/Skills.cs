using System;
using UnityEngine;
using Manager;

namespace Player
{
    public class Skills : MonoBehaviour
    {

        [Range(0, 15)] [SerializeField] private float _timerXRay;
        [Range(0, 15)] [SerializeField] private float _timerFingerprint;

        [Range(0, 99)] [SerializeField] private float _firstDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _secondDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _thirdDistanceRangeXray;

        private GameManager _gameManager;
        private GameObject _cursorMaskVision;


        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _cursorMaskVision = GameObject.Find("CursorMask");
        }

        public void OnInitializedLevel()
        {
            _cursorMaskVision.SetActive(false);
        }

        internal void OnActivedXRay()
        {
            if (_gameManager.CurrentSkill != SkillState.XRay)
            {
                _cursorMaskVision.SetActive(false);
                return;
            }

            _cursorMaskVision.SetActive(true);

            switch (_gameManager.CurrentDistance)
            {
                case XRayDistance.First:
                    _cursorMaskVision.transform.localScale = new Vector2(_firstDistanceRangeXray, _firstDistanceRangeXray);
                    break;
                case XRayDistance.Second:
                    _cursorMaskVision.transform.localScale = new Vector2(_secondDistanceRangeXray, _secondDistanceRangeXray);
                    break;
                case XRayDistance.Third:
                    _cursorMaskVision.transform.localScale = new Vector2(_thirdDistanceRangeXray, _thirdDistanceRangeXray);
                    break;
            }

        }

        internal void OnActivedFingerprint()
        {
            _cursorMaskVision.SetActive(false);
            Debug.Log("Poder fingerprint ativado!");
        }

        internal void OnActivedNightVision()
        {
            _cursorMaskVision.SetActive(false);
            Debug.Log("Poder Visão Noturna ativado!");
        }
    }
}
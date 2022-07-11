using System;
using UnityEngine;
using Manager;

namespace Player
{
    public class Skills : MonoBehaviour
    {
        [Range(0, 60)] [SerializeField] private float _timerXray;
        [Range(0, 60)] [SerializeField] private float _cooldownToXray;

        [Range(0, 60)] [SerializeField] private float _timerFingerprint;
        [Range(0, 60)] [SerializeField] private float _cooldownFingerprint;


        [Range(0, 99)] [SerializeField] private float _firstDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _secondDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _thirdDistanceRangeXray;

        private GameObject _cursorMaskVision;

        private void Awake() => _cursorMaskVision = GameObject.Find("CursorMask");

        public void OnInitializedLevel() => _cursorMaskVision.SetActive(false);

        internal void OnCooldownSkills()
        {
            if(_timerXray > 0)
            {
                _timerXray -= Time.deltaTime;
            }
        }

        internal void OnActivedXRay()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill != SkillState.XRay)
            {
                _cursorMaskVision.SetActive(false);
                return;
            }

            _cursorMaskVision.SetActive(true);



            switch (gameManager.CurrentDistance)
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
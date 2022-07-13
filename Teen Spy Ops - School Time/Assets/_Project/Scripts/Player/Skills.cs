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
        private GameObject _laserMask;

        private void Awake()
        {
            _cursorMaskVision = GameObject.Find("CursorMask");
            _laserMask = GameObject.Find("LaserMask");
        }

        internal void OnInitializedLevel()
        {
            _cursorMaskVision.SetActive(false);
            _laserMask.SetActive(false);

        }

        //internal void OnCooldownSkills()
        //{
        //    if(_timerXray > 0)
        //    {
        //        _timerXray -= Time.deltaTime;
        //    }
        //}

        internal void OnActivedXRay()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill != SkillState.XRay)
            {
                _cursorMaskVision.SetActive(false);
                return;
            }

            if(!_cursorMaskVision.activeSelf)
                Invoke(nameof(ActiveMaskCursor), 1f);
            
            //_cursorMaskVision.SetActive(true);
            _laserMask.SetActive(false);



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
            _laserMask.SetActive(true);
            _cursorMaskVision.SetActive(false);

            _laserMask.GetComponent<Animator>().speed = 1/(_timerFingerprint / 2);

            
                Debug.Log("Poder fingerprint ativado!");
        }

        internal void OnActivedNightVision()
        {
            _laserMask.SetActive(false);

            _cursorMaskVision.SetActive(false);
            Debug.Log("Poder Visão Noturna ativado!");
        }

        private void ActiveMaskCursor()
        {
            _cursorMaskVision.SetActive(true);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Skills : MonoBehaviour
    {
        private bool _onCoolDownSkill;

        [Range(0, 15)][SerializeField] private float _timerXRay;
        [Range(0, 15)][SerializeField] private float _timerDigital;
        
        [SerializeField] private float _firstRangeVisionXRay;
        [SerializeField] private float _secondRangeVisionXRay;
        [SerializeField] private float _thirdRangeVisionXRay;

        private GameObject _cursorMaskVision;


        private void Awake()
        {
            _cursorMaskVision = GameObject.Find("CursorMask");    
        }

        private void Start()
        {
            _cursorMaskVision.SetActive(false);
            _onCoolDownSkill = false;
        }

        public void OnActivedXRay()
        {
            if (_onCoolDownSkill)
                return;

            _cursorMaskVision.gameObject.SetActive(true);
            _onCoolDownSkill = true;
            Invoke(nameof(SetOffCoolDown), _timerXRay);
        }

        private bool SetOffCoolDown()
        {
            return _onCoolDownSkill = false;
        }
    }
}
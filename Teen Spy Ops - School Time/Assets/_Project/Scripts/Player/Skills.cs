using System;
using UnityEngine;
using Manager;

namespace Player
{
    public class Skills : MonoBehaviour
    {
        [SerializeField] private bool _alreadyXRayCast;
        [SerializeField] private bool _alreadyFingerprint;
        [SerializeField] private bool _alreadyNightVision;


        [field: Range(0, 60)] [field: SerializeField] public float TimerXray { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentTimerXray { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CooldownToXray { get; private set; }
        [field: Space]
        [field: Range(0, 60)] [field: SerializeField] public float TimerFingerprint { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentTimerFingerprint { get; private set; }

        [field: Range(0, 60)] [field: SerializeField] public float CooldownFingerprint { get; private set; }
        [field: Space]
        [field: Range(0, 60)] [field:SerializeField] public float TimerNightVision { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentTimerNightVision { get; private set; }

        [field: Range(0, 60)] [field: SerializeField] public float CooldownNightVision { get; private set; }
        
        public bool AlreadyXRayCast { get => _alreadyXRayCast; private set => _alreadyXRayCast = value; }
        public bool AlreadyFingerprint { get => _alreadyFingerprint; private set => _alreadyFingerprint = value; }
        public bool AlreadyNightVision { get => _alreadyNightVision; private set => _alreadyNightVision = value; }

        [Space]
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

            CurrentTimerXray = TimerXray;
            CurrentTimerFingerprint = TimerFingerprint;
            CurrentTimerNightVision = TimerNightVision;

        }

        internal void OnCountdownXRayTimer()
        {
            if (AlreadyXRayCast)
                return;

            if (CurrentTimerXray > 0)
            {
                CurrentTimerXray -= Time.deltaTime;
            }
            else
            {
                CurrentTimerXray = TimerXray;
                AlreadyXRayCast = true;
                Debug.Log("Skill pronta para uso novamente.");
            }

             
        }

        internal void OnCooldownTimerprint()
        {
            if (AlreadyFingerprint)
                return;

        }

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

            AlreadyXRayCast = false;

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

            _laserMask.GetComponent<Animator>().speed = 1/(TimerFingerprint / 2);

            
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
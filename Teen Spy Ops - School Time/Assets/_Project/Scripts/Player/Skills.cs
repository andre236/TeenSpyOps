using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Manager;
using UnityEngine.UI;

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
        [field: Range(0, 60)] [field: SerializeField] public float CurrentCooldownToXray { get; private set; }


        [field: Space]
        [field: Range(0, 60)] [field: SerializeField] public float TimerFingerprint { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentTimerFingerprint { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CooldownFingerprint { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentCooldownFingerprint { get; private set; }


        [field: Space]
        [field: Range(0, 60)] [field: SerializeField] public float TimerNightVision { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentTimerNightVision { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CooldownNightVision { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CurrentCooldownNightVision { get; private set; }


        public bool AlreadyXRayCast { get => _alreadyXRayCast; private set => _alreadyXRayCast = value; }
        public bool AlreadyFingerprint { get => _alreadyFingerprint; private set => _alreadyFingerprint = value; }
        public bool AlreadyNightVision { get => _alreadyNightVision; private set => _alreadyNightVision = value; }

        [Space]
        [Range(0, 99)] [SerializeField] private float _firstDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _secondDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _thirdDistanceRangeXray;

        private GameObject _cursorMaskVision;
        private GameObject _laserMask;

        public Action<float, float> CountdownNightVisionTimer;
        public Action<float, float> CountdownXrayTimer;
        public Action<float, float> CountdownFingerprintTimer;

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
            CurrentCooldownToXray = CooldownToXray;
            CurrentCooldownFingerprint = CooldownFingerprint;
            CurrentCooldownNightVision = CooldownNightVision;

        }

        internal void OnActivedXRay()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill != SkillState.XRay)
            {
                _cursorMaskVision.SetActive(false);
                return;
            }

            if (!_cursorMaskVision.activeSelf)
                Invoke(nameof(ActiveMaskCursor), 1f);

            _laserMask.SetActive(false);
            AlreadyXRayCast = false;
            StartCoroutine(CooldownToUseXray(CurrentCooldownToXray, CooldownToXray, AlreadyXRayCast));

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

            _laserMask.GetComponent<Animator>().speed = 1 / (TimerFingerprint / 2);

            StartCoroutine(CooldownToUseFingerprint(CurrentCooldownFingerprint, CurrentCooldownFingerprint, AlreadyFingerprint));
            AlreadyFingerprint = false;

            Debug.Log("Poder fingerprint ativado!");
        }

        internal void OnActivedNightVision()
        {
            _laserMask.SetActive(false);

            _cursorMaskVision.SetActive(false);

            StartCoroutine(CooldownToUseNightVision(CurrentTimerNightVision, CooldownNightVision, AlreadyNightVision));
            AlreadyNightVision = false;

            Debug.Log("Poder Visão Noturna ativado!");
        }

        private void ActiveMaskCursor()
        {
            _cursorMaskVision.SetActive(true);
        }

        IEnumerator CooldownToUseXray(float timer, float initialTime, bool alreadySkill)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            while (timer > 0)
            {
                if (gameManager.CurrentGameState == GameState.Running)
                {
                    timer -= Time.deltaTime;
                }

                CountdownXrayTimer?.Invoke(timer, initialTime);
                yield return timer;
            }

            if (timer <= 0)
            {
                alreadySkill = true;
                Debug.Log("A skill está pronta");

            }
        }

        IEnumerator CooldownToUseFingerprint(float timer, float initialTime, bool alreadySkill)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            while (timer > 0)
            {
                if (gameManager.CurrentGameState == GameState.Running)
                {
                    timer -= Time.deltaTime;
                }

                CountdownFingerprintTimer?.Invoke(timer, initialTime);
                yield return timer;
            }

            if (timer <= 0)
            {
                alreadySkill = true;
                Debug.Log("A skill está pronta");

            }
        }

        IEnumerator CooldownToUseNightVision(float timer, float initialTime, bool alreadySkill)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            while (timer > 0)
            {
                if (gameManager.CurrentGameState == GameState.Running)
                {
                    timer -= Time.deltaTime;
                }

                CountdownNightVisionTimer?.Invoke(timer, initialTime);
                yield return timer;
            }

            if (timer <= 0)
            {
                alreadySkill = true;
                Debug.Log("A skill está pronta");

            }
        }


    }
}
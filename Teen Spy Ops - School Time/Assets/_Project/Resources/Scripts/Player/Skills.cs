using System;
using UnityEngine;
using System.Collections;
using Manager;

namespace Player
{
    public class Skills : MonoBehaviour
    {
        private bool _alreadyXRayCast;
        private bool _alreadyFingerprint;
        private bool _alreadyNightVision;

        [SerializeField] private bool _reset;


        [field: Range(0, 90)] [field: SerializeField] public float TimerXray { get; private set; }
        public float CurrentTimerXray { get; private set; }
        [field: Range(0, 90)] [field: SerializeField] public float CooldownToXray { get; private set; }
        public float CurrentCooldownToXray { get; private set; }


        [field: Space]
        [field: Range(0, 60)] [field: SerializeField] public float TimerFingerprint { get; private set; }
        public float CurrentTimerFingerprint { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CooldownFingerprint { get; private set; }
        public float CurrentCooldownFingerprint { get; private set; }


        [field: Space]
        [field: Range(0, 60)] [field: SerializeField] public float TimerNightVision { get; private set; }
        public float CurrentTimerNightVision { get; private set; }
        [field: Range(0, 60)] [field: SerializeField] public float CooldownNightVision { get; private set; }
        public float CurrentCooldownNightVision { get; private set; }


        public bool AlreadyXRayCast { get => _alreadyXRayCast; protected set => _alreadyXRayCast = value; }
        public bool AlreadyFingerprint { get => _alreadyFingerprint; protected set => _alreadyFingerprint = value; }
        public bool AlreadyNightVision { get => _alreadyNightVision; protected set => _alreadyNightVision = value; }
         public bool Reset { get => _reset; set => _reset = value; }

        [Space]
        [Range(0, 99)] [SerializeField] private float _firstDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _secondDistanceRangeXray;
        [Range(0, 99)] [SerializeField] private float _thirdDistanceRangeXray;

        protected GameObject _cursorMaskVision;
        protected GameObject _laserMask;

        public Action FinishedTimerSkill;

        public Action<float, float> CountdownNightVisionCooldown;
        public Action<float, float> CountdownXrayCooldown;
        public Action<float, float> CountdownFingerprintCooldown;

        public Action<float, float> CountdownXrayTimer;
        public Action<float, float> CountdownFingerprintTimer;
        public Action<float, float> CountdownNightVisionTimer;


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

        internal virtual void OnActivedXRay()
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

            OnUpgradeXRayVision();
            StartCoroutine(TimerForSkill(CurrentTimerXray, TimerXray, CurrentCooldownToXray, CooldownToXray, AlreadyXRayCast, CountdownXrayTimer, CountdownXrayCooldown));



        }

        internal void OnUpgradeXRayVision()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

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

            StartCoroutine(TimerForSkill(CurrentTimerFingerprint, TimerFingerprint, CurrentCooldownFingerprint, CurrentCooldownFingerprint, AlreadyFingerprint, CountdownFingerprintTimer, CountdownFingerprintCooldown));
            AlreadyFingerprint = false;

        }

        internal void OnActivedNightVision()
        {
            //if(AlreadyNightVision == false)
            //{
            //    StartCoroutine(TimerForSkill(CurrentTimerNightVision, TimerNightVision, CurrentCooldownNightVision, CooldownNightVision, AlreadyNightVision, CountdownNightVisionTimer, CountdownNightVisionCooldown));
            //    return;
            //}

            _laserMask.SetActive(false);
            _cursorMaskVision.SetActive(false);

            StartCoroutine(TimerForSkill(CurrentTimerNightVision, TimerNightVision, CurrentCooldownNightVision, CooldownNightVision, AlreadyNightVision, CountdownNightVisionTimer, CountdownNightVisionCooldown));
            AlreadyNightVision = false;

            Debug.Log("Passei por aqui");
        }

        internal void OnResetSkillNightVision()
        {
            Reset = true;
            StartCoroutine(TurnReset());
            //StartCoroutine(TimerForSkill(CurrentTimerNightVision, TimerNightVision, CurrentCooldownNightVision, CooldownNightVision, AlreadyNightVision, CountdownNightVisionTimer, CountdownNightVisionCooldown));

        }

        protected void ActiveMaskCursor()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill != SkillState.XRay)
            {
                _cursorMaskVision.SetActive(false);
                return;
            }

            _cursorMaskVision.SetActive(true);
        }

        protected void DeactiveAllMask()
        {
            _laserMask.SetActive(false);
            _cursorMaskVision.SetActive(false);

        }

        internal virtual IEnumerator TimerForSkill(float timer, float initialTimer, float cooldown, float initialCooldown, bool alreadySkill, Action<float, float> countdownUsingSkill, Action<float, float> countdownSkillCooldown)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            while (timer > 0)
            {
                if (gameManager.CurrentGameState == GameState.Running)
                {
                    if (!Reset)
                        timer -= Time.deltaTime;
                    else
                        timer = 0;
                }

                countdownUsingSkill?.Invoke(timer, initialTimer);

                yield return timer;
            }

            if (timer <= 0)
            {
                StartCoroutine(CooldownToUseSkill(cooldown, initialCooldown, alreadySkill, countdownSkillCooldown));
                FinishedTimerSkill?.Invoke();
                DeactiveAllMask();
            }
        }


        protected IEnumerator CooldownToUseSkill(float cooldown, float initialCooldown, bool alreadySkill, Action<float, float> countdownSkill)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();

            while (cooldown > 0)
            {
                if (gameManager.CurrentGameState == GameState.Running)
                {
                    cooldown -= Time.deltaTime;
                }

                countdownSkill?.Invoke(cooldown, initialCooldown);

                yield return cooldown;
            }

            if (cooldown <= 0)
            {
                alreadySkill = true;
            }
        }

        private IEnumerator TurnReset()
        {
            yield return new WaitForSeconds(2f);
            Reset = false;
        }




    }
}
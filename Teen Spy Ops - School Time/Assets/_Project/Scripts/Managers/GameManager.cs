using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public GameState CurrentState { get; private set; }
        [field: SerializeField] public SkillState CurrentSkill { get; private set; }
        [field: SerializeField] public XRayDistance CurrentDistance { get; private set; }

        [field: SerializeField] public float TimerLevel { get; private set; }

        private void Awake()
        {

        }

        internal void OnCountdownTimerLevel()
        {
            if (CurrentState != GameState.Running)
                return;

            if (TimerLevel > 0)
            {
                TimerLevel -= Time.deltaTime;
            }
            else
            {
                TimerLevel = 0;
                CurrentState = GameState.None;
            }

        }

        internal void OnInitializedLevel()
        {
            CurrentState = GameState.Running;
            CurrentSkill = SkillState.Normal;
            CurrentDistance = XRayDistance.None;
        }

        internal void OnPausedGame() => CurrentState = GameState.Paused;

        internal void OnUnPausedGame() => CurrentState = GameState.Running;

        internal void OnActivedXRay()
        {

            if (CurrentSkill != SkillState.XRay)
            {
                CurrentSkill = SkillState.XRay;
            }

            if (CurrentSkill == SkillState.XRay)
            {
                if (CurrentDistance > XRayDistance.First)
                {
                    CurrentDistance--;
                }
                else
                {
                    CurrentDistance = XRayDistance.None;
                    CurrentSkill = SkillState.Normal;
                }
            }


        }

        internal void OnActivedFingerprint()
        {

            if (CurrentSkill != SkillState.Fingerprint)
            {
                CurrentSkill = SkillState.Fingerprint;
            }
            else
            {
                CurrentSkill = SkillState.Normal;
            }
        }

        internal void OnActivedNightVision()
        {

            if (CurrentSkill != SkillState.NightVision)
            {
                CurrentSkill = SkillState.NightVision;
            }
            else
            {
                CurrentSkill = SkillState.Normal;
            }
        }
    }
}
using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public GameState CurrentGameState { get; private set; }
        [field: SerializeField] public SkillState CurrentSkill { get; private set; }
        [field: SerializeField] public XRayDistance CurrentDistance { get; private set; }
        [field: SerializeField] public float InitialTimerLevel { get; private set; }
        public float TimerLevel { get; private set; }

        internal void OnCountdownTimerLevel()
        {
            if (CurrentGameState != GameState.Running)
                return;

            if (TimerLevel > 0)
            {
                TimerLevel -= Time.deltaTime;
            }
            else
            {
                TimerLevel = 0;
                CurrentGameState = GameState.Ended;
            }

        }

        internal void OnInitializedLevel()
        {
            TimerLevel = InitialTimerLevel;
            CurrentGameState = GameState.Running;
            CurrentSkill = SkillState.Normal;
            CurrentDistance = XRayDistance.None;
        }

        internal void OnPausedGame() => CurrentGameState = GameState.Paused;

        internal void OnUnPausedGame() => CurrentGameState = GameState.Running;

        internal void OnActivedXRay()
        {
            if (CurrentGameState != GameState.Running)
                return;

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
            if (CurrentGameState != GameState.Running)
                return;

            if (CurrentSkill != SkillState.Fingerprint)
            {
                CurrentSkill = SkillState.Fingerprint;

            }
            else
            {
                CurrentSkill = SkillState.Normal;
                CurrentDistance = XRayDistance.None;
            }

        }

        internal void OnLosedGame()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentGameState = GameState.Ended;
<<<<<<< Updated upstream
=======

        }

        internal void OnStoppedTime()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentGameState = GameState.Ended;
>>>>>>> Stashed changes
        }

        internal void OnWonGame()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentGameState = GameState.Ended;
            Debug.Log("Fim de jogo! Coletou todos os objetos!");
        }

        internal void OnActivedNightVision()
        {
            if (CurrentGameState != GameState.Running)
                return;

            if (CurrentSkill != SkillState.NightVision)
            {
                CurrentSkill = SkillState.NightVision;
            }
            else
            {
                CurrentSkill = SkillState.Normal;
                CurrentDistance = XRayDistance.None;
            }

        }
    }
}
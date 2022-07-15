using System;
using UnityEngine;
using Player;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public GameState CurrentGameState { get; private set; }
        [field: SerializeField] public SkillState CurrentSkill { get; private set; }
        [field: SerializeField] public XRayDistance CurrentDistance { get; private set; }

        internal void OnInitializedLevel()
        {
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
                CurrentDistance = XRayDistance.Third;
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
                CurrentDistance = XRayDistance.None;
                CurrentSkill = SkillState.Normal;
            }

        }

        internal void OnLosedGame()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentGameState = GameState.Ended;

        }

        internal void OnStoppedTime()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentGameState = GameState.Ended;
        }

        internal void OnWonGame()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentGameState = GameState.Ended;
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

        internal void OnFinishedTimerSkill()
        {
            if (CurrentGameState != GameState.Running)
                return;

            CurrentDistance = XRayDistance.None;
            CurrentSkill = SkillState.Normal;
        }

        internal void OnUpgradeXRayVision()
        {
            if (CurrentSkill != SkillState.XRay)
                return;

            CurrentDistance--;

            if (CurrentDistance < XRayDistance.First)
                CurrentDistance = XRayDistance.Third;
        }
    }
}
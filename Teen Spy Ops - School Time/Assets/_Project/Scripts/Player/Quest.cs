using UnityEngine;
using Manager;
using System.Collections.Generic;

namespace Player
{
    public class Quest : MonoBehaviour
    {
        [field: SerializeField] public bool OverTimeA { get; private set; }
        [field: SerializeField] public bool OverTimeB { get; private set; }
        [field: SerializeField] public bool OverThreeAttempts { get; private set; }
        [field: SerializeField] public bool OverOneAttempt { get; private set; }

        [field: SerializeField] public int TimeLimitA { get; private set; }
        [field: SerializeField] public int TimeLimitB { get; private set; }
        
        [field: SerializeField] public int CurrentErrorNumbers { get; private set; }
        [field: SerializeField] public int CurrentNumberStars { get; private set; }
        [field: SerializeField] public float CurrentQuestTime { get; private set; }

        private void Start()
        {
            CurrentNumberStars = 5;
            CurrentErrorNumbers = 0;
        }

        internal void OnLosedGame() => CurrentNumberStars = 0;

        internal void OnCountdownPerfomed()
        {
            var levelManager = FindObjectOfType<LevelManager>();

            if (levelManager.TimerLevel < TimeLimitA && !OverTimeA)
            {
                OverTimeA = true;
                CurrentNumberStars--;
            }

            if (levelManager.TimerLevel < TimeLimitB && !OverTimeB)
            {
                OverTimeB = true;
                CurrentNumberStars--;
            }

        }

        internal void OnChosenIncorrect()
        {
            CurrentErrorNumbers++;

            if (CurrentErrorNumbers >= 4 && !OverThreeAttempts)
            {
                OverThreeAttempts = true;
                CurrentNumberStars--;
            }

            if(CurrentErrorNumbers > 0 && !OverOneAttempt)
            {
                OverOneAttempt = true;
                CurrentNumberStars--;
            }
        }

    }
}
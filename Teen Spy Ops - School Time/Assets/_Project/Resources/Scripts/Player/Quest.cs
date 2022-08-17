using UnityEngine;
using Manager;

namespace Player
{
    public class Quest : MonoBehaviour
    {
        public bool OverTimeA { get; private set; }
        public bool OverTimeB { get; private set; }
        public bool OverThreeAttempts { get; private set; }
        public bool OverOneAttempt { get; private set; }

        [field: SerializeField] public int TimeLimitA { get; private set; }
        [field: SerializeField] public int TimeLimitB { get; private set; }

        public int CurrentErrorNumbers { get; private set; }
        public int CurrentNumberStars { get; private set; }

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

            if (CurrentErrorNumbers > 0 && !OverOneAttempt)
            {
                OverOneAttempt = true;
                CurrentNumberStars--;
            }
        }

    }
}
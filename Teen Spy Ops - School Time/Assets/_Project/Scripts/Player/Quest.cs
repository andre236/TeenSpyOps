using UnityEngine;
using Manager;

namespace Player
{
    public class Quest : MonoBehaviour
    {
        public int CurrentErrorNumbers { get; private set; }
        public int CurrentNumberStars { get; private set; }
        public float CurrentQuestTime { get; private set; }

        private void Start()
        {
            CurrentNumberStars = 5;
            CurrentErrorNumbers = 0;
        }

        internal void OnDecreasedStar()
        {
            CurrentNumberStars--;

            if (CurrentNumberStars <= 0)
                CurrentNumberStars = 0;
        }

        internal void OnCountdownPerfomed()
        {
            var levelManager = FindObjectOfType<LevelManager>();
            
            if (levelManager.TimerLevel < 180)
                CurrentNumberStars--;
            

        }

        internal void OnChosenIncorrect()
        {
            CurrentErrorNumbers++;
        }

    }
}
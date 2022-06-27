using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public GameState CurrentState { get; private set; }
        [SerializeField] public SkillState CurrentSkill { get; private set; }

        [field:SerializeField] public float TimerLevel { get ; private set; }

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

        public void OnInitializedLevel()
        {
            CurrentState = GameState.Running;
            CurrentSkill = SkillState.Normal;
        }

        public void OnPausedGame() => CurrentState = GameState.Paused;

        public void OnUnPausedGame() => CurrentState = GameState.Running;

        public void OnActivedXRay() => CurrentSkill = SkillState.XRay;

    }
}
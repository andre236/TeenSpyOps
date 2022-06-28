using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public GameState CurrentState { get; private set; }
        [SerializeField] public SkillState CurrentSkill { get; private set; }
        [SerializeField] public XRayDistance CurrentDistance { get; private set; }

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
            CurrentDistance = XRayDistance.First;
        }

        internal void OnPausedGame() => CurrentState = GameState.Paused;

        internal void OnUnPausedGame() => CurrentState = GameState.Running;

        internal void OnActivedXRay()
        {
            CurrentSkill = SkillState.XRay;

            if (CurrentDistance == XRayDistance.First)
            {
                CurrentDistance = XRayDistance.Third;
            }
            else
            {
                CurrentDistance--;
            }
        }
    }
}
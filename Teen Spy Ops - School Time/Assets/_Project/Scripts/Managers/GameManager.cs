using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _timerLevel;



        public Action CountdownPerfomed;



        public GameState CurrentState;

        public float TimerLevel { get => _timerLevel; set => _timerLevel = value; }

        private void Awake()
        {
            CountdownPerfomed += CountdownTimerLevel;
        }


        private void CountdownTimerLevel()
        {
            if (CurrentState != GameState.Running)
                return;

            if (_timerLevel > 0)
            {
                _timerLevel -= Time.deltaTime;
            }
            else
            {
                _timerLevel = 0;
                CurrentState = GameState.None;
            }

        }

        public void OnInitializedLevel()
        {
            CurrentState = GameState.Running;
        }

        public void OnPausedGame()
        {
            CurrentState = GameState.Paused;
        }

        public void OnUnPausedGame()
        {
            CurrentState = GameState.Running;
        }



    }
}
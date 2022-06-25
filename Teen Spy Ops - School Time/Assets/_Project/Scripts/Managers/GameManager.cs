using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _timerLevel;
        
        private GameObject _xRayScene;
        private GameObject _nightVisionScene;

        public Action CountdownPerfomed;
        public Action<bool> PausedGame;

        public GameState CurrentState;

        public float TimerLevel { get => _timerLevel; set => _timerLevel = value; }

        private void Awake()
        {
            CountdownPerfomed += CountdownTimerLevel;
            _xRayScene = GameObject.FindGameObjectWithTag("XRay");
            _nightVisionScene = GameObject.FindGameObjectWithTag("NightVision");
        }

        private void Start()
        {
            CurrentState = GameState.Running;
        }

        private void CountdownTimerLevel()
        {
            if (CurrentState != GameState.Running)
                return;

            if(_timerLevel > 0)
            {
                _timerLevel -= Time.deltaTime;
            }
            else
            {
                _timerLevel = 0;
                CurrentState = GameState.None;
            }

            Debug.Log(_timerLevel.ToString());
        }

        public void PauseGame()
        {
            if (CurrentState != GameState.Paused)
            {
                CurrentState = GameState.Running;
                PausedGame?.Invoke(false);
            }
            else
            {
                CurrentState = GameState.Paused;
                PausedGame?.Invoke(true);
            }
        }


    }
}
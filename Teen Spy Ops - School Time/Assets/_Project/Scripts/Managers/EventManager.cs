using UnityEngine;
using Player;
using System;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {

        private GameManager _gameManager;
        private SceneryManager _sceneryManager;
        private UIManager _uiManager;
        private Skills _skills;

        public Action InitializedGame;
        
        public Action PausedGame;
        public Action UnPausedGame;

        public Action ActivedXRay;

        private void Awake()
        {
            // -- Managers -- //
            _gameManager = FindObjectOfType<GameManager>();
            _uiManager = FindObjectOfType<UIManager>();
            _sceneryManager = FindObjectOfType<SceneryManager>();

            // -- Player -- //
            _skills = FindObjectOfType<Skills>();
        }

        private void Start()
        {

            // -- Events -- //
            InitializedGame += _gameManager.OnInitializedLevel;
            InitializedGame += _uiManager.OnInitializedLevel;

            PausedGame += _gameManager.OnPausedGame;
            PausedGame += _uiManager.OnPausedGame;

            UnPausedGame += _gameManager.OnUnPausedGame;
            UnPausedGame += _uiManager.OnUnPausedGame;

            ActivedXRay += _sceneryManager.OnActivedXRay;
            ActivedXRay += _skills.OnActivedXRay;


            InitializedGame?.Invoke();
        }

        private void Update()
        {
            _gameManager.CountdownPerfomed?.Invoke();

            _uiManager.OnCountdownPerfomed(_gameManager.TimerLevel);
        }

        // -- Reference in buttons -- //
        public void OnActivedXRay()
        {
            ActivedXRay?.Invoke();
        }

        public void OnPausedGame() => PausedGame?.Invoke();

        public void OnUnPausedGame() => UnPausedGame?.Invoke();
    }
}
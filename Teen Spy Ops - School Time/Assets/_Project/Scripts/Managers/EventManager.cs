using System.Collections.Generic;
using UnityEngine;
using Player;
using System;
using Objects;
using Controllers;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private SceneryManager _sceneryManager;
        private UIManager _uiManager;
        private LevelManager _levelManager;

        private GuessController _guessController;
        private Skills _skills;

        // -- System -- //
        public Action InitializedGame;
        public Action CountdownPerfomed;

        public Action PausedGame;
        public Action UnPausedGame;

        public Action<string> ItemCollected;

        // -- Player Skills -- //
        public Action ActivedXRay;

        // -- Items -- //
        public int AmountItems { get; private set; }
         
        private void Awake()
        {
            // -- Managers -- //
            _gameManager = FindObjectOfType<GameManager>();
            _uiManager = FindObjectOfType<UIManager>();
            _sceneryManager = FindObjectOfType<SceneryManager>();
            _levelManager = FindObjectOfType<LevelManager>();

            // -- Controllers -- //
            _guessController = FindObjectOfType<GuessController>();

            // -- Player -- //
            _skills = FindObjectOfType<Skills>();

        }

        private void Start()
        {
            // -- Events -- //
            InitializedGame += _gameManager.OnInitializedLevel;
            InitializedGame += _uiManager.OnInitializedLevel;
            InitializedGame += _skills.OnInitializedLevel;
            InitializedGame += _levelManager.OnInitializedLevel;
            InitializedGame += _sceneryManager.OnInitializedLevel;

            CountdownPerfomed += _gameManager.OnCountdownTimerLevel;

            PausedGame += _gameManager.OnPausedGame;
            PausedGame += _uiManager.OnPausedGame;

            UnPausedGame += _gameManager.OnUnPausedGame;
            UnPausedGame += _uiManager.OnUnPausedGame;

            ActivedXRay += _gameManager.OnActivedXRay;
            ActivedXRay += _sceneryManager.OnActivedXRay;
            ActivedXRay += _skills.OnActivedXRay;

            foreach(Collectable coll in _levelManager.ItemsCollectable)
            {
                coll.Collected += _levelManager.OnCollected;

                coll.GotQuestion += _uiManager.OnGotQuestion;
                
                ActivedXRay += coll.OnActivatedXray;
            }
            
            InitializedGame?.Invoke();
        }

        private void Update()
        {
            CountdownPerfomed?.Invoke();

            _uiManager.ShowCountdownPerfomedText(_gameManager.TimerLevel);
            _uiManager.ShowAmoutItemsLeft(_levelManager.ItemsLeft);
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
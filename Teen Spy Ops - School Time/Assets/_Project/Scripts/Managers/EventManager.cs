using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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


        // -- Player Skills -- //
        public Action ActivedXRay;
        public Action ActivedFingerprint;
        public Action ActivedNightVision;

        // -- Items -- //
        public int AmountItems { get; private set; }
        public Action ItemCollected;

        public Action ChosenCorrect;
        public Action ChosenIncorrect;

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

            ActivedFingerprint += _gameManager.OnActivedFingerprint;
            ActivedFingerprint += _sceneryManager.OnActivedFingerprint;
            ActivedFingerprint += _skills.OnActivedFingerprint;

            ActivedNightVision += _gameManager.OnActivedNightVision;
            ActivedNightVision += _sceneryManager.OnActivedNightVision;
            ActivedNightVision += _skills.OnActivedNightVision;

            ChosenIncorrect += _guessController.OnChosenIncorrect;
            ChosenIncorrect += _uiManager.OnChosenIncorrect;

            ItemCollected += _levelManager.OnCollected;
            ItemCollected += _uiManager.OnCollected;

            foreach (Collectable coll in _levelManager.ItemsCollectable)
            {
                coll.GotQuestion += _uiManager.OnGotQuestion;
                coll.CheckedItemOnList += _levelManager.OnCheckedItemOnList;

                ActivedXRay += coll.OnActivatedXray;
                ActivedNightVision += coll.OnActivedNightVision;
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
        public void OnActivedXRay() => ActivedXRay?.Invoke();
        public void OnActivedNightVision()
        {
            ActivedNightVision?.Invoke();
        }
        public void OnActivedFingerprint() => ActivedFingerprint?.Invoke();
        public void OnChosenCorrect() => ItemCollected?.Invoke();

        public void OnChosenIncorrect() => ChosenIncorrect?.Invoke();

        public void OnPausedGame() => PausedGame?.Invoke();

        public void OnUnPausedGame() => UnPausedGame?.Invoke();
    }
}
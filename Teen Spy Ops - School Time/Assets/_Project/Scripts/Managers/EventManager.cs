using UnityEngine;
using Player;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {

        private GameManager _gameManager;
        private UIManager _uiManager;
        private Skills _skills;

        private void Awake()
        {
            // -- Managers -- //
            _gameManager = FindObjectOfType<GameManager>();
            _uiManager = FindObjectOfType<UIManager>();

            // -- Player -- //
            _skills = FindObjectOfType<Skills>();
        }

        private void Start()
        {
            // -- Managers -- //
            _gameManager.PausedGame += _uiManager.OnPausedGame; 
        }

        private void Update()
        {
            _gameManager.CountdownPerfomed?.Invoke();

            _uiManager.OnCountdownPerfomed(_gameManager.TimerLevel);
        }

    }
}
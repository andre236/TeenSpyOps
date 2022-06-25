using System;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private Text _timerLevelText;
        
        private GameObject _pausePage;
        private Button _menuButton;


        private void Awake()
        {
            _pausePage = GameObject.Find("PausePage");
            _menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
            _timerLevelText = GameObject.Find("TimerText").GetComponent<Text>();    
        }

        private void Start()
        {
            _pausePage.SetActive(false);
        }

        public void OnCountdownPerfomed(float currentTime)
        {
            string minSec = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
            _timerLevelText.text = minSec;
        }

        public void OnPausedGame(bool isPaused)
        {
            _pausePage.SetActive(isPaused);
        }
    }
}
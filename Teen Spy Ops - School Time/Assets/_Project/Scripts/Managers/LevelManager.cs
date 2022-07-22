using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine;
using Objects;
using System;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] private GameObject _prefabSchoolObject;
        [SerializeField] private GameObject[] _schoolObjects;

        [field: SerializeField] public float InitialTimerLevel { get; private set; }
        public float TimerLevel { get; private set; }
        public int ItemsLeft { get; private set; }
        public List<Collectable> ItemsCollectable { get; private set; }
        public GameObject CurrentObject { get; private set; }


        public Action StoppedTimer;

        private void Awake()
        {
            _schoolObjects = GameObject.FindGameObjectsWithTag("RespawnObject");
            
            for(int i = 0; i < 3; i++)
            {
                Instantiate(_prefabSchoolObject, _schoolObjects[UnityEngine.Random.Range(0, _schoolObjects.Length - 1)].transform);
            }

            ItemsCollectable = new List<Collectable>();
            ItemsCollectable.AddRange(FindObjectsOfType<Collectable>());
            
        }

        internal void OnInitializedLevel()
        {
            TimerLevel = InitialTimerLevel;
            ItemsLeft = ItemsCollectable.Count;
        }

        internal void OnCountdownTimerLevel()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentGameState != GameState.Running)
                return;

            if (TimerLevel > 0)
            {
                TimerLevel -= Time.deltaTime;
            }
            else
            {
                TimerLevel = 0;
                StoppedTimer?.Invoke();
            }

        }

        internal void OnCollected()
        {
            CurrentObject.GetComponent<Collectable>().GotQuestion = null;

            ItemsCollectable.Remove(CurrentObject.GetComponent<Collectable>());
            Destroy(CurrentObject);
            ItemsLeft = ItemsCollectable.Count;
        }

        internal void OnCheckedItemOnList(GameObject collectableObject) => CurrentObject = collectableObject;

        internal void OnEarnedStars(int amountStars)
        {
            if (PlayerPrefs.GetInt(string.Concat("STARS", SceneManager.GetActiveScene().name)) < amountStars)
                PlayerPrefs.SetInt(string.Concat("STARS", SceneManager.GetActiveScene().name), amountStars);

            
        }

        internal void OnWonGame()
        {
            string nameLevel = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            int numberNextLevel = int.Parse(nameLevel) + 1;

            PlayerPrefs.SetInt("LEVEL" + numberNextLevel, 1);

        }
    }
}
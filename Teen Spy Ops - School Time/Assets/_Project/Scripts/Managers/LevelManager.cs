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
        [SerializeField] private GameObject[] _spawnSchoolObject;
        [SerializeField] private string[] _allowedSchoolObjects;

        private int _amountInstantiatedSchoolObjects;

        [field: SerializeField] public float InitialTimerLevel { get; private set; }
        public float TimerLevel { get; private set; }
        public int ItemsLeft { get; private set; }
        public List<Collectable> ItemsCollectable = new List<Collectable>();
        public GameObject CurrentObject { get; private set; }

        public Action StoppedTimer;

        private void Awake()
        {
            _spawnSchoolObject = GameObject.FindGameObjectsWithTag("RespawnObject");

            
            ItemsCollectable.AddRange(FindObjectsOfType<Collectable>());            
        }

        internal void OnInitializedLevel()
        {
            int randomIndexObject = UnityEngine.Random.Range(0, _spawnSchoolObject.Length - 1);
            
            for (int i = 0; i < 100; i++)
            {
                if(_spawnSchoolObject[randomIndexObject].transform.childCount == 0 && _amountInstantiatedSchoolObjects < 3)
                {
                    Instantiate(_prefabSchoolObject, _spawnSchoolObject[randomIndexObject].transform);
                    _amountInstantiatedSchoolObjects++;
                }

                randomIndexObject = UnityEngine.Random.Range(0, _spawnSchoolObject.Length - 1);

                if (_amountInstantiatedSchoolObjects >= 3)
                    break;
            }

            //string nameLevel = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            //int numberCurrentLevel = int.Parse(nameLevel);

            //for (int i = 0; i < 3; i++)
            //{
            //    if (PlayerPrefs.GetString("Item_" + i + "_LEVEL" + numberCurrentLevel) == null)
            //        PlayerPrefs.SetString("Item_" + i + "_LEVEL" + numberCurrentLevel, _allowedSchoolObjects[i]);
            //}



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
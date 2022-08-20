using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using Objects;
using Tutorial;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] protected GameObject _prefabSchoolObject;
        [SerializeField] private string[] _allowedSchoolObjects;

        [SerializeField] protected GameObject[] _spawnSchoolObject;

        protected int _amountInstantiatedSchoolObjects;

        [field:Header("Timer level in seconds.")]
        [field: SerializeField] public virtual float InitialTimerLevel { get; private set; }
        public virtual float TimerLevel { get; protected set; }
        public GameObject CurrentObject { get; private set; }
        public virtual int ItemsLeft { get; protected set; }
        public string[] AllowedSchoolObjects { get => _allowedSchoolObjects; set => _allowedSchoolObjects = value; }

        public List<Collectable> ItemsCollectable = new List<Collectable>();

        public Action StoppedTimer;

        protected virtual void Awake()
        {
            _spawnSchoolObject = GameObject.FindGameObjectsWithTag("RespawnObject");
            CheckObjectsPermission();
        }

        internal virtual void OnInitializedLevel()
        {

            int randomIndexObject = UnityEngine.Random.Range(0, _spawnSchoolObject.Length - 1);

            for (int i = 0; i < 100; i++)
            {
                if (_spawnSchoolObject[randomIndexObject].transform.childCount == 0 && _amountInstantiatedSchoolObjects < 3)
                {
                    Instantiate(_prefabSchoolObject, _spawnSchoolObject[randomIndexObject].transform);
                    _amountInstantiatedSchoolObjects++;
                }

                randomIndexObject = UnityEngine.Random.Range(0, _spawnSchoolObject.Length - 1);

                if (_amountInstantiatedSchoolObjects >= 3)
                    break;
            }

            ItemsCollectable.AddRange(FindObjectsOfType<Collectable>());
            TimerLevel = InitialTimerLevel;
            ItemsLeft = ItemsCollectable.Count;
        }

        protected virtual void CheckObjectsPermission()
        {
            if (FindObjectOfType<TutorialLevelManager>() != null)
                return;

            string nameLevel = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            int numberCurrentLevel = int.Parse(nameLevel);
            AllowedSchoolObjects = new string[3];

            for (int numberPhase = 0; numberPhase < 8; numberPhase++)
            {
                if (numberPhase == numberCurrentLevel)
                    for (int items = 0; items < 3; items++)
                    {
                        AllowedSchoolObjects[items] = PlayerPrefs.GetString("LEVEL" + numberCurrentLevel + "_ITEMINDEX_" + (items + (numberPhase * 3)) + "_ITEMPOSITION" + items).Replace(".asset", "");
                    }
            }

        }

        internal virtual void OnCountdownTimerLevel()
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

        internal virtual void OnCollected()
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
            if (SceneManager.GetActiveScene().name == "TUTORIAL")
                return;
            
            string nameLevel = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            int numberNextLevel = int.Parse(nameLevel) + 1;

            PlayerPrefs.SetInt("LEVEL" + numberNextLevel, 1);
        }

     
    }
}
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Manager;

namespace Tutorial
{
    public class TutorialLevelManager : LevelManager
    {
        [SerializeField] private ItemConfig[] _itemTutorial;

        public ItemConfig[] ItemTutorial { get => _itemTutorial; set => _itemTutorial = value; }

        protected override void Awake()
        {
            _spawnSchoolObject = GameObject.FindGameObjectsWithTag("RespawnObject");
            CheckObjectsPermission();
        }

        internal override void OnInitializedLevel()
        {
            Debug.Log("Override ");
        }

        protected override void CheckObjectsPermission()
        {
            
            AllowedSchoolObjects = new string[3];

            for(int i = 0; i < AllowedSchoolObjects.Length; i++)
                AllowedSchoolObjects[i] = ItemTutorial[i].NameObject;
        }

        internal override void OnCountdownTimerLevel()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentGameState != GameState.Running)
                return;


            if (TimerLevel > 0 || GameObject.Find("TinaPageTutorial") == null)
            {
                TimerLevel -= Time.deltaTime;
            }
            else
            {
                TimerLevel = 0;
                StoppedTimer?.Invoke();
            }

        }

    }
}
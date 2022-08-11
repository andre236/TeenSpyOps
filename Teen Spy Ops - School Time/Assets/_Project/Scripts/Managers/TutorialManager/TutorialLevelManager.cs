using UnityEngine;
using Manager;
using Objects;

namespace Tutorial
{
    public class TutorialLevelManager : LevelManager
    {
        [SerializeField] private ItemConfig[] _itemTutorial;

        public ItemConfig[] ItemTutorial { get => _itemTutorial; set => _itemTutorial = value; }

        protected override void Awake()
        {
            base.Awake();
        }

        internal override void OnInitializedLevel()
        {

            base.OnInitializedLevel();
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
using System.Collections.Generic;
using UnityEngine;
using Objects;
using System;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [field: SerializeField] public float InitialTimerLevel { get; private set; }
        public float TimerLevel { get; private set; }
        [SerializeField] public List<Collectable> ItemsCollectable = new List<Collectable>();
        public int ItemsLeft { get; private set; }
        [field:SerializeField] public GameObject CurrentObject { get; private set; }

        public Action StoppedTimer;

        private void Awake() => ItemsCollectable.AddRange(FindObjectsOfType<Collectable>());

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
            throw new NotImplementedException();
        }
    }
}
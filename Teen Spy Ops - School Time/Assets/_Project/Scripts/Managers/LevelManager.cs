using System.Collections.Generic;
using UnityEngine;
using Objects;
using System;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] public List<Collectable> ItemsCollectable = new List<Collectable>();
        public int ItemsLeft { get; private set; }
        [field:SerializeField] public GameObject CurrentObject { get; private set; }

        private void Awake() => ItemsCollectable.AddRange(FindObjectsOfType<Collectable>());

        internal void OnInitializedLevel() => ItemsLeft = ItemsCollectable.Count;

        internal void OnCollected()
        {
            ItemsCollectable.Remove(CurrentObject.GetComponent<Collectable>());
            Destroy(CurrentObject);
            ItemsLeft = ItemsCollectable.Count;
        }

        internal void OnCheckedItemOnList(GameObject collectableObject) => CurrentObject = collectableObject;
    }
}
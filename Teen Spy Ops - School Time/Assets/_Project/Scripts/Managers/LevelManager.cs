using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] public List<Collectable> ItemsCollectable = new List<Collectable>();
        public int ItemsLeft { get; private set; }

        private void Awake()
        {
            ItemsCollectable.AddRange(FindObjectsOfType<Collectable>());
        }

        internal void OnInitializedLevel()
        {
            ItemsLeft = ItemsCollectable.Count; 
        }

        internal void OnCollected(Collectable collectable)
        {
            ItemsCollectable.Remove(collectable);
            ItemsLeft = ItemsCollectable.Count;
        }

    }
}
using System.Collections;
using UnityEngine;

namespace Manager
{
    public class EventMenuManager : EventManager
    {
        private MenuUIManager _menuUIManager;

        protected override void Awake() => _menuUIManager = FindObjectOfType<MenuUIManager>();
        protected override void Update() { }
        protected override void Start()
        {
            PlayerPrefs.DeleteAll();
            LoadedNextScene += _menuUIManager.OnLoadedNextScene;
        }

        internal override void LoadNextLevelScene() => base.LoadNextLevelScene();

        internal override IEnumerator GetLoadingNextScene()
        {
            return base.GetLoadingNextScene();
        }
    }
}
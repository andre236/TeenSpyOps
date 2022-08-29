using System.Collections;

namespace Manager
{
    public class EventMenuManager : EventManager
    {
        private MenuUIManager _menuUIManager;

        protected override void Awake() => _menuUIManager = FindObjectOfType<MenuUIManager>();
        protected override void Update() { }
        protected override void Start() => LoadedNextScene += _menuUIManager.OnLoadedNextScene;

        internal override void LoadNextLevelScene() => base.LoadNextLevelScene();

        internal override IEnumerator GetLoadingNextScene()
        {
            return base.GetLoadingNextScene();
        }
    }
}
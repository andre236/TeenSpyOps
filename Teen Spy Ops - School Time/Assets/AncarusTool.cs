using UnityEditor;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace ancarustool
{
    public static class AncarusTool
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateFolders()
        {
            Dir("_Project", "Scripts", "Scenes", "Scenes/Done", "Scenes/Creating", "Animatios", "Images", "Images/HUD","Images/Backgrounds", "Images/Tileset", "Scripts", "Scripts/Managers", "Audio", "Audio/BGM", "Audio/SFX", "Fonts");
            Refresh();
        }

        public static void Dir(string root, params string[] dir)
        {
            var fullPath = Combine(dataPath, root);
            foreach (var newDirectory in dir)
                CreateDirectory(Combine(fullPath, newDirectory));
        }
    }
}
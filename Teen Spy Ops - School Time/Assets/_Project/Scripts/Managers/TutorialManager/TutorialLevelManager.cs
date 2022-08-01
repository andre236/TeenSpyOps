using Manager;
using System.IO;

namespace Tutorial
{
    public class TutorialLevelManager : LevelManager
    {

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void CheckObjectsPermission()
        {
            DirectoryInfo directory = new DirectoryInfo("Assets/_Project/Scripts/ScriptableObject/SchoolObjects");
            FileInfo[] filesInfo = directory.GetFiles("*.asset");


        }
    }
}
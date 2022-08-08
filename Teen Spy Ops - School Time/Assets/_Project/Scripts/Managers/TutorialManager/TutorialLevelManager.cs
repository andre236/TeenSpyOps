using Manager;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

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

        protected override void CheckObjectsPermission()
        {
            AllowedSchoolObjects = new string[3];

            for(int i = 0; i < AllowedSchoolObjects.Length; i++)
                AllowedSchoolObjects[i] = ItemTutorial[i].NameObject;
        }

    }
}
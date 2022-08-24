using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Statics
{
    public class GeneralTexts : MonoBehaviour
    {
        [SerializeField] private string[] _nameObjects;
        [SerializeField] private string[] _tinaLinesTutorial;

        public string[] NameObjects { get => _nameObjects; set => _nameObjects = value; }

        [System.Serializable]
        public class TinaSectionLinesTutorial
        {
            public string[] TinaLines;
        }

        [System.Serializable]
        public class HintsPhase
        {
            public HintsString[] RespawnHint;

            [System.Serializable]
            public class HintsString
            {
                public string[] Hint;
            }
        }

        public List<HintsPhase> HintsPerPhaseList;
        public List<TinaSectionLinesTutorial> TinaSectionLinesTutorialsList;
        public List<TinaSectionLinesTutorial> TestSaveData;
        
        public static GeneralTexts Instance { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

        }

        private void Start()
        {
            GetSchoolObjectsName();
            
        }

        private void GetSchoolObjectsName()
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/_Project/Resources/Scripts/ScriptableObject/SchoolObjects");

            FileInfo[] info = dir.GetFiles("*.asset");

            NameObjects = new string[info.Length];

            for (int i = 0; i < info.Length; i++)
            {
                NameObjects[i] = info[i].Name.Replace(".asset", "");
            }


        }

        private void SetHintsEachPhase()
        {

        }
    }
}
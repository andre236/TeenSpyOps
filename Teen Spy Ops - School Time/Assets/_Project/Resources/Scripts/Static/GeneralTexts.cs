using System.Collections.Generic;
using System.IO;
using UnityEngine;
using JsonsUnip;
using System.Collections;

namespace Statics
{
    public class GeneralTexts : MonoBehaviour
    {
        [SerializeField] private string[] _nameObjects;
        [SerializeField] private string _schoolObjectsName;

        
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

        public string SchoolObjectsName { get => _schoolObjectsName; set => _schoolObjectsName = value; }
        public static string[,,] TinaLinesTutorial { get; internal set; }

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
           
            StartCoroutine(TesteDelay());
        }


        private void GetTinaLinesTutorial()
        {


            for (int sectionLines = 0; sectionLines < TinaLinesTutorial.GetLength(0); sectionLines++)
            {

                for (int indexTinaLine = 0; indexTinaLine < TinaLinesTutorial.GetLength(1); indexTinaLine++)
                {
                    if(TinaLinesTutorial[sectionLines, indexTinaLine, 0] != "" && TinaLinesTutorial[sectionLines, indexTinaLine, 0] != null)
                    {
                        TinaSectionLinesTutorialsList[sectionLines].TinaLines[indexTinaLine] = TinaLinesTutorial[sectionLines, indexTinaLine, 0];
                    }
                }
            }
            

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
        IEnumerator TesteDelay()
        {
            yield return new WaitForSeconds(0.5f);
            //TinaSectionLinesTutorialsList[0].TinaLines[0] = TinaLinesTutorial[0, 0, 0];
            //Debug.Log(TinaSectionLinesTutorialsList[0].TinaLines[0]);
            GetTinaLinesTutorial();
        }
        private void SetHintsEachPhase()
        {

        }
    }
}
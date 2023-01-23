using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

namespace Statics
{
    public class GeneralTexts : MonoBehaviour
    {
        [SerializeField] private bool _onTesting;

        [SerializeField] private string[] _nameObjects;


        public string[] NameObjects { get => _nameObjects; set => _nameObjects = value; }

        [Serializable]
        public class TinaSectionLinesTutorial
        {
            public string[] TinaLines;
        }

        [Serializable]
        public class HintsPhase
        {
            public HintsString[] RespawnHint;

            [System.Serializable]
            public class HintsString
            {
                public string[] Hint;
            }
        }

        [Serializable]
        public class SectionsCutsceneLines
        {
            public string NameArray;
            public string[] CutsceneLines;
        }

        public string[] SectionInnitialCutsceneLinesList;
        public string[] SectionFinalCutsceneLinesList;


        public List<HintsPhase> HintsPerPhaseList;
        public List<TinaSectionLinesTutorial> TinaSectionLinesTutorialsList;

        public static GeneralTexts Instance { get; set; }

        public static string[,,] SchoolObjectsNameFromJSON { get; internal set; }
        public static string[,,] TinaLinesTutorial { get; internal set; }
        public static string[,,] HintsFromJSON { get; internal set; }

        public static string[,,] InnitialCutscene { get; internal set; }
        public static string[,,] FinalCutscene { get; internal set; }


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
            if (_onTesting)
                return;

            StartCoroutine(TesteDelay());
        }

        private void GetHints()
        {
            int limitNumberHints = 2;
            int currentNumberHint = 0;

            for (int phase = 0; phase < HintsFromJSON.GetLength(0); phase++)
            {
                for (int respawn = 0; respawn < 5; respawn++)
                {
                    for (int numberHint = 0; numberHint < limitNumberHints; numberHint++)
                    {
                        HintsPerPhaseList[phase].RespawnHint[respawn].Hint[numberHint] = HintsFromJSON[phase, currentNumberHint, 0];
                        //HintsPerPhaseList[phase + 3].RespawnHint[respawn].Hint[numberHint] = HintsFromJSON[phase, currentNumberHint, 0];
                        if (currentNumberHint < HintsFromJSON.GetLength(1) - 1)
                            currentNumberHint++;
                        else
                            currentNumberHint = 0;

                    }

                }
            }

        }

        private void GetTinaLinesTutorial()
        {


            for (int sectionLines = 0; sectionLines < TinaLinesTutorial.GetLength(0); sectionLines++)
            {

                for (int indexTinaLine = 0; indexTinaLine < TinaLinesTutorial.GetLength(1); indexTinaLine++)
                {
                    if (TinaLinesTutorial[sectionLines, indexTinaLine, 0] != "" && TinaLinesTutorial[sectionLines, indexTinaLine, 0] != null)
                        TinaSectionLinesTutorialsList[sectionLines].TinaLines[indexTinaLine] = TinaLinesTutorial[sectionLines, indexTinaLine, 0];

                }
            }


        }

        private void GetSchoolObjectsName()
        {
            ItemConfig[] allSchoolObjects = Resources.LoadAll<ItemConfig>("Scripts/ScriptableObject/SchoolObjects");

            _nameObjects = new string[allSchoolObjects.Length];

            for (int indexSchoolObject = 0; indexSchoolObject < SchoolObjectsNameFromJSON.Length; indexSchoolObject++)
            {
                NameObjects[indexSchoolObject] = SchoolObjectsNameFromJSON[0, indexSchoolObject, 0];
            }

            for (int i = 0; i < allSchoolObjects.Length; i++)
            {
                allSchoolObjects[i].NameObject = NameObjects[i];
            }
        }

        IEnumerator TesteDelay()
        {

            yield return new WaitForSeconds(0.5f);
            GetSchoolObjectsName();
            GetTinaLinesTutorial();
            GetAllCutsceneLines();
            GetHints();
        }

        private void GetAllCutsceneLines()
        {
            SectionInnitialCutsceneLinesList = new string[InnitialCutscene.GetLength(1)];
            SectionFinalCutsceneLinesList = new string[FinalCutscene.GetLength(1)];

            for (int cutsceneLineIndex = 0; cutsceneLineIndex < InnitialCutscene.GetLength(1); cutsceneLineIndex++)
                SectionInnitialCutsceneLinesList[cutsceneLineIndex] = InnitialCutscene[0, cutsceneLineIndex, 0];

            for (int cutsceneLineIndex = 0; cutsceneLineIndex < FinalCutscene.GetLength(1); cutsceneLineIndex++)
                SectionFinalCutsceneLinesList[cutsceneLineIndex]  = FinalCutscene[0, cutsceneLineIndex, 0];

        }
    }
}
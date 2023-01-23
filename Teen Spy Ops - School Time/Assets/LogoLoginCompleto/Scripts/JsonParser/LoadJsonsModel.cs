using System.IO;
using UnityEngine;
using Statics;

namespace JsonsUnip
{
    [RequireComponent(typeof(JsonParserWeb))]
    public class LoadJsonsModel : MonoBehaviour
    {
        [HideInInspector] public string[] Urls;
        [HideInInspector] public string CurrentUrl;

        private void Awake()
        {
            PlatformVerification();
            Urls = new string[9];
            Urls[0] = CurrentUrl + "/AchievementsInfo.json";
            Urls[1] = CurrentUrl + "/AchievementsName.json";
            Urls[2] = CurrentUrl + "/HintsPerPhase.json";
            Urls[3] = CurrentUrl + "/SchoolObjects.json";
            Urls[4] = CurrentUrl + "/TinaLinesTutorial.json";
            Urls[5] = CurrentUrl + "/InnitialCutscene.json";
            Urls[6] = CurrentUrl + "/FinalCutscene.json";


        }
        private void Start()
        {
            //Aqui você vai chamar a função para pegar todos os jsons que serão utilizados no jogo
            JsonParserWeb.instance.JsonStringReturn(Urls[0], nameof(SetAchievementsInfoJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[1], nameof(SetAchievementsNameJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[2], nameof(SetHintsPerPhaseJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[3], nameof(SetSchoolObjectsJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[4], nameof(SetTinaLinesTutorialJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[5], nameof(SetInnitialCutsceneJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[6], nameof(SetFinalCutsceneJson));



        }

        #region SETJSONS
        public void SetTinaLinesTutorialJson()
        {
            GeneralTexts.TinaLinesTutorial = JsonParserWeb.instance.JStringReturnValue().data;
            //Debug.Log("TinaLinesTutorial Loaded");
            Debug.Log(nameof(GeneralTexts.TinaLinesTutorial) + " Loaded");
        }

        public void SetHintsPerPhaseJson()
        {
            GeneralTexts.HintsFromJSON = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("HintsPerPhase Loaded");
        }


        public void SetAchievementsNameJson()
        {
            Texts.AchievementNames = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("AchievementNames Loaded");
            //Debug.Log(nameof(Texts.AchievementNames)+" Loaded");

        }

        public void SetAchievementsInfoJson()
        {
            Texts.AchievementInfos = JsonParserWeb.instance.JStringReturnValue().data;
            //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
            Debug.Log("Achievements Infos loaded");
        }

        public void SetSchoolObjectsJson()
        {
            GeneralTexts.SchoolObjectsNameFromJSON = JsonParserWeb.instance.JStringReturnValue().data; //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
            Debug.Log("SchoolObjects Loaded");
        }

        public void SetpedagogicJson()
        {
            Texts.PedagogicText = JsonParserWeb.instance.JStringReturnValue().data;
            //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
            Debug.Log("pedagogic loaded");
        }

        public void SetInnitialCutsceneJson()
        {
            GeneralTexts.InnitialCutscene = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("InnitialCutscene loaded");

        }

        public void SetFinalCutsceneJson()
        {
            GeneralTexts.FinalCutscene = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("FinalCutscene loaded");

        }

        #endregion

        public void PlatformVerification()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                CurrentUrl = "https://sistemashomologacao.suafaculdade.com.br/Jogos/unity/TeenSpyOps1/?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJjYm9zc3NAZ21haWwuY29tIiwicm9sZSI6WyIzMDExMjYiLCIxIl0sIm5iZiI6MTYxODUwMDA2MCwiZXhwIjoxNjE4NTA3MjYwLCJpYXQiOjE2MTg1MDAwNjB9.cRMPMOkvoHichbYr2FnbiKALfQAi9IuGukpgVWg5kKQ";
                CurrentUrl = SplitUrl(CurrentUrl.Split('?'));
                CurrentUrl = CurrentUrl + "JSONs";

            }
            else
            {
                CurrentUrl = SplitUrl(Application.absoluteURL.Split('?'));
                CurrentUrl = CurrentUrl + "JSONs";
            }

            Debug.Log("URL atual:" + CurrentUrl);
        }

        public string SplitUrl(string[] URLt)
        {
            string newToken = URLt[0];

            return newToken;
        }


    }
}
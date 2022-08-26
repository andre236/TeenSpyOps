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
            Urls[2] = CurrentUrl + "/HintsLevel0.json";
            Urls[3] = CurrentUrl + "/HintsLevel1.json";
            Urls[4] = CurrentUrl + "/HintsLevel2.json";
            Urls[5] = CurrentUrl + "/HintsLevel3.json";
            Urls[6] = CurrentUrl + "/HintsPerPhase.json";
            Urls[7] = CurrentUrl + "/SchoolObjects.json";
            Urls[8] = CurrentUrl + "/TinaLinesTutorial.json";

        }
        private void Start()
        {
            //Aqui você vai chamar a função para pegar todos os jsons que serão utilizados no jogo
            JsonParserWeb.instance.JsonStringReturn(Urls[0], nameof(SetAchievementsInfoJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[1], nameof(SetAchievementsNameJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[2], nameof(SetHintsLevel0Json));
            JsonParserWeb.instance.JsonStringReturn(Urls[3], nameof(SetHintsLevel1Json));
            JsonParserWeb.instance.JsonStringReturn(Urls[4], nameof(SetHintsLevel2Json));
            JsonParserWeb.instance.JsonStringReturn(Urls[5], nameof(SetHintsLevel3Json));
            JsonParserWeb.instance.JsonStringReturn(Urls[6], nameof(SetHintsPerPhaseJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[7], nameof(SetSchoolObjectsJson));
            JsonParserWeb.instance.JsonStringReturn(Urls[8], nameof(SetTinaLinesTutorialJson));


        }

        #region SETJSONS
        public void SetTinaLinesTutorialJson()
        {
            GeneralTexts.TinaLinesTutorial = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("TinaLinesTutorial Loaded");
        }

        public void SetHintsPerPhaseJson()
        {
            Texts.HintsPerPhase = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("HintsPerPhase Loaded");
        }

        public void SetHintsLevel3Json()
        {
            Texts.HintsLevel3 = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("HintsLevel3 Loaded");
        }

        public void SetHintsLevel2Json()
        {
            Texts.HintsLevel2 = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("HintsLevel2 Loaded");
        }

        public void SetHintsLevel1Json()
        {
            Texts.HintsLevel1 = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("HintsLevel1 Loaded");
        }

        public void SetHintsLevel0Json()
        {
            Texts.HintsLevel0 = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("HintsLevel0 Loaded");
        }

        public void SetAchievementsNameJson()
        {
            Texts.AchievementNames = JsonParserWeb.instance.JStringReturnValue().data;
            Debug.Log("AchievementNames Loaded");
        }

        public void SetAchievementsInfoJson()
        {
            Texts.AchievementInfos = JsonParserWeb.instance.JStringReturnValue().data;
            //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
            Debug.Log("Achievements Infos loaded");
        }

        public void SetSchoolObjectsJson()
        {
            Texts.SchoolObjectsJsons = JsonParserWeb.instance.JStringReturnValue().data; //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
            Debug.Log("SchoolObjects Loaded");
        }

        public void SetpedagogicJson()
        {
            Texts.PedagogicText = JsonParserWeb.instance.JStringReturnValue().data;
            //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
            Debug.Log("pedagogic loaded");
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
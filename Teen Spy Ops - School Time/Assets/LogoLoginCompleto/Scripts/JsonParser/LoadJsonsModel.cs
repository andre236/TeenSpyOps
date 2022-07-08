using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadJsonsModel : MonoBehaviour
{
    public string[] urls;
    public string CurrentUrl;

    private void Awake()
    {
        platformVerification();
        urls = new string[4];
        urls[0] = CurrentUrl + "/Tutorial.json";
        urls[1] = CurrentUrl + "/Pedagogico.json";
        urls[2] = CurrentUrl + "/AchievementsNames.json";
        urls[3] = CurrentUrl + "/AchievementsInfos.json";
    }
    private void Start()
    {
        //Aqui você vai chamar a função para pegar todos os jsons que serão utilizados no jogo
        JsonParserWeb.instance.JsonStringReturn(urls[0], "SetTutorialJson");
        JsonParserWeb.instance.JsonStringReturn(urls[1], "SetpedagogicJson");
        JsonParserWeb.instance.JsonStringReturn(urls[2], "SetAchievementsNamesJson");
        JsonParserWeb.instance.JsonStringReturn(urls[3], "SetAchievementsInfoJson");
    }

    public void SetTutorialJson()
    {
        Texts.TexTurials = JsonParserWeb.instance.JStringReturnValue().data; //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
        Debug.Log("tutorial loaded");
    }

    public void SetpedagogicJson()
    {
        Texts.PedagogicText = JsonParserWeb.instance.JStringReturnValue().data;
        //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
        Debug.Log("pedagogic loaded");
    }

    public void SetAchievementsNamesJson()
    {
        Texts.achievementNames = JsonParserWeb.instance.JStringReturnValue().data;
        //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
        Debug.Log("Achievements Names loaded");
    }

    public void SetAchievementsInfoJson()
    {
        Texts.achievementInfos = JsonParserWeb.instance.JStringReturnValue().data;
        //Texts.setPedagogicText(JsonParserWeb.instance.JStringReturnValue().data); //apos fazer a conexão aplica o valor do json pra variavel statoca da classe Texts
        Debug.Log("Achievements Infos loaded");
    }

    public void platformVerification()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            CurrentUrl = "https://sistemashomologacao.suafaculdade.com.br/Jogos/unity/Contrix/?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImJjYm9zc3NAZ21haWwuY29tIiwicm9sZSI6WyIzMDExMjYiLCIxIl0sIm5iZiI6MTYxODUwMDA2MCwiZXhwIjoxNjE4NTA3MjYwLCJpYXQiOjE2MTg1MDAwNjB9.cRMPMOkvoHichbYr2FnbiKALfQAi9IuGukpgVWg5kKQ";
            CurrentUrl = splitUrl(CurrentUrl.Split('?'));
            CurrentUrl = CurrentUrl + "JSONs";
        }
        else
        {
            CurrentUrl = splitUrl(Application.absoluteURL.Split('?'));
            CurrentUrl = CurrentUrl + "JSONs";
        }

        Debug.Log("URL atual:" + CurrentUrl);
    }

    public string splitUrl(string[] URLt)
    {
        string newToken = URLt[0];

        return newToken;
    }
}

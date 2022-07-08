using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class XuxaApiController : MonoBehaviour
{
    private readonly string baseXuxaApi = "https://sistemashomologacao.suafaculdade.com.br/ObjetivoGames/api/";
    static string baseToken;
    //private string CurrentURL;
    private string CurrentURL;
    public ChangeScenes chanceScene;

    public Login login;

    public bool needYear = false;

    public Text copyright;

    [SerializeField]
    private string startSceneName;

    private void Start()
    {
        //verifica se o jogo nao esta sendo executado no Unity Player
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            //pega a URL atual
            CurrentURL = Application.absoluteURL;
            //Pega o token do usuario na URL
            SetBaseToken();
        }

        //pega o ano vigente
        if(needYear)
        {
            startYearCoroutine();
        }
    }

    //inicia uma Corutine para verifcar se o token e valido e se o usuario esta conectado
    public void GetToken()
    {
        StartCoroutine(GetSiteGamesAcess());
    }

    //verifcar se o token e valido
    IEnumerator GetSiteGamesAcess()
    {
        //configura o header que sera enviado para API
        string authenticationURL = baseXuxaApi + "Usuarios/authenticated";
        UnityWebRequest userHeader = UnityWebRequest.Get(authenticationURL);
        userHeader.SetRequestHeader("Content-Type", "text/plain");
        userHeader.SetRequestHeader("Authorization", "Bearer "+baseToken);

        //envia as informacoes com header configurado e espera retorno
        yield return userHeader.SendWebRequest();

        //se nao tiver conectado vai para a tela de login
        if (userHeader.result == UnityWebRequest.Result.ConnectionError || userHeader.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(userHeader.error);
            chanceScene.GoToSomeScene("Login");
            yield break;
        }
        //se tiver conectado vai direto para o jogo
        chanceScene.GoToSomeScene(startSceneName);
        Debug.Log("Funfou caraio");
    }

    //referente a tela de login
    //coemca uma coroutine que verifica se o usuario e senha estao corretos
    public void PostLogin(string user, string password)
    {
        StartCoroutine(PostLoginOnsite(user, password));
    }


    //verifica se o usuario e senha estao corretos
    IEnumerator PostLoginOnsite(string user, string password)
    {
        //monta o header que vai ser enviado e verificado na API
        string URL = "https://sistemashomologacao.suafaculdade.com.br/ObjetivoGames/api/Usuarios/Login";

        //monta um JSON com as informacoes
        JSONObject LoginSenha = new JSONObject();
        LoginSenha.Add("Email_Usuario", user);
        LoginSenha.Add("Senha_Usuario", password);

        //comeca a montar o metodo post
        var plataformRequest = new UnityWebRequest(URL, "POST");

        //tranforma o JSON criado numa string q possa ser lida pelo navegador
        byte[] bodyRaw = Encoding.UTF8.GetBytes(LoginSenha.ToString());

        //monta o header com as informacores
        plataformRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        plataformRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        plataformRequest.SetRequestHeader("Content-Type", "application/json");
        login.UserMessage.text = "Logando...";

        //Debug.Log(LoginSenha);

        //manda e espera o retorno da requisicao
        yield return plataformRequest.SendWebRequest();

        //se der erro aparece as mensagens pro usuario
        if (plataformRequest.result == UnityWebRequest.Result.ConnectionError || plataformRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(plataformRequest.error + " Agora deu ruim");
            login.UserMessage.text = "Usuário e/ou senha incorretos.";
            yield break;
        }

        //se der certo pega o token do usuario para poder ser utilizado depois
        JSONNode plataformInfo = JSON.Parse(plataformRequest.downloadHandler.text);
        baseToken = plataformInfo["token"];
        //Debug.Log("Meu token " + baseToken);

        login.NextScene();
    }

    //metodo para adicionar conquistas
    //insira o id da conquista quando chama la
    //como ela e estatica basta chamala quando a condicao da donquista for realizada
    //exemplo XuxaApiController.AddAchievement("46")
    public static void AddAchievement(string idConquista)
    {
        string URL = "https://sistemashomologacao.suafaculdade.com.br/ObjetivoGames/api/UsuarioConquista";

        JSONObject LoginSenha = new JSONObject();
        LoginSenha.Add("Id_Conquista", idConquista);

        var infoRequest = new UnityWebRequest(URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(LoginSenha.ToString());
        infoRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        infoRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        infoRequest.SetRequestHeader("Content-Type", "application/json");
        infoRequest.SetRequestHeader("Authorization", "Bearer " + baseToken);

        infoRequest.SendWebRequest();

        //StartCoroutine(IEAddAchievement(baseToken));
    }

    IEnumerator IEAddAchievement(string tokenIE)
    {
        string URL = "https://sistemashomologacao.suafaculdade.com.br/ObjetivoGames/api/UsuarioConquista";

        JSONObject LoginSenha = new JSONObject();
        LoginSenha.Add("Id_Conquista", "2");

        var infoRequest = new UnityWebRequest(URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(LoginSenha.ToString());
        infoRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        infoRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        infoRequest.SetRequestHeader("Content-Type", "application/json");
        infoRequest.SetRequestHeader("Authorization", "Bearer " + tokenIE);

        yield return infoRequest.SendWebRequest();

        if (infoRequest.result == UnityWebRequest.Result.ConnectionError || infoRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(infoRequest.error);
            yield break;
        }

        Debug.Log("Funfou caraio");
    }

    public string splitUrlToken(string[] URLt)
    {
        string newToken = URLt[1];

        return newToken;
    }

    public void SetBaseToken()
    {
        baseToken = splitUrlToken(CurrentURL.Split('='));
    }

    private void startYearCoroutine()
    {
        StartCoroutine(getYearC());
    }

    IEnumerator getYearC()
    {
        // Get Pokemon Info

        string yearUrl = "https://sistemashomologacao.suafaculdade.com.br/ObjetivoGames/api/Data/Ano";
        // Example URL: https://pokeapi.co/api/v2/pokemon/151

        UnityWebRequest webrequest = UnityWebRequest.Get(yearUrl);

        yield return webrequest.SendWebRequest();

        if (webrequest.result == UnityWebRequest.Result.ConnectionError || webrequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webrequest.error);
            yield break;
        }

        JSONNode pokeInfo = JSON.Parse(webrequest.downloadHandler.text);
        //Debug.Log(pokeInfoRequest.downloadHandler.text);
        copyright.text = "Copyright © 1997-" + webrequest.downloadHandler.text + " Grupo UNIP-Objetivo. Todos os direitos reservados.";
    }
}

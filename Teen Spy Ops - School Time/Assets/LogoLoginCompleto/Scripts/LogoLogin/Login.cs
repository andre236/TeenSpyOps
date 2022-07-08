using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField InputLogin;
    public TMP_InputField InputSenha;
    public XuxaApiController plataformApi;

    public string nextScene;

    public Text UserMessage;

    void Start()
    {
        //plataformApi = GameObject.FindGameObjectWithTag("PlataformRequest").GetComponent<XuxaApiController>();
        plataformApi = GameObject.Find("PlataformRequest").GetComponent<XuxaApiController>();
        plataformApi.login = gameObject.GetComponent<Login>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("return") | Input.GetKeyDown("tab") | Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckLogin();
        }
    }

    public void CheckLogin()
    {
        if (InputLogin.text.Length > 0 & InputSenha.text.Length == 0)
        {
            InputSenha.ActivateInputField();
        }
        if (InputLogin.text.Length > 0 & InputSenha.text.Length > 0)
        {
            plataformApi.PostLogin(InputLogin.text, InputSenha.text);
        }

        if(InputLogin.text.Length==0)
        {
            UserMessage.text = "Insira seu usuário.";
            InputLogin.ActivateInputField();
        }
        else if(InputSenha.text.Length == 0)
        {
            UserMessage.text = "Insira sua senha.";
            InputSenha.ActivateInputField();
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}

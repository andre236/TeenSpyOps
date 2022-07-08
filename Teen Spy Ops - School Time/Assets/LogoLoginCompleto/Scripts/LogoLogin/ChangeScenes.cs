using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public Animator ChangeSceneFade;
    public string NextSceneName;

    public void GoToSomeScene(string someSceneName)
    {
        NextSceneName = someSceneName;
        ChangeSceneFade.SetBool("fadeIn", true);
    }

    public void goToNextSceneAnimTrigger()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}

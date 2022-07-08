using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    public ChangeScenes changeScenes;

    public void NextScene()
    {
        changeScenes.goToNextSceneAnimTrigger();
    }
}

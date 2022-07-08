using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariaveisGlobais : MonoBehaviour {

    public static bool efeitos = true;
    public static bool musicas = true;

    public static int CurrentPhase = 1;
    public static int SelectedPhase = 1;
    // Use this for initialization

    //selecao de fases
    public static bool VerificaSelecao = true;

    //gameplay
    public static int vidas = 6;

    //controle de estrelas das fases
    public static int[] Estrelas = new int [21];

    public static bool InTutorial;

    public static bool IcanAddPhase;

    internal static GameObject[] setChildrenWithTag(Transform scenario, string v)
    {
        int qtdChilds = 0;
        for(int i = 0; i < scenario.childCount; i++)
        {
            if(scenario.GetChild(i).tag == v)
            {
                qtdChilds++;
            }
        }

        GameObject[] genericArray = new GameObject[qtdChilds];

        qtdChilds = 0;

        for (int i = 0; i < scenario.childCount; i++)
        {
            if (scenario.GetChild(i).tag == v)
            {
                genericArray[qtdChilds] = scenario.GetChild(i).gameObject;
                qtdChilds++;
            }
        }

        return genericArray;
    }

    public static void AddPhase()
    {
        if (SelectedPhase == CurrentPhase && IcanAddPhase)
        {
            SelectedPhase++;
            CurrentPhase = SelectedPhase;
            IcanAddPhase = false;
        }else
        {
            SelectedPhase++;
            IcanAddPhase = false;
        }
        Debug.Log("Fase: " + SelectedPhase);
    }
}

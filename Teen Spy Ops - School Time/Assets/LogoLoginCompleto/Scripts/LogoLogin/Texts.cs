using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Texts : MonoBehaviour
{
    public static string[,,] TexTutorials;
    public static string[,,] InfTextsTable;
    public static string[,,] PedagogicText;
    public static string[,,] AchievementNames;
    public static string[,,] AchievementInfos;
    public static int TutorialOrder = 0;
    public static Text TutorialInstructions;

    public static void SetTexTutorials(string[,,] jsonLoaded)
    {
        //TexTurials = new string[1, 10, 1];
        
        /*
        TexTurials[0] = "Os clientes estão chegando! Ganhe bastante dinheiro para modificar seu restaurante!";
        TexTurials[1] = "Toque no rostinho em cima da mesa para visualizar quais nutrientes os clientes da mesa precisão!";
        TexTurials[2] = "Para ter mais detalhes dos clientes, toque no balão de informação que acabou de aparecer!";
        TexTurials[3] = "Os garçons trarão pratos com os nutrientes necessários para encher as barras correspondentes!";
        TexTurials[4] = "O numero na parte superior no botão do garçom corresponde ao número da mesa que ele está indo!";
        TexTurials[5] = "Se alguma barra estiver cheia, não deixe o prato com o nutriente correspondente chegar até a mesa. Toque no botão que está em cima do garçom para cancelar o pedido!";
        TexTurials[6] = "NUNCA deixe pratos com comida mal preparada chegar a mesa do cliente, se não é fim de jogo!";
        TexTurials[7] = "Lembre-se: se algum cliente receber mais nutrientes do que ele precisa, ele vai começar a ficar cheio. Se ele ficar muito cheio é fim de jogo!";
        TexTurials[8] = "Quando um cliente estiver com todas as barras cheias, toque em sua mesa para fechar a conta! Feche a conta de todos os clientes para passar de fase!";
    */
        //JsonString jsonVar = jsonLoaded;
        TexTutorials = jsonLoaded;

    }

    public static void SetPedagogicText(string[,,] jsonLoaded)
    {
        PedagogicText = jsonLoaded;
    }

    public static void SetAchievementNames(string[,,] jsonLoaded)
    {
        AchievementNames = jsonLoaded;
    }

    public static void SetAchievementInfos(string[,,] jsonLoaded)
    {
        AchievementInfos = jsonLoaded;
    }

    public static void VerifyMyTutorialPhaseToUpdate(int phase)
    {
        if (phase == TutorialOrder)
            UpdateTutorialPhase();
    }

    public static bool VerifyMyTutorialPhase(int phase)
    {
        if (phase == TutorialOrder)
            return true;
        else
            return false;
    }

    public static void UpdateTutorialPhase()
    {
        TutorialOrder++;
        if(VariaveisGlobais.InTutorial)
            TutorialInstructions.text = TexTutorials[0,TutorialOrder,0];
    }
}

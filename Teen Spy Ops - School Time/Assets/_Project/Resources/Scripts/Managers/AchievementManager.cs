using System;
using UnityEngine;

namespace Manager
{
    public class AchievementManager : MonoBehaviour
    {

        protected internal Action UnlockedSpyRookie;
        protected internal Action UnlockedHackerman;
        protected internal Action UnlockedInTime;
        protected internal Action UnlockedTacticalEspionageAction;
        protected internal Action UnlockedSoloAgent;
        protected internal Action UnlockedCodecMaster;

        private void Awake()
        {
            UnlockedInTime += OnUnlockedInTime;
            UnlockedHackerman += OnUnlockedHackerman;
            UnlockedSpyRookie += OnUnlockedSpyRookie;
            UnlockedTacticalEspionageAction += OnUnlockedTacticalEspionageAction;
            UnlockedSoloAgent += OnUnlockedSoloAgent;
            UnlockedCodecMaster += OnUnlockedCodecMaster;

        }

        private void OnUnlockedHackerman()
        {
            // Usar as dicas de Tina 10 vezes.
            if (PlayerPrefs.GetInt("HACKERMAN") == 1)
                return;

            if (PlayerPrefs.GetInt("AMOUNT_HINTS_USED") >= 10)
            {
                PlayerPrefs.SetInt("HACKERMAN", 1);
                XuxaApiController.AddAchievement("61");
                Debug.Log("Conquista HACKERMAN desbloqueada!");
            }
        }

        private void OnUnlockedCodecMaster()
        {
            // Obtenha 5 estrelas em todas as fases.
            if (PlayerPrefs.GetInt("CODEC_MASTER") == 1)
                return;

            PlayerPrefs.SetInt("CODEC_MASTER", 1);
            XuxaApiController.AddAchievement("65");

            Debug.Log("Conquista CODEC_MASTER desbloqueada!");

        }

        private void OnUnlockedSpyRookie()
        {
            // Termine a primeira fase do jogo.
            if (PlayerPrefs.GetInt("SPY_ROOKIE") == 1)
                return;

            if (PlayerPrefs.GetInt("LEVEL1") == 1)
            {
                XuxaApiController.AddAchievement("60");
                PlayerPrefs.SetInt("SPY_ROOKIE", 1);
                Debug.Log("Conquista SPY_ROOKIE desbloqueada!");

            }
        }

        private void OnUnlockedTacticalEspionageAction()
        {
            // Conclua a última fase do jogo
            if (PlayerPrefs.GetInt("TACTICAL_ESPIONAGE_ACTION") == 1)
                return;

            if (PlayerPrefs.GetInt("LEVEL8") == 1)
            {
                PlayerPrefs.SetInt("TACTICAL_ESPIONAGE_ACTION", 1);
                XuxaApiController.AddAchievement("64");

                Debug.Log("Conquista TACTICAL_ESPIONAGE_ACTION desbloqueada!");

            }

        }

        private void OnUnlockedInTime()
        {
            //Terminar a fase com menos de 30 segundos restantes para o sinal bater.
            if (PlayerPrefs.GetInt("IN_TIME") == 1)
                return;

            PlayerPrefs.SetInt("IN_TIME", 1);
            Debug.Log("Conquista IN_TIME desbloqueada!");

        }

        private void OnUnlockedSoloAgent()
        {
            //Consiga 5 estrelas na última fase sem recorrer às dicas de Tina.
            if (PlayerPrefs.GetInt("SOLO_AGENT") == 1)
                return;

            PlayerPrefs.SetInt("SOLO_AGENT", 1);
            XuxaApiController.AddAchievement("63");

            Debug.Log("Conquista SOLO_AGENT desbloqueada!");

        }




    }
}
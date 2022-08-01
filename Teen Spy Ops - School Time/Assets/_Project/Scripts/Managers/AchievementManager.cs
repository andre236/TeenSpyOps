using System;
using UnityEngine;

namespace Manager
{
    public class AchievementManager : MonoBehaviour
    {

        public Action UnlockedSpyRookie;
        public Action UnlockedHackerman;
        public Action UnlockedInTime;
        public Action UnlockedTacticalEspionageAction;
        public Action UnlockedSoloAgent;
        public Action UnlockedCodecMaster;

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

            PlayerPrefs.SetInt("HACKERMAN", 1);
        }

        private void OnUnlockedCodecMaster()
        {
            // Obtenha 5 estrelas em todas as fases.
            if (PlayerPrefs.GetInt("CODEC_MASTER") == 1)
                return;

            PlayerPrefs.SetInt("CODEC_MASTER", 1);
        }

        private void OnUnlockedSpyRookie()
        {
            // Termine a primeira fase do jogo.
            if (PlayerPrefs.GetInt("SPY_ROOKIE") == 1)
                return;

            if (PlayerPrefs.GetInt("LEVEL1") == 1)
                PlayerPrefs.SetInt("SPY_ROOKIE", 1);
        }

        private void OnUnlockedTacticalEspionageAction()
        {
            // Conclua a última fase do jogo
            if (PlayerPrefs.GetInt("TACTICAL_ESPIONAGE_ACTION") == 1)
                return;

            if (PlayerPrefs.GetInt("LEVEL8") == 1)
                PlayerPrefs.SetInt("TACTICAL_ESPIONAGE_ACTION", 1);

        }

        private void OnUnlockedInTime()
        {
            //Terminar a fase com menos de 30 segundos restantes para o sinal bater.
            if (PlayerPrefs.GetInt("IN_TIME") == 1)
                return;

            PlayerPrefs.SetInt("IN_TIME", 1);
        }

        private void OnUnlockedSoloAgent()
        {
            //Consiga 5 estrelas na última fase sem recorrer às dicas de Tina.
            if (PlayerPrefs.GetInt("SOLO_AGENT") == 1)
                return;

            PlayerPrefs.SetInt("SOLO_AGENT", 1);
        }




    }
}
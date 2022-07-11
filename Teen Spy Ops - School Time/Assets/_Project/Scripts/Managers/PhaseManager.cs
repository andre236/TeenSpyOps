using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class PhaseManager : MonoBehaviour
    {
        private int _currentLevelSelected = -1;

        private Button _playButton;

        [System.Serializable]
        public class Phase
        {
            public bool Unlocked;
            public int Stars;
            public GameObject PhaseButtonPrefab;
            public RawImage[] StarsImage;
        }

        public List<Phase> PhaseList;

        private void Awake() => _playButton = GameObject.Find("PlayButton").GetComponent<Button>();

        private void Start()
        {
            ActiveButtonStars();
            CheckHavePhaseSelected();
        }

        private void CheckHavePhaseSelected()
        {
            if (_currentLevelSelected > -1)
                _playButton.interactable = true;
            else
                _playButton.interactable = false;
        }

        private void ActiveButtonStars()
        {
            PlayerPrefs.SetInt("LEVEL" + 0, 1);

            foreach (Phase phaseButton in PhaseList)
            {

                // Verifying if Unlocked or not
                if (PlayerPrefs.GetInt("LEVEL" + PhaseList.IndexOf(phaseButton)) > 0)
                    phaseButton.Unlocked = true;
                else
                    phaseButton.Unlocked = false;

                phaseButton.PhaseButtonPrefab.GetComponent<Button>().interactable = phaseButton.Unlocked;

                phaseButton.Stars = -1;

                phaseButton.StarsImage = phaseButton.PhaseButtonPrefab.GetComponentsInChildren<RawImage>();

                // Verifying if have stars on level
                if (PlayerPrefs.GetInt("STARSLEVEL" + PhaseList.IndexOf(phaseButton)) > 0)
                    phaseButton.Stars = PlayerPrefs.GetInt("STARSLEVEL" + PhaseList.IndexOf(phaseButton)) - 1;

                else
                    phaseButton.Stars = -1;

                for (int i = 0; i < phaseButton.StarsImage.Length; i++)
                {
                    phaseButton.StarsImage[i].gameObject.SetActive(false);

                    if (i <= phaseButton.Stars && i > -1 && phaseButton.Unlocked)
                        phaseButton.StarsImage[i].gameObject.SetActive(true);
                    else
                        phaseButton.StarsImage[i].gameObject.SetActive(false);
                }

            }


        }

        public void EnterPhaseSelected()
        {
            if (_currentLevelSelected > -1)
                SceneManager.LoadScene("LEVEL" + _currentLevelSelected);
            else
                _playButton.interactable = false;
        }

        public void SendIndexPhase(int phaseIndex)
        {
            _currentLevelSelected = phaseIndex;
            CheckHavePhaseSelected();
        }

    }
}
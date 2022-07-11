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

            foreach (Phase phaseButton in PhaseList)
            {
                phaseButton.PhaseButtonPrefab.GetComponent<Button>().interactable = phaseButton.Unlocked;

                phaseButton.Stars = -1;

                phaseButton.StarsImage = phaseButton.PhaseButtonPrefab.GetComponentsInChildren<RawImage>();

                if (PlayerPrefs.GetInt(string.Concat("STARSLEVEL", PhaseList.IndexOf(phaseButton))) > 0)
                    phaseButton.Stars = PlayerPrefs.GetInt("STARSLEVEL" + PhaseList.IndexOf(phaseButton)) - 1;
                else
                    phaseButton.Stars = -1;

                for (int i = 0; i < phaseButton.StarsImage.Length; i++)
                    phaseButton.StarsImage[i].gameObject.SetActive(false);

                for (int i = 0; i < phaseButton.StarsImage.Length; i++)
                {
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
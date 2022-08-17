using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class PhaseManager : MonoBehaviour
    {
        private int _currentLevelSelected = -1;
        private Button _playButton;
        private AchievementManager _achievementManager;

        [System.Serializable]
        public class Phase
        {
            public bool Unlocked;
            public int Stars;
            public GameObject PhaseButtonPrefab;
            public RawImage[] StarsImage;
            public ItemConfig[] ItemsOnPhase;
        }

        public List<Phase> PhaseList;

        private void Awake()
        {
            _achievementManager = FindObjectOfType<AchievementManager>();
            _playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        }

        private void Start()
        {
            _playButton.onClick.AddListener(EnterPhaseSelected);
            PlayerPrefs.SetInt("TUTORIAL_ISDONE", 1);
            //PlayerPrefs.SetInt("AMOUNT_HINTS_USED", 0);
            ActiveButtonStars();
            CheckHavePhaseSelected();
            CheckHaveAllStars();
            SetItemsEachPhase();
            LoadItemEachPhase();
        }

        internal void CheckHaveAllStars()
        {
            int allStars = 0;
            foreach (Phase phaseButton in PhaseList)
            {
                if (phaseButton.Stars == 4)
                    allStars++;
                else
                    allStars = 0;
            }

            if (allStars >= 8)
                _achievementManager.UnlockedCodecMaster?.Invoke();
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

        private void SetItemsEachPhase()
        {
            if (PlayerPrefs.GetInt("ITEMS_GENERATED") == 1)
                return;

            DirectoryInfo directory = new DirectoryInfo("Assets/_Project/Scripts/ScriptableObject/SchoolObjects");
            FileInfo[] filesInfo = directory.GetFiles("*.asset");

            int[] numbersToShuffle = new int[filesInfo.Length];

            for (int i = 0; i < filesInfo.Length; i++)
            {
                numbersToShuffle[i] = i;
            }

            var sortedNumbers = numbersToShuffle.OrderBy(a => Guid.NewGuid()).ToArray();

            int indexItem = 0;
            for (int numberPhase = 0; numberPhase < 8; numberPhase++)
            {
                for (int items = 0; items < 3; items++)
                {
                    PhaseList[numberPhase].ItemsOnPhase[items] = (ItemConfig)Resources.Load((directory + "/" + filesInfo[sortedNumbers[indexItem]]), typeof(ItemConfig));
                    PlayerPrefs.SetString("LEVEL" + numberPhase + "_ITEMINDEX_" + indexItem + "_ITEMPOSITION" + items, filesInfo[sortedNumbers[indexItem]].Name);
                    indexItem++;
                    if (items == 2 && numberPhase == 7)
                        PlayerPrefs.SetInt("ITEMS_GENERATED", 1);

                }


            }

        }

        private void LoadItemEachPhase()
        {
            if (PlayerPrefs.GetInt("ITEMS_GENERATED") == 0)
                return;

            DirectoryInfo directory = new DirectoryInfo("Assets/_Project/Scripts/ScriptableObject/SchoolObjects");

            int indexItem = 0;
            for (int numberPhase = 0; numberPhase < 8; numberPhase++)
            {
                for (int items = 0; items < 3; items++)
                {
                    PhaseList[numberPhase].ItemsOnPhase[items] = (ItemConfig)Resources.Load((directory + "/" + PlayerPrefs.GetString("LEVEL" + numberPhase + "_ITEMINDEX_" + indexItem + "_ITEMPOSITION" + items)), typeof(ItemConfig));
                    indexItem++;
                }
            }
        }

        public void EnterPhaseSelected()
        {
            if (_currentLevelSelected > -1)
            {
                if (PlayerPrefs.GetInt("TUTORIAL_ISDONE") >= 1)
                    SceneManager.LoadScene("LEVEL" + _currentLevelSelected);
                else
                    SceneManager.LoadScene("TUTORIAL");
            }
            else
            {
                _playButton.interactable = false;

            }
        }

        public void SendIndexPhase(int phaseIndex)
        {
            _currentLevelSelected = phaseIndex;
            CheckHavePhaseSelected();
        }

    }
}
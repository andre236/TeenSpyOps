using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            {
                _achievementManager.UnlockedCodecMaster?.Invoke();
            }
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

            ItemConfig[] allSchoolObjects = Resources.LoadAll<ItemConfig>("Scripts/ScriptableObject/SchoolObjects");

            //DirectoryInfo directory = new DirectoryInfo("Scripts/ScriptableObject/SchoolObjects");
            //FileInfo[] filesInfo = directory.GetFiles("*.asset");

            int[] numbersToShuffle = new int[allSchoolObjects.Length];

            for (int i = 0; i < allSchoolObjects.Length; i++)
            {
                numbersToShuffle[i] = i;
            }

            var sortedNumbers = numbersToShuffle.OrderBy(a => Guid.NewGuid()).ToArray();

            int indexItem = 0;

            for (int numberPhase = 0; numberPhase < 8; numberPhase++)
            {
                for (int items = 0; items < 3; items++)
                {
                    PhaseList[numberPhase].ItemsOnPhase[items] = Resources.Load<ItemConfig>("Scripts/ScriptableObject/SchoolObjects/" + allSchoolObjects[sortedNumbers[indexItem]].name) ;
                    PlayerPrefs.SetString("LEVEL" + numberPhase + "_ITEMINDEX_" + indexItem + "_ITEMPOSITION" + items, allSchoolObjects[sortedNumbers[indexItem]].name);
                    indexItem++;
                    if (items == 2 && numberPhase == 7 && PhaseList[0].ItemsOnPhase[0] != null)
                        PlayerPrefs.SetInt("ITEMS_GENERATED", 1);

                }


            }

        }

        private void LoadItemEachPhase()
        {
            if (PlayerPrefs.GetInt("ITEMS_GENERATED") == 0)
                return;

            int indexItem = 0;
            for (int numberPhase = 0; numberPhase < 8; numberPhase++)
            {
                for (int items = 0; items < 3; items++)
                {
                    PhaseList[numberPhase].ItemsOnPhase[items] = Resources.Load<ItemConfig>("Scripts/ScriptableObject/SchoolObjects/" + PlayerPrefs.GetString("LEVEL" + numberPhase + "_ITEMINDEX_" + indexItem + "_ITEMPOSITION" + items));
                    indexItem++;
                }
            }
        }

        public void EnterPhaseSelected()
        {
            if (_currentLevelSelected > -1)
            {
                StartCoroutine(StartEnterPhase());
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

        private IEnumerator StartEnterPhase()
        {
            var transition = GameObject.Find("Transition").GetComponent<Animator>();
            transition.SetTrigger("FadeIn");

            yield return new WaitForSeconds(1.5f);
            Debug.Log("TUTORIAL_ISDONE: 1");
            if (PlayerPrefs.GetInt("TUTORIAL_ISDONE") >= 1)
            {
                Debug.Log("TUTORIAL_ISDONE: 1");
                SceneManager.LoadScene("LEVEL" + _currentLevelSelected);
            }
            else
            {
                Debug.Log("TUTORIAL_ISDONE: 0");
                SceneManager.LoadScene("TUTORIAL");
            }
        }

    }
}
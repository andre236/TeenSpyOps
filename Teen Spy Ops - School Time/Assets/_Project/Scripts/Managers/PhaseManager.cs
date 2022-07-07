using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class PhaseManager : MonoBehaviour
    {
    
        [System.Serializable]
        public class Phase
        {
            public bool Unlocked;
            public int Stars;
            public GameObject PhaseButtonPrefab;
            public RawImage[] StarsImage;
        }

        public List<Phase> PhaseList;

        private void Awake()
        {

        }

        private void Start()
        {
            ActiveButtonStars();
        }

        private void ActiveButtonStars()
        {
            PlayerPrefs.SetInt("StarsLevel" + 3, 3);
            PlayerPrefs.SetInt("StarsLevel" + 6, 1);
            PlayerPrefs.SetInt("StarsLevel" + 2, 0);
            PlayerPrefs.SetInt("StarsLevel" + 0, 2);

            foreach (Phase phaseButton in PhaseList)
            {
                phaseButton.Stars = PlayerPrefs.GetInt("StarsLevel" + PhaseList.IndexOf(phaseButton));
                phaseButton.StarsImage[PhaseList.IndexOf(phaseButton)].gameObject.SetActive(true);
            }
        }



    }
}
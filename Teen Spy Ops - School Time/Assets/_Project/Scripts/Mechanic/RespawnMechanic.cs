using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Statics;

namespace Mechanic
{
    public class RespawnMechanic : MonoBehaviour
    {
        [field: SerializeField] public SkillState[] GetPossibleSkillState { get; set; }
        [field: SerializeField] public SkillState FinalSkillState { get; set; }
        [field: SerializeField] public XRayDistance FinalDistance { get; set; }

        [field: TextArea(4, 8)]
        [field: SerializeField] public string[] HintsThisPlace { get; set; }
        private void Awake() => GeneratePossibleSkill();
        private void Start()
        {
            GetAllHintsFromGeneral();
        }

        private void GetAllHintsFromGeneral()
        {
            string nameCurrentLevel = Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value;
            int levelNumber = int.Parse(nameCurrentLevel);

            int currentRespawnNumber = int.Parse(Regex.Match(gameObject.name, @"\d+").Value) - 1;

            for (int phaseNumber = 0; phaseNumber < 8; phaseNumber++)
            {
                if (phaseNumber == levelNumber)

                    for (int respawnNumber = 0; respawnNumber < 5; respawnNumber++)
                    {
                        if (currentRespawnNumber == respawnNumber)

                            for (int hintNumber = 0; hintNumber < 2; hintNumber++)
                            {
                                HintsThisPlace[hintNumber] = GeneralTexts.Instance.HintsPerPhaseList[levelNumber].RespawnHint[respawnNumber].Hint[hintNumber];
                            }



                    }



            }

        }

        private void GeneratePossibleSkill()
        {
            var randomSkillNumber = Random.Range(0, GetPossibleSkillState.Length - 1);

            if (GetPossibleSkillState.Length <= 1)
                FinalSkillState = GetPossibleSkillState[0];
            else
                FinalSkillState = GetPossibleSkillState[randomSkillNumber];

        }
    }
}
using Mechanic;
using Statics;
using System.Text.RegularExpressions;

namespace Tutorial
{
    public class TutorialRespawnMechanic : RespawnMechanic
    {

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            GetAllHintsFromGeneral();
        }

        protected override void GetAllHintsFromGeneral()
        {
            int currentRespawnNumber = int.Parse(Regex.Match(gameObject.name, @"\d+").Value) - 1;

            for (int respawnNumber = 0; respawnNumber < 5; respawnNumber++)
            {
                if (currentRespawnNumber == respawnNumber)

                    for (int hintNumber = 0; hintNumber < 2; hintNumber++)
                    {
                        HintsThisPlace[hintNumber] = GeneralTexts.Instance.HintsPerPhaseList[0].RespawnHint[respawnNumber].Hint[hintNumber];
                    }

            }
        }


    }
}
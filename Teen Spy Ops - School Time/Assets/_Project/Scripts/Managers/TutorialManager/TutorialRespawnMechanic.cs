using UnityEngine;
using Mechanic;
using Statics;

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
            base.Start();
        }

        protected override void GetAllHintsFromGeneral()
        {
            for(int hintNumber = 0; hintNumber < GeneralTexts.Instance.HintsTutorial.Length; hintNumber++)
            {
                HintsThisPlace[hintNumber] = GeneralTexts.Instance.HintsTutorial[hintNumber];
            }
        }


    }
}
using Mechanic;
using Statics;
using UnityEngine;

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
            base.GetAllHintsFromGeneral();
        }


    }
}
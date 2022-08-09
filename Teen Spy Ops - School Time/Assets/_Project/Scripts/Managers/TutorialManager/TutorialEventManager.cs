using Manager;
using System;

namespace Tutorial
{
    public class TutorialEventManager : EventManager
    {

        protected internal Action SkippedTutorialLine;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

    }
}
using UnityEngine;

namespace Controllers
{
    public class GuessController : MonoBehaviour
    {
        [field: SerializeField] public int NumberAttempts { get; private set; }

        [field: SerializeField] public int CurrentNumberAttempts { get; private set; }

        public void OnGotQuestion()
        {
            CurrentNumberAttempts = NumberAttempts;
        }

        public void OnChosenIncorrect()
        {
            if (CurrentNumberAttempts > 0)
                CurrentNumberAttempts--;
            else
                CurrentNumberAttempts = 0;
        }
    }
}
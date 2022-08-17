using UnityEngine;

namespace Controllers
{
    public class GuessController : MonoBehaviour
    {
        [field: SerializeField] public int NumberAttempts { get; private set; }

        [field: SerializeField] public int CurrentNumberAttempts { get; private set; }

        public void OnGotQuestion()
        {
            CurrentNumberAttempts = 0;
        }

        public void OnChosenIncorrect()
        {
            if (CurrentNumberAttempts < NumberAttempts)
                CurrentNumberAttempts++;
            else
                CurrentNumberAttempts = NumberAttempts;
        }
    }
}
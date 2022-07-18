using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {

        public AudioController[] SFXArray;
        
        private void Awake()
        {
            
        }
    }

    [System.Serializable]
    public class AudioController
    {
        public AudioSource SoundEffect;
    }
}
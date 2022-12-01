using UnityEngine;

namespace Controllers
{
    public class FingerprintController : MonoBehaviour
    {
        private SpriteRenderer[] _handsSprite;

        private void Awake()
        {
            _handsSprite = GameObject.Find("Fingerprints").GetComponents<SpriteRenderer>();
        }

        private void RandomizerHand()
        {
            var respawnObjects = GameObject.FindGameObjectsWithTag("RespawnObject");


        }


    }
}
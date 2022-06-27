using UnityEngine;

namespace Manager
{
    public class SceneryManager : MonoBehaviour
    {
        private GameObject _normalScene;
        private GameObject _xRayScene;
        private GameObject _nightVisionScene;


        private void Awake()
        {
            _normalScene = GameObject.FindGameObjectWithTag("Normal");
            _xRayScene = GameObject.FindGameObjectWithTag("XRay");
            _nightVisionScene = GameObject.FindGameObjectWithTag("NightVision");
        }

        internal void OnInitializedLevel()
        {
            _normalScene.SetActive(true);
            _xRayScene.SetActive(false);
            _nightVisionScene.SetActive(false);
        }

        internal void OnActivedXRay()
        {
            _xRayScene.SetActive(true);
            _normalScene.SetActive(false);
            _nightVisionScene.SetActive(false);
        }
    }
}
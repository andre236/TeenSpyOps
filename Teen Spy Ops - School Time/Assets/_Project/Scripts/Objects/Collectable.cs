using UnityEngine;
using Manager;
using System;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private SkillState _currentTypeObject;
        [SerializeField] private XRayDistance _currentDistanceHidden;

        public Action<Collectable> Collected;
        

        private void Start()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == _currentTypeObject && gameManager.CurrentDistance == _currentDistanceHidden)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }


        private void OnMouseDown()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if(gameManager.CurrentSkill == _currentTypeObject && gameManager.CurrentDistance == _currentDistanceHidden)
            {
                Collected?.Invoke(this);
                Collected = null;
                Destroy(gameObject);
            }
        }

        internal void OnActivatedXray()
        {
            //var gameManager = FindObjectOfType<GameManager>();

            //if (gameManager.CurrentSkill == _currentTypeObject && gameManager.CurrentDistance == _currentDistanceHidden)
            //    gameObject.SetActive(true);
            //else
            //    gameObject.SetActive(false);
        }
    }
}
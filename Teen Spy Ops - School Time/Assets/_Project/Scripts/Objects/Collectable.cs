using UnityEngine;
using Manager;
using System;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private string _nameObject;
        [SerializeField] private Sprite _spriteObject;

        [SerializeField] private SkillState _currentTypeObject;
        [SerializeField] private XRayDistance _currentDistanceHidden;

        public Action<Collectable> GotQuestion;
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

            if(gameManager.CurrentSkill == _currentTypeObject && gameManager.CurrentDistance <= _currentDistanceHidden)
            {
                GotQuestion?.Invoke(this);
            }
        }

        internal void OnActivatedXray()
        {
            if (this == null)
                return;
            
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == _currentTypeObject && gameManager.CurrentDistance <= _currentDistanceHidden)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);

        }
    }
}
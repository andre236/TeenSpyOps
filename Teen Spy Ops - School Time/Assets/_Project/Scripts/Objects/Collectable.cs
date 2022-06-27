using UnityEngine;
using Manager;
using System;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private SkillState _currentTypeObject;

        public Action<Collectable> Collected;

        private void OnMouseDown()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if(gameManager.CurrentSkill == _currentTypeObject)
            {
                gameObject.SetActive(false);
                Collected?.Invoke(this);
            }
        }

    }
}
using UnityEngine;
using Manager;
using System;
using UnityEngine.Events;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {
        private string _nameObject;
        private Sprite _spriteObject;
        private Sprite _modalNameObject;
        private Sprite _correctModalNameObject;

        private Sprite _modalObjectA;
        private Sprite _incorrectModalA;
        private Sprite _modalObjectB;
        private Sprite _incorrectModalB;

        private Animator _clickOverAnimation;

        private SkillState _currentTypeObject;
        private XRayDistance _currentDistanceHidden;

        [SerializeField] private ItemConfig _itemConfig;

        [SerializeField] private UnityAction CorrectChoosen;

        public Action<Sprite, Sprite, Sprite, Sprite, Sprite, Sprite, Sprite> GotQuestion;
        public Action<Collectable> Collected;


        private void Awake()
        {
            _nameObject = _itemConfig.NameObject;
            _spriteObject = _itemConfig.SpriteObject;
            _modalNameObject = _itemConfig.ModalNameObject;
            _correctModalNameObject = _itemConfig.CorrectModalNameObject;
            
            _modalObjectA = _itemConfig.ModalObjectA;
            _modalObjectB = _itemConfig.ModalObjectB;
            _incorrectModalA = _itemConfig.IncorrectModalA;
            _incorrectModalB = _itemConfig.IncorrectModalB;

            _clickOverAnimation = GetComponent<Animator>();

            GetComponent<SpriteRenderer>().sprite = _spriteObject;
            _currentTypeObject = _itemConfig.CurrentTypeObject;
            _currentDistanceHidden = _itemConfig.CurrentDistanceHidden;
        }

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

            if (gameManager.CurrentSkill == _currentTypeObject && gameManager.CurrentDistance <= _currentDistanceHidden)
            {
                _clickOverAnimation.Play("GettingItem");
                GotQuestion?.Invoke(_spriteObject, _modalNameObject, _correctModalNameObject, _modalObjectA, _incorrectModalA, _modalObjectB, _incorrectModalB);
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
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
        private Sprite _normalModal;
        private Sprite _correctModal;
        private Sprite _incorrectModal;

        private Animator _clickOverAnimation;

        private SkillState _currentTypeObject;
        private XRayDistance _currentDistanceHidden;

        [SerializeField] private ItemConfig _itemConfig;

        [SerializeField] private UnityAction CorrectChoosen;

        public Action<string, Sprite, Sprite, Sprite, Sprite> GotQuestion;
        public Action<Collectable> Collected;


        private void Awake()
        {
            _nameObject = _itemConfig.NameObject;
            _spriteObject = _itemConfig.SpriteObject;
            _normalModal = _itemConfig.ModalScriptable.DefaultCorrectModal;
            _correctModal = _itemConfig.ModalScriptable.DefaultCorrectModal;
            _incorrectModal = _itemConfig.ModalScriptable.DefaultIncorrectModal;

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
                GotQuestion?.Invoke(_nameObject, _spriteObject, _normalModal, _correctModal, _incorrectModal);
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
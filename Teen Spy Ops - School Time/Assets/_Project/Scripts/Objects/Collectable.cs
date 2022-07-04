using UnityEngine;
using Manager;
using System;
using UnityEngine.Events;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {
        private string _nameObject;
        private string[] _fakeNames;
        private Sprite _spriteObject;
        private Sprite _normalModal;
        private Sprite _correctModal;
        private Sprite _incorrectModal;

        private Animator _clickOverAnimation;

        [field: Header("Where you going find")]
        [field: SerializeField] public SkillState CurrentTypeObject { get; private set; }
        [field: SerializeField] public XRayDistance CurrentDistanceHidden { get; private set; }
        [Space]
        [SerializeField] private ItemConfig _itemConfig;

        [SerializeField] private UnityAction CorrectChoosen;

        public Action<string,string[], Sprite, Sprite, Sprite, Sprite> GotQuestion;
        public Action<GameObject> CheckedItemOnList;


        private void Awake()
        {
            _nameObject = _itemConfig.NameObject;
            _fakeNames = _itemConfig.FakeNames;
            _spriteObject = _itemConfig.SpriteObject;
            _normalModal = _itemConfig.ModalScriptable.DefaultCorrectModal;
            _correctModal = _itemConfig.ModalScriptable.DefaultCorrectModal;
            _incorrectModal = _itemConfig.ModalScriptable.DefaultIncorrectModal;

            _clickOverAnimation = GetComponent<Animator>();

            GetComponent<SpriteRenderer>().sprite = _spriteObject;
        }

        private void Start()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject && gameManager.CurrentDistance == CurrentDistanceHidden)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }


        private void OnMouseDown()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject)
            {
                GotQuestion?.Invoke(_nameObject, _fakeNames, _spriteObject, _normalModal, _correctModal, _incorrectModal);
                CheckedItemOnList?.Invoke(gameObject);
            }
        }

        internal void OnActivatedXray()
        {
            if (this == null)
                return;

            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject && gameManager.CurrentDistance <= CurrentDistanceHidden)
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                gameObject.SetActive(true);
            }
            else
                gameObject.SetActive(false);
        }


        internal void OnActivedNightVision()
        {
            if (this == null)
                return;

            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }

        internal void OnActivedFingerprint()
        {
            if (this == null)
                return;

            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }
}
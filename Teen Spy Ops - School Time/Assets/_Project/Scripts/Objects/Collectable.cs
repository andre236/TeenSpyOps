using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Manager;
using UnityEditor;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {
        private string _nameObject;
        [SerializeField] private string _hintObject;

        private Sprite _spriteObject;
        private Sprite _normalModal;
        private Sprite _correctModal;
        private Sprite _incorrectModal;

        private Animator _clickOverAnimation;

        [SerializeField] private bool _isRandomValue;
        [SerializeField] private SkillState _customTypeObject;
        [SerializeField] private XRayDistance _customDistanceHidden;
        [Space]

        private ItemConfig _itemConfig;
        [SerializeField] private UnityAction CorrectChoosen;

        [field: SerializeField] public SkillState CurrentTypeObject { get; private set; }
        [field: SerializeField] public XRayDistance CurrentDistanceHidden { get; private set; }

        public Action<string, Sprite, Sprite, Sprite, Sprite> GotQuestion;
        public Action<GameObject> CheckedItemOnList;


        private void Awake()
        {
            GetRandomSchoolObject();
            _nameObject = _itemConfig.NameObject;

            _normalModal = _itemConfig.ModalScriptable.DefaultCorrectModal;
            _correctModal = _itemConfig.ModalScriptable.DefaultCorrectModal;
            _incorrectModal = _itemConfig.ModalScriptable.DefaultIncorrectModal;

            _clickOverAnimation = GetComponent<Animator>();

        }

        private void Start()
        {
            var gameManager = FindObjectOfType<GameManager>();

            GenerateSkillRandom();

            if (CurrentTypeObject == SkillState.XRay)
                _spriteObject = _itemConfig.SpriteXRayObject;
            else
                _spriteObject = _itemConfig.DefaultSpriteObject;

            GetComponent<SpriteRenderer>().sprite = _spriteObject;

            if (gameManager.CurrentSkill == CurrentTypeObject && gameManager.CurrentDistance == CurrentDistanceHidden)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);


        }

        private void GetRandomSchoolObject()
        {
            DirectoryInfo directory = new DirectoryInfo("Assets/_Project/Scripts/ScriptableObject/SchoolObjects");

            FileInfo[] filesInfo = directory.GetFiles("*.asset");
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            ItemConfig[] itemsInScene = FindObjectsOfType<ItemConfig>();

            for (int i = 0; i < levelManager.AllowedSchoolObjects.Length; i++)
            {
                if (filesInfo[i].Name == levelManager.AllowedSchoolObjects[i])
                {
                    if(itemsInScene[i].NameObject != filesInfo[i].Name.Replace(".asset", ""))
                    {
                        _itemConfig = (ItemConfig)AssetDatabase.LoadAssetAtPath(directory + "/" + filesInfo[i].Name, typeof(ItemConfig));
                        Debug.Log("Registrado o item: " + filesInfo[i].Name);
                        break;
                    }
                    else
                    {
                        Debug.Log("Já existe um objeto com o nome: " + filesInfo[i].Name);
                    }
                }
            }

        }

        private void GenerateSkillRandom()
        {
            if (!_isRandomValue)
                return;

            CurrentTypeObject = (SkillState)UnityEngine.Random.Range(1, 3);
            CurrentDistanceHidden = (XRayDistance)UnityEngine.Random.Range(0, 2);
        }

        private void OnMouseDown()
        {
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject)
            {
                GotQuestion?.Invoke(_nameObject, _spriteObject, _normalModal, _correctModal, _incorrectModal);
                GetComponent<AudioSource>().Play();
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

        internal void OnUpgradeXRayVision()
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
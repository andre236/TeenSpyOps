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
        [SerializeField] private SkillState _currentTypeObject;
        [SerializeField] private XRayDistance _currentDistanceHidden;

        private ItemConfig _itemConfig;
        [SerializeField] private UnityAction CorrectChoosen;

        [field: Header("Where you going find")]
        public SkillState CurrentTypeObject
        {
            get
            {
                if (!_isRandomValue)
                    return _currentTypeObject;
                else
                    return CurrentTypeObject;
            }
            set 
            {
                if (!_isRandomValue)
                    _currentTypeObject = value;
                else
                    CurrentTypeObject = value;
            }
        }

        public XRayDistance CurrentDistanceHidden { get; private set; }

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

            if (_isRandomValue)
            {
                CurrentTypeObject = (SkillState)UnityEngine.Random.Range(1, 3);
                CurrentDistanceHidden = (XRayDistance)UnityEngine.Random.Range(0,2);
            }

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

            int[] numbersToShuffle = new int[filesInfo.Length];


            for (int i = 0; i < filesInfo.Length; i++)
            {
                numbersToShuffle[i] = i;
            }

            var sortedNumbers = numbersToShuffle.OrderBy(a => Guid.NewGuid()).ToArray();

            for (int i = 0; i < sortedNumbers.Length; i++)
            {
                if (PlayerPrefs.GetInt(filesInfo[sortedNumbers[i]].Name) == 0)
                {
                    _itemConfig = (ItemConfig)AssetDatabase.LoadAssetAtPath(directory + "/" + filesInfo[sortedNumbers[i]].Name, typeof(ItemConfig));
                    PlayerPrefs.SetInt(filesInfo[sortedNumbers[i]].Name, 1);
                    Debug.Log("Registrado o item: " + filesInfo[sortedNumbers[i]].Name);
                    break;
                }
                else if (i == sortedNumbers.Length - 1 && PlayerPrefs.GetInt(filesInfo[sortedNumbers[i]].Name) == 1)
                {
                    //Só para não deixar em branco.
                    _itemConfig = (ItemConfig)AssetDatabase.LoadAssetAtPath(directory + "/" + filesInfo[UnityEngine.Random.Range(0, 3)].Name, typeof(ItemConfig));

                    Debug.Log("Todos já foram gerados.");
                }
            }



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
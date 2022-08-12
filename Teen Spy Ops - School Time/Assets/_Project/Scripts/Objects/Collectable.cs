using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Manager;
using UnityEditor;
using Mechanic;
using Tutorial;

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

        private Animator _itemAnimation;
        private ParticleSystem _particle;

        [SerializeField] private bool _isRandomValue;
        [SerializeField] private SkillState _customTypeObject;
        [SerializeField] private XRayDistance _customDistanceHidden;
        [Space]

        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private UnityAction CorrectChoosen;

        [field: SerializeField] public SkillState CurrentTypeObject { get; private set; }
        [field: SerializeField] public XRayDistance CurrentDistanceHidden { get; private set; }
        public ItemConfig ItemConfig { get => _itemConfig; set => _itemConfig = value; }

        public Action<string, Sprite> GotQuestion;
        public Action<GameObject> CheckedItemOnList;


        private void Awake()
        {
            GetRandomSchoolObject();
            _nameObject = ItemConfig.NameObject;

            _normalModal = ItemConfig.ModalScriptable.DefaultCorrectModal;
            _correctModal = ItemConfig.ModalScriptable.DefaultCorrectModal;
            _incorrectModal = ItemConfig.ModalScriptable.DefaultIncorrectModal;

            _itemAnimation = GetComponent<Animator>();
            _particle = transform.GetChild(0).GetComponent<ParticleSystem>();

        }

        private void Start()
        {
            var gameManager = FindObjectOfType<GameManager>();

            GenerateSkill();

            if (CurrentTypeObject == SkillState.XRay)
                _spriteObject = ItemConfig.SpriteXRayObject;
            else
                _spriteObject = ItemConfig.DefaultSpriteObject;

            GetComponent<SpriteRenderer>().sprite = _spriteObject;

            StartCoroutine(nameof(ApplyDelayToDeActiveGameObject));


        }

        private void GetRandomSchoolObject()
        {
            DirectoryInfo directory = new DirectoryInfo("Assets/_Project/Scripts/ScriptableObject/SchoolObjects");

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            Collectable[] itemsCollectables = FindObjectsOfType<Collectable>();

            for (int i = 0; i < itemsCollectables.Length; i++)
            {
                if (SceneManager.GetActiveScene().name != "TUTORIAL")
                    ItemConfig = (ItemConfig)AssetDatabase.LoadAssetAtPath(directory + "/" + levelManager.AllowedSchoolObjects[i], typeof(ItemConfig));
                else
                    if (GameObject.Find(FindObjectOfType<TutorialLevelManager>().ItemTutorial[i].NameObject) == null)
                        ItemConfig = FindObjectOfType<TutorialLevelManager>().ItemTutorial[i];
            }

            gameObject.name = ItemConfig.NameObject;
        }

        private void GenerateSkill()
        {
            var typeSkillGenerate = transform.GetComponentInParent<RespawnMechanic>();

            if (!_isRandomValue)
            {
                CurrentTypeObject = _customTypeObject;
                CurrentDistanceHidden = _customDistanceHidden;
                return;
            }

            CurrentTypeObject = typeSkillGenerate.FinalSkillState;
            CurrentDistanceHidden = typeSkillGenerate.FinalDistance;
        }

        private void OnMouseDown()
        {
            var gameManager = FindObjectOfType<GameManager>();
            var levelManager = FindObjectOfType<LevelManager>();


            if (gameManager.CurrentSkill == CurrentTypeObject)
            {
                if (GameObject.Find("GuessingPage") == null)
                {
                    CheckedItemOnList?.Invoke(gameObject);
                    GotQuestion?.Invoke(levelManager.CurrentObject.name, levelManager.CurrentObject.GetComponent<SpriteRenderer>().sprite);
                    _itemAnimation.SetTrigger("Clicked");

                }
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
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                gameObject.SetActive(false);
            }
        }

        internal void OnUpgradeXRayVision()
        {
            if (this == null)
                return;

            var gameManager = FindObjectOfType<GameManager>();

            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            gameObject.SetActive(true);

            if (gameManager.CurrentSkill == CurrentTypeObject && gameManager.CurrentDistance <= CurrentDistanceHidden)
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                gameObject.SetActive(true);
            }
            else
            {
                GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                gameObject.SetActive(false);
            }
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

        internal void OnFinishedTimerSkill()
        {
            if (this == null)
                return;

            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }

        IEnumerator ApplyDelayToDeActiveGameObject()
        {
            yield return new WaitForSeconds(0.1f);
            var gameManager = FindObjectOfType<GameManager>();

            if (gameManager.CurrentSkill == CurrentTypeObject && gameManager.CurrentDistance == CurrentDistanceHidden)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }
}
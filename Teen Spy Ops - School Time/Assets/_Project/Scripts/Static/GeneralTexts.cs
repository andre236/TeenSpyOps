using System.IO;
using UnityEditor;
using UnityEngine;

namespace Statics
{
    public class GeneralTexts : MonoBehaviour
    {
        [SerializeField] private string[] _nameObjects;

        public string[] NameObjects { get => _nameObjects; set => _nameObjects = value; }

        public static GeneralTexts Instance { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

        }

        private void Start()
        {
            GetSchoolObjectsName();
        }

        private void GetSchoolObjectsName()
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/_Project/Scripts/ScriptableObject/SchoolObjects");

            FileInfo[] info = dir.GetFiles("*.asset");

            NameObjects = new string[info.Length];

            for (int i = 0; i < info.Length; i++)
            {
                NameObjects[i] = info[i].Name.Replace(".asset", "");
            }


        }
    }
}
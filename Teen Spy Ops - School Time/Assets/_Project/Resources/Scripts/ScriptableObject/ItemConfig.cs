using UnityEngine;
using Statics;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "School Object", menuName = "Create new School Object")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string NameObject { get; private set; }
    [field: SerializeField] public Sprite DefaultSpriteObject { get; private set; }
    [field: SerializeField] public Sprite SpriteXRayObject { get; private set; }
    [field: SerializeField] public ModalScriptable ModalScriptable { get; private set; }

    private void OnValidate()
    {
        ItemConfig[] allSchoolObjects = Resources.LoadAll<ItemConfig>("Scripts/ScriptableObject/SchoolObjects");

        for (int i = 0; i < allSchoolObjects.Length; i++)
        {
            if (allSchoolObjects[i].name == this.name)
            {
                NameObject = allSchoolObjects[i].name;
                //NameObject = GeneralTexts.SchoolObjects[0, i, 0];
                break;
            }
        }

    }
}

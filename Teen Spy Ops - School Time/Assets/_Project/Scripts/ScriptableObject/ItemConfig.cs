using UnityEngine;

[CreateAssetMenu(fileName = "School Object", menuName = "Create new School Object")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string NameObject { get; private set; }
    [field: SerializeField] public Sprite SpriteObject { get; private set; }
    [field: SerializeField] public Sprite ModalNameObject { get; private set; }
    [field: SerializeField] public Sprite CorrectModalNameObject { get; private set; }

    [field: SerializeField] public SkillState CurrentTypeObject { get; private set; }
    [field: SerializeField] public XRayDistance CurrentDistanceHidden { get; private set; }


}

using UnityEngine;

[CreateAssetMenu(fileName = "School Object", menuName = "Create new School Object")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string NameObject { get; private set; }
    [field: SerializeField] public Sprite SpriteObject { get; private set; }
    [field: SerializeField] public ModalScriptable ModalScriptable { get; private set; }

    [field:Header("Where you going find")]
    [field: SerializeField] public SkillState CurrentTypeObject { get; private set; }
    [field: SerializeField] public XRayDistance CurrentDistanceHidden { get; private set; }


}

using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "School Object", menuName = "Create new School Object")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string NameObject { get; private set; }
    [field: SerializeField] public string[] FakeNames { get; private set; }
    [field: SerializeField] public Sprite DefaultSpriteObject { get; private set; }
    [field: SerializeField] public Sprite SpriteXRayObject { get; private set; }

    [field: SerializeField] public ModalScriptable ModalScriptable { get; private set; }

    private void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        string displayName = Path.GetFileNameWithoutExtension(assetPath);
        NameObject = displayName;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName ="Create New Costumizated Modal")]
public class ModalScriptable : ScriptableObject
{
    [field:SerializeField] public Sprite DefaultModal { get; private set; }
    [field: SerializeField] public Sprite DefaultCorrectModal { get; private set; }
    [field: SerializeField] public Sprite DefaultIncorrectModal { get; private set; }
}

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Statics;

[CreateAssetMenu(fileName = "School Object", menuName = "Create new School Object")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string NameObject { get; internal set; }
    [field: SerializeField] public Sprite DefaultSpriteObject { get; private set; }
    [field: SerializeField] public Sprite SpriteXRayObject { get; private set; }
    [field: SerializeField] public ModalScriptable ModalScriptable { get; private set; }

}

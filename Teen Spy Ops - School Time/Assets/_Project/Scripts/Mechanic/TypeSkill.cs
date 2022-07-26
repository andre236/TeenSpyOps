using UnityEngine;

namespace Mechanic
{
    public class TypeSkill : MonoBehaviour
    {
        [field: SerializeField] public SkillState[] GetPossibleSkillState { get; set; }
        [field: SerializeField] public SkillState FinalSkillState { get; set; }
        [field: SerializeField] public XRayDistance FinalDistance { get; set; }

        private void Awake() => GeneratePossibleSkill();

        private void GeneratePossibleSkill()
        {
            var randomSkillNumber = Random.Range(0, GetPossibleSkillState.Length - 1);

            if (GetPossibleSkillState.Length <= 1)
                FinalSkillState = GetPossibleSkillState[0];
            else
                FinalSkillState = GetPossibleSkillState[randomSkillNumber];
            
        }
    }
}
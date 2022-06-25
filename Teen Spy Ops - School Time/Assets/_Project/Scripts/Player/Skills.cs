using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Skills : MonoBehaviour
    {
        private bool _onCoolDownSkill;

        [Range(0,15)][SerializeField] private float _timerXRay;
        [Range(0, 15)][SerializeField] private float _timerDigital;


        [SerializeField] private UnityEvent _activatedXRay;

        private void Awake()
        {
            
        }

        private void ActiveXRay()
        {
            if (_onCoolDownSkill)
                return;

            _activatedXRay?.Invoke();
            Invoke(nameof(SetOffCoolDown), _timerXRay);
        }

        private bool SetOffCoolDown()
        {
            return _onCoolDownSkill = false;
        }
    }
}
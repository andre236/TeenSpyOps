using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Mechanic;

namespace Controllers
{
    public class HintController : MonoBehaviour
    {
        [SerializeField] private int _amountHint;
        [SerializeField] private GameObject[] _respawnsObject;
        [SerializeField] private string[] _avaliableHints;
        [SerializeField] private string _currentHint;

        public int AmountHint { get => _amountHint; set => _amountHint = value; }
        public string CurrentHint { get => _currentHint; set => _currentHint = value; }

        private void GetAvailableHint()
        {
            _respawnsObject = GameObject.FindGameObjectsWithTag("RespawnObject");

            for (int i = 0; i < _respawnsObject.Length; i++)
            {
                if (_respawnsObject[i].transform.childCount > 0)
                    if (_respawnsObject[i].transform.GetChild(0).gameObject.transform.childCount > 0)
                        _currentHint = _respawnsObject[i].GetComponent<RespawnMechanic>().HintsThisPlace[UnityEngine.Random.Range(0, 1)];

            }

        }

        internal void OnGotHint(string hint, int amountHints)
        {
            if (amountHints < 1)
                return;

            GetAvailableHint();
        }
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Mechanic;
using System.Collections;

namespace Controllers
{
    public class HintController : MonoBehaviour
    {
        [SerializeField] private int _amountHint;
        [SerializeField] private List<GameObject> _respawnsObject;
        [SerializeField] private List<string> _avaliableHints;
        [SerializeField] private string _currentHint;

        public int AmountHint { get => _amountHint; set => _amountHint = value; }
        public string CurrentHint { get => _currentHint; set => _currentHint = value; }

        private void Start() => StartCoroutine(nameof(StartCheckingHints));

        private void GetAvailableHint()
        {
            if (_avaliableHints.Count > 0)
            {
                _avaliableHints.Clear();
                _respawnsObject.Clear();
            }

            _respawnsObject.AddRange(GameObject.FindGameObjectsWithTag("RespawnObject"));

            foreach (GameObject respawn in _respawnsObject)
            {
                if (respawn.transform.childCount > 0)
                {
                    if (respawn.transform.GetChild(0).transform.childCount > 0)
                    {
                        _avaliableHints.Add(respawn.GetComponent<RespawnMechanic>().HintsThisPlace[0]);
                        _avaliableHints.Add(respawn.GetComponent<RespawnMechanic>().HintsThisPlace[1]);

                    }

                }
            }



            _currentHint = _avaliableHints[UnityEngine.Random.Range(0, _avaliableHints.Count - 1)];
            _avaliableHints.Remove(_currentHint);

        }

        internal void OnGotHint(string hint, int amountHints)
        {
            if (amountHints < 1)
                return;

            GetAvailableHint();
        }

        IEnumerator StartCheckingHints()
        {
            yield return new WaitForSeconds(1f);
            GetAvailableHint();
        }
    }
}
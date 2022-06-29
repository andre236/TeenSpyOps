using UnityEngine;
using Manager;

namespace Mechanic
{
    public class FollowCursor : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (_gameManager.CurrentState == GameState.Paused)
                return;

            Vector2 mousePosition = Input.mousePosition;

            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = mousePosition;
        }

    }
}
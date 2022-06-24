using UnityEngine;

namespace Mechanic
{
    public class FollowCursor : MonoBehaviour
    {
        private Vector2 _mousePosition;

        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = mousePosition;
        }

    }
}
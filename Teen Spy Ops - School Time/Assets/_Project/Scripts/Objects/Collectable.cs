using UnityEngine;

namespace Objects
{
    public class Collectable : MonoBehaviour
    {

        private void OnMouseDown()
        {
            gameObject.SetActive(false);
        }
    }
}
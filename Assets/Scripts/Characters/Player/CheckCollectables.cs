using UnityEngine;
using Collectables;

namespace Characters.Player 
{
    public class CheckCollectables : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}

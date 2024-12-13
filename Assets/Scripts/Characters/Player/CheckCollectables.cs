using UnityEngine;
using Collectables;
using UnityEngine.Events;

namespace Characters.Player 
{
    public class CheckCollectables : MonoBehaviour
    {
        public UnityEvent onCollected;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
                onCollected?.Invoke();
            }
        }
    }
}

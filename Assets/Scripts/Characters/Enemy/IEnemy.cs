
using UnityEngine;

namespace Characters.Enemy 
{
    public interface IEnemy
    {
        public Transform GetTransform(); 
        public void InRange(bool inRange);
        public void SetTarget(bool isTarget);
    }
}

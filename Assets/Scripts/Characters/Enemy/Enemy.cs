using UnityEngine;

namespace Characters.Enemy 
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        public void InRange(bool inRange)
        {

        }


        public void SetTarget(bool isTarget)
        {

        }


        public Transform GetTransform()
        {
            return transform;
        } 
    }
}
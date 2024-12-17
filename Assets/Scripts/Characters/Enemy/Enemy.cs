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

        
        public void Hit(float attackForce)
        {
            Debug.Log("Enemy hit");
        }


        public Transform GetTransform()
        {
            return transform;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Characters.Enemy.States
{
    public class Die : EnemyState
    {
        private new Collider collider;
        private new Rigidbody rigidbody;


        private void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
        }


        public override void StartState(EnemyStateMachine stateMachine)
        {
            base.StartState(stateMachine);

            rigidbody.useGravity = false;
            collider.enabled = false;
            
            StartCoroutine("ScaleAnimation");
        }


        private IEnumerator ScaleAnimation()
        {
            animator.SetTrigger("Die");

            yield return new WaitForSeconds(2f);

            collider.enabled = true;
            rigidbody.useGravity = true;

            gameObject.SetActive(false);
        }
    }
}


using System.Collections;
using UnityEngine;

namespace Characters.Enemy.States
{
    public class Init : EnemyState
    {
        [SerializeField] private AnimationCurve scaleCurve;

        public EnemyStateType nextState;

        private new Collider collider;
        private new Rigidbody rigidbody;


        private void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
        }


        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
        }


        public override void StartState(EnemyStateMachine stateMachine)
        {
            base.StartState(stateMachine);

            collider.enabled = false;
            rigidbody.useGravity = false;

            StartCoroutine(ScaleAnimation(stateMachine));
        }


        public override void ExitState(EnemyStateMachine stateMachine)
        {
            base.ExitState(stateMachine);

            collider.enabled = true;
            rigidbody.useGravity = true;
        }


        private IEnumerator ScaleAnimation(EnemyStateMachine stateMachine)
        {
            float evaluateTime = 0f;

            while (evaluateTime != 1f)
            {
                float value = scaleCurve.Evaluate(evaluateTime);
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, value);

                evaluateTime += 0.01f;

                if (evaluateTime > 1f)
                {
                    evaluateTime = 1f;
                    transform.localScale = Vector3.one;
                }

                yield return null;
            }

            animator.SetBool("Init", true);

            yield return new WaitForSeconds(1f);

            animator.SetBool("Init", false);
            stateMachine.SwitchState(nextState);
        }
    }
}

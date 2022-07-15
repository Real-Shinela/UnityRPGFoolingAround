using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float AttackRange = 2f;
        [SerializeField] float AttackCoolDown = 5f;
        private float AttackCDTemp = 0f;
        private Animator animator;

        public Transform target;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (target != null)
            {
                bool isInRange = Vector3.Distance(transform.position, target.position) < AttackRange;
                if (!isInRange)
                {
                    GetComponent<ActionScheduler>().StartAction(this);
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else
                {
                    if (AttackCDTemp < Time.time)
                    {
                        AttackAnimStart();
                        AttackCDTemp = Time.time + AttackCoolDown;
                    }
                }
            }
            else
            {

            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            GetComponent<ActionScheduler>().StartAction(this);
            animator.ResetTrigger("AttackTrigger");
            CancelInvoke();
            target = null;
        }

        // Animator event
        void Hit()
        {
            print("Damoog done");
        }

        private void AttackAnimStart()
        {
            animator.SetTrigger("AttackTrigger");
        }
    }
}
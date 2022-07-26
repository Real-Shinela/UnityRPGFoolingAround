using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System.Collections;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float attackDamage = 10f;
        [SerializeField] float attackDeviationRange = 3f;
        [SerializeField] float AttackRange = 2f;
        [SerializeField] float AutoAttCd = 5f;
        private Animator animator;
        private bool attackReady = true;
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
                    enabled = false;
                    enabled = true;
                    GetComponent<ActionScheduler>().StartAction(this);
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else
                {
                    GetComponent<ActionScheduler>().StartAction(this);
                    AttackRoutine();
                }
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        private void AttackRoutine()
        {
            // Attack not ready therefore just stop
            if (!attackReady) return;
            // Anything below this happens if attack IS ready

            AttackAnimStart();

            // Cooldown starts here
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            attackReady = false;
            yield return new WaitForSeconds(AutoAttCd);
            attackReady = true;
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
            if (target.TryGetComponent(out Health targetHP))
            {
                // Damage Roundings to halves
                float damageDone = Mathf.Round((attackDamage + Random.Range(-attackDeviationRange, attackDeviationRange)) * 2) / 2;

                targetHP.TakeDamage(damageDone);
            }
        }

        private void AttackAnimStart()
        {
            animator.SetTrigger("AttackTrigger");
        }
    }
}
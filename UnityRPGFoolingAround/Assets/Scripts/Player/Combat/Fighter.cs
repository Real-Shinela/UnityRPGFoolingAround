using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System.Collections;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // Combat stuff
        [SerializeField] float attackDamage = 10f;
        [SerializeField] float attackDeviationRange = 3f;
        [SerializeField] float AttackRange = 2f;
        [SerializeField] float AutoAttCd = 5f;

        // Animations
        private Animator animator;
        private bool attackReady = true;

        // Targetting
        public Health target;


        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (target != null)
            {
                if (target.IsDead())
                {
                    Cancel();
                    return;
                }
                bool isInRange = Vector3.Distance(transform.position, target.transform.position) < AttackRange;
                if (!isInRange)
                {
                    enabled = false;
                    enabled = true;
                    GetComponent<ActionScheduler>().StartAction(this);
                    GetComponent<Mover>().MoveTo(target.transform.position);
                }
                else
                {
                    GetComponent<ActionScheduler>().StartAction(this);
                    AttackRoutine();
                }
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();

            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.GetComponent<Health>();
        }

        private void AttackRoutine()
        {
            transform.LookAt(target.transform);

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
            animator.SetTrigger("cancelAttack");
            animator.ResetTrigger("cancelAttack");
            animator.ResetTrigger("AttackTrigger");
            CancelInvoke();
            target = null;
        }

        // Starting animation which triggers Hit()
        private void AttackAnimStart()
        {
            animator.SetTrigger("AttackTrigger");
        }

        // Animator event
        void Hit()
        {
            // Damage Roundings to halves
            float damageDone = Mathf.Round((attackDamage + Random.Range(-attackDeviationRange, attackDeviationRange)) * 2) / 2;
            try
            {
                target.TakeDamage(damageDone);
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("lol u missed");
            }
        }

    }
}
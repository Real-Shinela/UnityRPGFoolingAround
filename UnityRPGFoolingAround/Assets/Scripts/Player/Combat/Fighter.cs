using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float AttackRange = 2f;

        public Transform target;

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
            target = null;
        }
    }
}
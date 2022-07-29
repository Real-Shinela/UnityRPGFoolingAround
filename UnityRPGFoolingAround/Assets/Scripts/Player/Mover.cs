using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;
        Animator animator;
        private Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = true;
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void MoveAction(Vector3 moveDestination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(moveDestination);
        }

        public void MoveTo(Vector3 moveDestination)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(moveDestination);
        }

        public void Cancel()
        {
            navMeshAgent.ResetPath();
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 8f;

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

        public void MoveAction(Vector3 moveDestination, float speedFraction = 1)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(moveDestination, speedFraction);
        }

        public void MoveTo(Vector3 moveDestination, float speedFraction = 1)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
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
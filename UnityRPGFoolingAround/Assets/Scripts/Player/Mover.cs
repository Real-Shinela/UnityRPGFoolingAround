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

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
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
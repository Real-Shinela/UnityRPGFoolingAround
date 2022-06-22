using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        private void Start()
        {
            GetComponent<NavMeshAgent>();
        }

        private void Update()
        {

            UpdateAnimation();
        }

        public void MoveTo(Vector3 moveDestination)
        {
            GetComponent<NavMeshAgent>().destination = moveDestination;
        }

        private void UpdateAnimation()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<NavMeshAgent>().velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
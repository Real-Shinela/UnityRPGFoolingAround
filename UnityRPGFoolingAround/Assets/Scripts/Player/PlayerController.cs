using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent navMesh;
        private Mover mover;
        private Fighter fighter;

        private void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            InteractWithCombat();
            MoveForwards();
        }

        private bool InteractWithCombat()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach (RaycastHit hit in hits)
                {
                    hit.transform.TryGetComponent(out CombatTarget target);
                    if (target == null)
                    {
                        continue;
                    }
                    else
                    {
                        fighter.Attack(target);
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        public void Stop()
        {
            navMesh.isStopped = true;
        }

        private bool MoveForwards()
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            if (horInput == 0 && verInput == 0)
            {
                Stop();
            }
            else
            {
                navMesh.isStopped = false;
            }
            if (navMesh.isStopped) return false;

            Vector3 movement = new(horInput, 0f, verInput);
            Vector3 moveDestination = transform.position + movement.normalized;
            mover.MoveAction(moveDestination);
            return true;
        }
    }
}

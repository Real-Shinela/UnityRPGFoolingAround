using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            InteractWithCombat();
            bool test = MoveForwards();
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        public void Stop()
        {
            GetComponent<NavMeshAgent>().isStopped = true;
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
                GetComponent<NavMeshAgent>().isStopped = false;
            }
            if (GetComponent<NavMeshAgent>().isStopped) return false;

            Vector3 movement = new Vector3(horInput, 0f, verInput);
            Vector3 moveDestination = transform.position + movement;
            GetComponent<Mover>().MoveTo(moveDestination);
            return true;
        }
    }
}

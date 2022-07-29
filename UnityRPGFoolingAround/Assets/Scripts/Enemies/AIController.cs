using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDist = 10f;
        [SerializeField] float aggressDist = 15f;
        [SerializeField] float suspicionTme = 5f;

        private Fighter fighter;
        private Mover mover;
        private Health health;
        private GameObject player;

        // Guarding stuff
        private Vector3 guardPos;
        float timeSinceSawPlayer = Mathf.Infinity;

        // Distance checking to save processing power
        private float nextCheck;
        private readonly float checkSpeed = 0.5f;

        private void Awake()
        {
            enabled = true;
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPos = transform.position;
        }

        private void Update()
        {
            // If dead, don't do anything
            if (health.IsDead()) enabled = false;

            timeSinceSawPlayer += Time.deltaTime;

            if (nextCheck < Time.time)
            {
                nextCheck = Time.time + checkSpeed;

                // Attack goes first
                if (PlayerInRange() && fighter.CanAttack(player))
                {
                    AttackBehaviour();
                }

                // Enemy left sight: Be suspicious
                else if (timeSinceSawPlayer < suspicionTme)
                {
                    SuspicionBehaviour();
                }

                // Return to position because suspicion is lost
                else
                {
                    GuardBehaviour();
                }

            }
        }

        private void AttackBehaviour()
        {
            timeSinceSawPlayer = 0;
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrAction();
        }

        private void GuardBehaviour()
        {
            mover.MoveAction(guardPos);
        }

        private bool PlayerInRange()
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            return dist < aggressDist;
        }
    }
}
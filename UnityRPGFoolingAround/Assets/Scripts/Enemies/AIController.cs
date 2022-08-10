using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [Header("Distances")]
        [SerializeField] float chaseDist = 10f;
        [SerializeField] float aggressDist = 15f;
        [Header("Timers")]
        [SerializeField] float suspicionTme = 5f;
        [SerializeField] float dwellTimeAtWaypoint = 5f;
        [Header("Waypoint Stuff")]
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float patrolSpeedFraction = 0.2f;

        private Fighter fighter;
        private Mover mover;
        private Health health;
        private GameObject player;

        // Guarding stuff
        private Vector3 guardPos;
        private float timeSinceSawPlayer = Mathf.Infinity;
        private float timeSinceWayPoint = Mathf.Infinity;
        public int currentWayPointIndex = 0;

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
            timeSinceWayPoint += Time.deltaTime;

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
                    PatrolBehaviour();
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

        private void PatrolBehaviour()
        {
            Vector3 nextPos;
            if (patrolPath == null) nextPos = guardPos;
            else
            {
                if (Vector2DDistance(transform.position, GetWayPoint()) < wayPointTolerance)
                {
                    timeSinceWayPoint = 0;
                    CycleWayPoint();
                }
                nextPos = GetWayPoint();
            }
            if (timeSinceWayPoint > dwellTimeAtWaypoint) mover.MoveAction(nextPos, patrolSpeedFraction);
        }

        private Vector3 GetWayPoint()
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool PlayerInRange()
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            return dist < aggressDist;
        }

        private float Vector2DDistance(Vector3 a, Vector3 b)
        {
            float xdiff = a.x - b.x;
            float zdiff = a.z - b.z;
            return MathF.Sqrt((xdiff * xdiff) + (zdiff * zdiff));
        }
    }
}
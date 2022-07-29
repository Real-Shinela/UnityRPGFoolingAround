using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDist = 10f;
        [SerializeField] float aggressDist = 15f;

        private Fighter fighter;
        private Health health;
        private GameObject player;

        // Distance checking to save processing power
        private float nextCheck;
        private readonly float checkSpeed = 0.5f;

        private void Awake()
        {
            enabled = true;
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) enabled = false;

            if (nextCheck < Time.time)
            {
                nextCheck = Time.time + checkSpeed;
                if (PlayerInRange() && fighter.CanAttack(player)) fighter.Attack(player);
            }
        }

        private bool PlayerInRange()
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            return dist < aggressDist;
        }
    }
}
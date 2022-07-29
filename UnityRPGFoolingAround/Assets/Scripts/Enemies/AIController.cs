using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDist = 10f;
        [SerializeField] float aggressDist = 15f;

        private GameObject player;

        private float nextCheck;
        private float checkSpeed = 0.5f;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (nextCheck < Time.time)
            {
                nextCheck = Time.time + checkSpeed;
                if (PlayerInRange()) print(name + "come at me bro");
            }
        }

        private bool PlayerInRange()
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            return dist < aggressDist;
        }
    }
}
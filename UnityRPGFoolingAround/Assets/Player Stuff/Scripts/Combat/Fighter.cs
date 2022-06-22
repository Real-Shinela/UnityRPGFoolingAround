using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float AttackRange = 2f;

        public void Attack(CombatTarget target)
        {
            if (Vector3.Distance(target.transform.position, GetComponent<NavMeshAgent>().transform.position) < AttackRange)
            {
                print("Gotcha");
            }
            else print("Come closer dude");
        }
    }
}
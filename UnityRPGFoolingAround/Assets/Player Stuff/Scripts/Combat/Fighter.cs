using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float AttackRange;

        public void Attack(CombatTarget target)
        {
            print("Uwot m9 fite me");
        }      
    }
}
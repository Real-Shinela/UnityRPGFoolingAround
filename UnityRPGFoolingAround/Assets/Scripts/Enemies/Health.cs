using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100;
        [SerializeField] float currHealth;
        private Animator anim;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Awake()
        {
            currHealth = maxHealth;
            anim = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            currHealth = Mathf.Clamp(currHealth - damage, 0, maxHealth);

            if (currHealth <= 0)
            {
                if (isDead) return;
                anim.SetTrigger("die");
                isDead = true;
            }
        }
    }
}

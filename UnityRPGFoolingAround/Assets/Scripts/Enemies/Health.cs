using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100;
        public float currHealth;
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
                DieAction();
            }
        }

        private void DieAction()
        {
            if (isDead) return;
            anim.SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrAction();
            isDead = true;
        }
    }
}

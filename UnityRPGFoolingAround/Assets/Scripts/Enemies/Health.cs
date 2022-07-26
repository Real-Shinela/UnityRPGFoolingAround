using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100;
        [SerializeField] float currHealth;

        private void Awake()
        {
            currHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currHealth = Mathf.Clamp(currHealth - damage, 0, maxHealth);

            if (currHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

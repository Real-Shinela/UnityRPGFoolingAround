using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        currHealth -= damage;
        print(currHealth);
        if (currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

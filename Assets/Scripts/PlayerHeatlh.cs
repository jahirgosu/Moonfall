using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeatlh : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public int enemyDamage = 25;

    public HealthSlider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.SetMaxHealth(maxHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth = currentHealth - enemyDamage;

            healthSlider.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}

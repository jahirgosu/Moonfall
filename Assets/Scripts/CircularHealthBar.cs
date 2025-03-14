using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularHealthBar : MonoBehaviour
{
    public Image circleImage;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float enemyDamage = 25f;

    float lerpSpeed;




    void Start()
    {
        currentHealth = maxHealth;
        circleImage.fillAmount = currentHealth / maxHealth;

    }




    void Update()
    {
        // Ejemplo: Al presionar H, reduces vida
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHealth -= 10;
            if (currentHealth < 0) currentHealth = 0;
        }


        // Actualizas la UI
        circleImage.fillAmount = currentHealth / maxHealth;



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth -= enemyDamage;
            if (currentHealth < 0) currentHealth = 0;
            circleImage.fillAmount = currentHealth / maxHealth;
            if (currentHealth <= 0) Destroy(gameObject);

            circleImage.fillAmount = currentHealth / maxHealth;

        }
    }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularHealthBar : MonoBehaviour
{
    public Image circleImage;
    public Image idleImage;
    public Image damagedImage;
    public Sprite meow;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float enemyDamage = 25f;
    public float fillSpeedAmount = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        circleImage.fillAmount = currentHealth / maxHealth;
        //Color newColor = new Color(0.3f, 0.4f, 0.6f, 0.3f);
        //DOTween.SetTweensCapacity(2000, 100);
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
        float targetFillAmount = currentHealth / maxHealth;
        circleImage.DOFillAmount(targetFillAmount, fillSpeedAmount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth -= enemyDamage;
            if (currentHealth < 0) currentHealth = 0;
            //circleImage.fillAmount = currentHealth / maxHealth;
            if (currentHealth <= 0) Destroy(gameObject);

            //circleImage.fillAmount = currentHealth / maxHealth;
            // DOVirtual.DelayedCall(_damageDelay);
            //circleImage.DOColor(Color.red, 1f);
            //StartCoroutine(ColorRedDelay());

        }
    }

    //private IEnumerator ColorRedDelay()
    //{
      //  yield return new WaitForSeconds(2f);
      //  circleImage.DOColor(new Color(39f, 248f, 238f, 255f), 1f);
    //}

}

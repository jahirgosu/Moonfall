using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float damageDuration = 0.2f;
    public float invincibilityDuration = 1f;
    public float blinkInterval = 0.1f;  // Tiempo entre parpadeos

    AudioManager audioManager;

    private bool isInvincible = false;
    private SpriteRenderer playerSpriteRenderer;  // 🔹 Referencia al SpriteRenderer

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentHealth = maxHealth;
        circleImage.fillAmount = currentHealth / maxHealth;

        idleImage.gameObject.SetActive(true);
        damagedImage.gameObject.SetActive(false);

        playerSpriteRenderer = GetComponent<SpriteRenderer>();  // Obtener el SpriteRenderer del jugador
        if (playerSpriteRenderer == null)
        {
            Debug.LogWarning("No se encontró SpriteRenderer en el jugador.");
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        float targetFillAmount = currentHealth / maxHealth;
        circleImage.DOFillAmount(targetFillAmount, fillSpeedAmount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isInvincible)
        {
            audioManager.PlaySFX(audioManager.damaged);
            currentHealth -= enemyDamage;
            if (currentHealth < 0) currentHealth = 0;

            if (currentHealth <= 0)
            {
                GameOverScene();
            }

            StartCoroutine(ShowDamageEffect());
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator ShowDamageEffect()
    {
        idleImage.gameObject.SetActive(false);
        damagedImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(damageDuration);
        damagedImage.gameObject.SetActive(false);
        idleImage.gameObject.SetActive(true);
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        if (playerSpriteRenderer != null)
        {
            StartCoroutine(BlinkSprite());
        }

        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;

        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.enabled = true;  // Asegurarse de que quede visible al final
        }
    }

    private IEnumerator BlinkSprite()
    {
        while (isInvincible)
        {
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void GameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}


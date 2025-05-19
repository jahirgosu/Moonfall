using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private EnemyPatrol patrolScript;

    public float health, maxHealth = 3f;
    public float currentHealth;
    public Animator anim;

    public float invincibilityDuration = 1f;
    public float blinkInterval = 0.1f;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 savedVelocity;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        patrolScript = GetComponent<EnemyPatrol>();

        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetBool("isDamaged", false);
        health = maxHealth;
        currentHealth = health;
    }

    void Update()
    {
        if (health < currentHealth)
        {
            currentHealth = health;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;
        audioManager.PlaySFX(audioManager.batImpact);
        health -= damage;
        anim.SetBool("isDamaged", true);

        if (health <= 0)
        {
            audioManager.PlaySFX(audioManager.batDeath);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        if (patrolScript != null)
        {
            patrolScript.canMove = false;
        }
        // Detener movimiento
        if (rb != null)
        {
            savedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;  // Evita fuerzas externas si aplica
        }

        // Parpadeo
        if (spriteRenderer != null)
        {
            float timer = 0f;
            while (timer < invincibilityDuration)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(blinkInterval);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(blinkInterval);
                timer += blinkInterval * 2;
            }
        }
        else
        {
            yield return new WaitForSeconds(invincibilityDuration);
        }

        if (patrolScript != null)
        {
            patrolScript.canMove = true;
        }
        // Restaurar movimiento
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = savedVelocity;
        }

        isInvincible = false;
        anim.SetBool("isDamaged", false);
    }
}


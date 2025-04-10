using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damagePoints;
    public float pushbackForce;
    public Transform attackOrigin;
    [Range(0f, 5f)]
    public float attackRangeRadius;
    public LayerMask enemyLayer;
    [Header("Animation Settings")]
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("AttackTrigger");
        }
    }

    public void Attack()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRangeRadius, enemyLayer);
        for (int i = 0; i < enemyColliders.Length; i++) {
            GameObject enemy = enemyColliders[i].gameObject;
            enemy.gameObject.GetComponent<AIBrain>().TransitionToState("Damaged");
           // enemy.gameObject.GetComponent<PlayerHeatlh>().TakeDamage(damagePoints); //
            Vector3 attackDir = enemy.transform.position - attackOrigin.position;
            enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(attackDir.normalized * pushbackForce);
        }
    }

    private void OnDrawGizmos()
    {
        if (attackOrigin != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackOrigin.position, attackRangeRadius);
        }
    }
}

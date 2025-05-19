using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         EnemyScript enemy = collision.GetComponent<EnemyScript>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}

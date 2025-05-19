using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyScript ENEMYY = collision.GetComponent<EnemyScript>();
            if (ENEMYY != null)
            {
                ENEMYY.TakeDamage(damage);
            }
        }
    }

}

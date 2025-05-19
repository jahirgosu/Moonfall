using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public GameObject Melee;
    bool isAttacking = false;
    float attackTimer = 0f;
    float attackDuration = 0.5f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("isAttacking", true);
            //OnAttack();

        }
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
            audioManager.PlaySFX(audioManager.Attack);

            //Call animator to play attack
            //anim.SetBool("isAttacking", true);
        }
    }

    void CheckMeleeTimer()
    {
        if(isAttacking)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                attackTimer = 0f;
                isAttacking=false;
                Melee.SetActive(false); 
            }
        }
    }

    public void endAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}


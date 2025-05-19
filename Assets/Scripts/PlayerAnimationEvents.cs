using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public void OnAttackAnimationEvent()
    {
        this.gameObject.GetComponentInParent<PlayerAttack>().Attack();
    }
}

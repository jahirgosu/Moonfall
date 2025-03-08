using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    private void Start()
    {
        StartLogo();
    }

    public void StartLogo()
    {
        transform.LeanMoveLocal(new Vector2(0, 74), 1).setEaseOutQuad();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour
{
    public Image Logo;
    private void Start()
    {
        StartLogo();
    }

    public void StartLogo()
    {
        transform.LeanMoveLocal(new Vector2(0, 225), 1).setEaseOutQuad();
        Logo.transform.LeanMoveLocal(new Vector2(680, -391), 1).setEaseOutQuad();
    }
}

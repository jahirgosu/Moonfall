using System.Collections.Generic;

using System.Collections;
using UnityEngine;

public class StaminaGotasTest : MonoBehaviour
{
    public Animator[] gotasAnim; // Asigna en Inspector
    public float tiempoRecuperacion = 2f;

    private int gotasMax = 5;
    private int gotasActuales;
    private bool recuperando = false;

    void Start()
    {
        gotasActuales = gotasMax;
        ActualizarAnimaciones();
    }

    void Update()
    {
        // Clic izquierdo (mouse button 0)
        if (Input.GetMouseButtonDown(0))
        {
            if (gotasActuales > 0)
            {
                ConsumirGota();
                Debug.Log("Ataque ejecutado. Gotas restantes: " + gotasActuales);
            }
            else
            {
                Debug.Log("Sin gotas. No puedes atacar.");
            }
        }

        if (gotasActuales < gotasMax && !recuperando)
        {
            StartCoroutine(RecuperarGota());
        }
    }

    void ConsumirGota()
    {
        gotasActuales--;
        gotasAnim[gotasActuales].Play("GotaVacia");
    }

    IEnumerator RecuperarGota()
    {
        recuperando = true;
        yield return new WaitForSeconds(tiempoRecuperacion);
        gotasAnim[gotasActuales].Play("GotaLlena");
        gotasActuales++;
        recuperando = false;
    }

    void ActualizarAnimaciones()
    {
        for (int i = 0; i < gotasMax; i++)
        {
            if (i < gotasActuales)
                gotasAnim[i].Play("GotaLlena");
            else
                gotasAnim[i].Play("GotaVacia");
        }
    }
}

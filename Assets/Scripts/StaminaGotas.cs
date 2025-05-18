using System.Collections;
using UnityEngine;

public class StaminaGotas : MonoBehaviour
{
    public Animator[] gotasAnim; // Asigna en el inspector
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
        // Ataca con J
        if (Input.GetKeyDown(KeyCode.J) && gotasActuales > 0)
        {
            ConsumirGota();
        }

        // Recupera automáticamente si hay gotas faltantes y no se está recuperando
        if (gotasActuales < gotasMax && !recuperando)
        {
            StartCoroutine(RecuperarGota());
        }
    }

    void ConsumirGota()
    {
        gotasActuales--;
        gotasAnim[gotasActuales].Play("Vacia");
    }

    IEnumerator RecuperarGota()
    {
        recuperando = true;
        yield return new WaitForSeconds(tiempoRecuperacion);
        gotasAnim[gotasActuales].Play("Llena");
        gotasActuales++;
        recuperando = false;
    }

    void ActualizarAnimaciones()
    {
        for (int i = 0; i < gotasMax; i++)
        {
            if (i < gotasActuales)
                gotasAnim[i].Play("Llena");
            else
                gotasAnim[i].Play("Vacia");
        }
    }
}

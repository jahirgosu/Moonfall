using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{


    public float Max = 100f;
    public float energia_actual;



    public float Consumo = 5f;
    public float runMultiplier = 2f;

    public float jumpMultiplier = 5f;

    public float Recarga = 20f;


    public Image energyImage;

    void Start()
    {
        energia_actual = Max;
        UpdateEnergyUI();
    }

    void Update()
    {
    
        bool Moviendose = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);


        if (Moviendose)
        {
            float consumoFinal = Consumo * Time.deltaTime;
            // Si se presiona Shift, se consume más energía
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                consumoFinal *= runMultiplier;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                consumoFinal *= jumpMultiplier;
            }

            energia_actual -= consumoFinal;
            if (energia_actual < 0f)
                energia_actual = 0f;
        }
        else
        {
            // Regenera energía cuando no se mueve
            energia_actual += Recarga * Time.deltaTime;
            if (energia_actual > Max)
                energia_actual = Max;
        }

        UpdateEnergyUI();
    }
   
    void UpdateEnergyUI()
    {
        if (energyImage != null)
            energyImage.fillAmount = energia_actual / Max;
    }
}

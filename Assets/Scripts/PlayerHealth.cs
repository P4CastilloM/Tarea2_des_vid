using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual = 100f;

    public Slider barraVida;

    void Start()
    {
        vidaActual = vidaMaxima;

        barraVida.maxValue = vidaMaxima;
        barraVida.value = vidaActual;
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual < 0)
            vidaActual = 0;

        barraVida.value = vidaActual;
    }

    public void Curar(float cantidad)
    {
        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;

        barraVida.value = vidaActual;
    }
}
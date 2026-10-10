using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual = 100f;

    public Slider barraVida;
    [Header("Efecto Curacion")]
    public ParticleSystem efectoCuracion;
    public AudioClip sonidoCuracion;    
    [Header("Efecto Danio")]
    public ParticleSystem efectoDanio;
    public AudioClip sonidoDanio;

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

        if(efectoDanio != null)
            efectoDanio.Play();
        if(sonidoDanio != null)
            AudioSource.PlayClipAtPoint(sonidoDanio, transform.position);
    }

    public void Curar(float cantidad)
    {
        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;
        if (efectoCuracion != null && vidaActual < vidaMaxima)
            efectoCuracion.Play();
        if (sonidoCuracion != null && vidaActual < vidaMaxima)
            AudioSource.PlayClipAtPoint(sonidoCuracion, transform.position);
        barraVida.value = vidaActual;
    }
}
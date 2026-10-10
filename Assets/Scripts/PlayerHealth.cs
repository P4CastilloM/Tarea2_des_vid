using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private bool cargandoDerrota = false;

    void Start()
    {
        vidaActual = vidaMaxima;

        barraVida.maxValue = vidaMaxima;
        barraVida.value = vidaActual;
    }

    public void RecibirDanio(float cantidad)
    {
        // Si ya estamos perdiendo, no recibe más daño
        if (cargandoDerrota)
            return;

        vidaActual -= cantidad;

        if (vidaActual < 0)
            vidaActual = 0;

        barraVida.value = vidaActual;

        // Efectos de daño
        if (efectoDanio != null)
            efectoDanio.Play();

        if (sonidoDanio != null)
            AudioSource.PlayClipAtPoint(sonidoDanio, transform.position);

        // DERROTA
        if (vidaActual <= 0)
        {
            StartCoroutine(CargarDerrota());
        }
    }

    public void Curar(float cantidad)
    {
        if (cargandoDerrota)
            return;

        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;

        if (efectoCuracion != null && vidaActual < vidaMaxima)
            efectoCuracion.Play();

        if (sonidoCuracion != null && vidaActual < vidaMaxima)
            AudioSource.PlayClipAtPoint(sonidoCuracion, transform.position);

        barraVida.value = vidaActual;
    }

    private IEnumerator CargarDerrota()
    {
        cargandoDerrota = true;

        Debug.Log("HAS PERDIDO");

        // Pequeña pausa antes de cambiar de escena
        yield return new WaitForSeconds(1f);

        AsyncOperation carga =
            SceneManager.LoadSceneAsync("perdiste", LoadSceneMode.Single);

        while (!carga.isDone)
        {
            yield return null;
        }
    }
}
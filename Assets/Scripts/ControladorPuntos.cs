using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorPuntos : MonoBehaviour
{
    [SerializeField]
    private int puntosTotales = 0;

    [SerializeField]
    private TMP_Text puntosTexto;

    [SerializeField]
    private int puntosParaGanar = 100;

    private bool cargandoVictoria = false;

    void Start()
    {
        ActualizarTextoPuntos();
    }

    public void SumarPuntos(int cantidad)
    {
        if (cargandoVictoria)
            return;

        puntosTotales += cantidad;

        Debug.Log("Puntos totales: " + puntosTotales);

        ActualizarTextoPuntos();

        // Cuando llegue a 100 puntos, gana
        if (puntosTotales >= puntosParaGanar)
        {
            StartCoroutine(CargarVictoria());
        }
    }

    private void ActualizarTextoPuntos()
    {
        if (puntosTexto != null)
        {
            puntosTexto.text = "Puntos: " + puntosTotales;
        }
    }

    private IEnumerator CargarVictoria()
    {
        cargandoVictoria = true;

        // Pequeña espera para alcanzar a ver los 100 puntos
        yield return new WaitForSeconds(0.5f);

        // Carga asincrónica de la escena
        AsyncOperation carga = SceneManager.LoadSceneAsync("victoria");

        while (!carga.isDone)
        {
            yield return null;
        }
    }
}
using UnityEngine;
using TMPro;

public class ControladorPuntos : MonoBehaviour
{
    [SerializeField]
    private int puntosTotales = 0;
    [SerializeField]
    private TMP_Text puntosTexto;

    void Start()
    {
        ActualizarTextoPuntos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SumarPuntos(int cantidad)
    {
        puntosTotales += cantidad;
        Debug.Log("Puntos totales: " + puntosTotales);
        ActualizarTextoPuntos();
    }
    private void ActualizarTextoPuntos()
    {
        if (puntosTexto != null)
        {
            puntosTexto.text = "Puntos: " + puntosTotales.ToString();
        }
    }
}

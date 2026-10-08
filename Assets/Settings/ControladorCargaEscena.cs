using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorCargaEscena : MonoBehaviour
{
    public GameObject pantalla_carga;
    public Slider barra_progreso;
    public TMP_Text porcentaje_text;

    public float velocidad_relleno = 1.5f;

    public void CargarEscena(int indice_escena)
    {
        StartCoroutine(_CargarEscena(indice_escena));
    }

    IEnumerator _CargarEscena(int indice_escena)
    {
        if(pantalla_carga != null)
        {
            pantalla_carga.SetActive(true);
        }
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync(indice_escena);
        operacionCarga.allowSceneActivation = false;
        float progreso = 0f;
        while(!operacionCarga.isDone)
        {
            float progresoReal = Mathf.Clamp01(operacionCarga.progress / 0.9f);
            progreso = Mathf.MoveTowards(progreso, progresoReal, velocidad_relleno * Time.deltaTime);
            if(barra_progreso != null)
            {
                barra_progreso.value = progreso;
            }
            if(porcentaje_text != null)
            {
                porcentaje_text.text = (progreso * 100f).ToString("F0") + "%";
            }
            if(progreso >= 1f && operacionCarga.progress >= 0.9f)
            {
                operacionCarga.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}

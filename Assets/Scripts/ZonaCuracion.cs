using System.Collections;
using UnityEngine;

public class ZonaCuracion : MonoBehaviour
{
    [Header("Curación")]
    public float cantidadCuracion = 5f;
    public float tiempoEntreCuracion = 1f;

    private PlayerHealth jugador;
    private Coroutine rutinaCuracion;
    private int contactos = 0;

    private void Awake()
    {
        // Asegura que la zona funcione como Trigger
        Collider col = GetComponent<Collider>();

        if (col != null)
            col.isTrigger = true;

        // Rigidbody para que los Trigger funcionen correctamente
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health == null)
            return;

        contactos++;

        if (jugador == null)
        {
            jugador = health;

            // Cura inmediatamente al entrar
            jugador.Curar(cantidadCuracion);

            rutinaCuracion = StartCoroutine(CurarContinuamente());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health == null || health != jugador)
            return;

        contactos--;

        if (contactos <= 0)
        {
            contactos = 0;

            if (rutinaCuracion != null)
            {
                StopCoroutine(rutinaCuracion);
                rutinaCuracion = null;
            }

            jugador = null;
        }
    }

    private IEnumerator CurarContinuamente()
    {
        while (jugador != null)
        {
            yield return new WaitForSeconds(tiempoEntreCuracion);

            if (jugador != null)
                jugador.Curar(cantidadCuracion);
        }
    }
}
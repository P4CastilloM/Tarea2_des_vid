using System.Collections;
using UnityEngine;

public class ObjetoDanino : MonoBehaviour
{
    [Header("Daño")]
    public float danioInicial = 25f;
    public float danioContinuo = 5f;
    public float tiempoEntreDanio = 2f;

    private PlayerHealth jugador;
    private Coroutine rutinaDanio;
    private int contactosConJugador = 0;

    private void Awake()
    {
        CrearZonasDeDanio();

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void CrearZonasDeDanio()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer rend in renderers)
        {
            // Creamos un objeto SOLO para detectar daño
            GameObject zona = new GameObject("TriggerDanio");

            zona.transform.SetParent(rend.transform);
            zona.transform.localPosition = Vector3.zero;
            zona.transform.localRotation = Quaternion.identity;
            zona.transform.localScale = Vector3.one;

            BoxCollider trigger = zona.AddComponent<BoxCollider>();
            trigger.isTrigger = true;

            MeshFilter mesh = rend.GetComponent<MeshFilter>();

            if (mesh != null && mesh.sharedMesh != null)
            {
                trigger.center = mesh.sharedMesh.bounds.center;
                trigger.size = mesh.sharedMesh.bounds.size;
            }
            else if (rend is SkinnedMeshRenderer skinned)
            {
                trigger.center = skinned.localBounds.center;
                trigger.size = skinned.localBounds.size;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health == null)
            return;

        contactosConJugador++;

        if (jugador == null)
        {
            jugador = health;

            jugador.RecibirDanio(danioInicial);

            rutinaDanio = StartCoroutine(DanioContinuo());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerHealth health = other.GetComponentInParent<PlayerHealth>();

        if (health == null || health != jugador)
            return;

        contactosConJugador--;

        if (contactosConJugador <= 0)
        {
            contactosConJugador = 0;

            if (rutinaDanio != null)
            {
                StopCoroutine(rutinaDanio);
                rutinaDanio = null;
            }

            jugador = null;
        }
    }

    private IEnumerator DanioContinuo()
    {
        while (jugador != null)
        {
            yield return new WaitForSeconds(tiempoEntreDanio);

            if (jugador != null)
                jugador.RecibirDanio(danioContinuo);
        }
    }
}
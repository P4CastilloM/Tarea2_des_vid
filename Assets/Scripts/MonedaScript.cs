using UnityEngine;

public class MonedaScript : MonoBehaviour
{
    public int valorMoneda = 1;
    [SerializeField]
    public string tag_buscado;
    public Animator animacionMoneda;
    public AudioClip sonidoMoneda;
    public float tiempoDestruccion = 1f;
    private bool obtenido = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (obtenido) return;

        if (other.gameObject.tag == tag_buscado)
        {
            Debug.Log("Trigger con: " + other.name + " (tag " + other.tag + ")");
            obtenido = true;
            GetComponent<Collider>().enabled = false;

            if (animacionMoneda != null)
            {
                animacionMoneda.SetTrigger("obtencion");
            }

            if (sonidoMoneda != null)
            {
                AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position);
            }
            ControladorPuntos controladorPuntos = other.GetComponent<ControladorPuntos>();
            if (controladorPuntos == null)
            {
                controladorPuntos = other.GetComponentInParent<ControladorPuntos>();
            }
            if (controladorPuntos != null)
            {
                controladorPuntos.SumarPuntos(valorMoneda);
            }
            else
            {
                Debug.LogWarning("No se encontró el componente ControladorPuntos en el objeto con tag " + tag_buscado);
            }
            Destroy(gameObject,tiempoDestruccion); 
        }
    }
}

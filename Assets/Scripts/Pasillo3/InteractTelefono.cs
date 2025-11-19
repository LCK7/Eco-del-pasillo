using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    public AudioSource telefonoSonando;     // Audio 1 (ring)
    public AudioSource audioConversacion;   // Audio 2 (voz)
    public GameObject uiInteract;           // "[E] Contestar"
    public float distancia = 3f;

    private bool llamadaEnCurso = false;

    void Update()
    {
        if (llamadaEnCurso) return;

        // Distancia jugador → teléfono
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador == null) return;

        float dist = Vector3.Distance(jugador.transform.position, transform.position);

        if (dist <= distancia)
        {
            uiInteract.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                ContestarTelefono();
            }
        }
        else
        {
            uiInteract.SetActive(false);
        }
    }

    void ContestarTelefono()
    {
        llamadaEnCurso = true;

        uiInteract.SetActive(false);

        if (telefonoSonando != null)
            telefonoSonando.Stop();

        if (audioConversacion != null)
            audioConversacion.Play();
    }
}

using UnityEngine;

public class PhoneCallTrigger : MonoBehaviour
{
    public AudioSource telefonoSonando; // audio 1
    private bool yaActivado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaActivado)
        {
            yaActivado = true;

            if (telefonoSonando != null)
                telefonoSonando.Play();
        }
    }
}

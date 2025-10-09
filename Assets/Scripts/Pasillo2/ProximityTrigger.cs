using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    public AudioSource sonido;
    public Animator animPuerta;
    public Collider bloqueoPasillo;
    private bool abierta = false; // Estado actual

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            abierta = !abierta; 

            if (sonido != null)
                sonido.Play();

            if (animPuerta != null)
                animPuerta.SetBool("Abierta", abierta);

            if (bloqueoPasillo != null)
                bloqueoPasillo.enabled = abierta; 
        }
    }
}

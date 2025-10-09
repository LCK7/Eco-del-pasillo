using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    public AudioSource sonido; 
    public Animator animPuerta; 
    public Collider bloqueoPasillo;
    private bool yaActivado = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (yaActivado) return;

        if (other.CompareTag("Player"))
        {
            yaActivado = true;


            if (sonido != null)
                sonido.Play();


            if (animPuerta != null)
                animPuerta.SetTrigger("AbrirParcial");


            if (bloqueoPasillo != null)
                bloqueoPasillo.enabled = true;
        }
    }
}

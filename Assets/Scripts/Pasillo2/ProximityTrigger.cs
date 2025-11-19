using UnityEngine;
using System.Collections; // Necesario para Coroutines

public class ProximityTrigger : MonoBehaviour
{
    public AudioSource sonido;
    public Animator animPuerta;
    public Collider bloqueoPasillo;
    
    public float duracionAnimacion = 0.5f; 

    private bool yaActivado = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaActivado) 
        {
            yaActivado = true; 

            if (sonido != null)
                sonido.Play();

            if (animPuerta != null)
            {
                animPuerta.SetBool("AbrirParcial", true); 
                StartCoroutine(DesactivarAnimator());
            }

            // ❌ Ya NO bloquear nada
            // if (bloqueoPasillo != null)
            //     bloqueoPasillo.enabled = false;
        }
    }

    IEnumerator DesactivarAnimator()
    {
        yield return new WaitForSeconds(duracionAnimacion); 
        
        if (animPuerta != null)
        {
            animPuerta.enabled = false;
        }
    }
}

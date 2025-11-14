using UnityEngine;
using System.Collections; // Necesario para Coroutines

public class ProximityTrigger : MonoBehaviour
{
    public AudioSource sonido;
    public Animator animPuerta;
    public Collider bloqueoPasillo;
    
    // Ajusta esto al tiempo exacto de tu clip "AnimacionPuerta" (ej: 0.500 segundos)
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
                // Inicia la animación de apertura parcial
                animPuerta.SetBool("AbrirParcial", true); 
                
                // Inicia el proceso de desactivación del Animator
                StartCoroutine(DesactivarAnimator());
            }

            if (bloqueoPasillo != null)
                bloqueoPasillo.enabled = false; 
        }
    }

    // Coroutine para esperar a que la animación termine y liberar el control.
    IEnumerator DesactivarAnimator()
    {
        // Espera la duración del clip de AnimacionPuerta
        yield return new WaitForSeconds(duracionAnimacion); 
        
        // 🚨 CRÍTICO: Desactivar el componente Animator.
        // Esto libera el control de la rotación y permite que Door.cs funcione.
        if (animPuerta != null)
        {
            animPuerta.enabled = false;
        }
    }
}
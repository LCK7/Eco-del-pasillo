using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    // Arrastra el componente Light de la linterna aquí desde el Inspector
    public Light flashlightLight; 

    // Tecla para encender/apagar
    public KeyCode toggleKey = KeyCode.F;

    void Update()
    {
        // Si el jugador presiona la tecla F
        if (Input.GetKeyDown(toggleKey))
        {
            // Invierte el estado de encendido/apagado de la luz
            flashlightLight.enabled = !flashlightLight.enabled;
            
            // Opcional: Sonido de interruptor
            // GetComponent<AudioSource>().Play(); 
        }
    }
}
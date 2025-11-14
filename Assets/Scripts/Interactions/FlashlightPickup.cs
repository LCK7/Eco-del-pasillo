using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public Light playerFlashlightComponent; 
    public GameObject playerFlashlightModel; 
    
    // La tecla 'E' se manejará en el script del Raycast, por lo que esta variable es opcional.
    // public KeyCode pickupKey = KeyCode.E; 

    // Ocultar las variables de UI si el Raycast las maneja
    // public GameObject pickupPromptUI; 
    
    // Hacemos la función pública para que pueda ser llamada por el Raycast.
    public void Pickup() 
    {
        // 1. Activar componentes de la linterna del jugador
        if (playerFlashlightComponent != null) playerFlashlightComponent.enabled = true;
        if (playerFlashlightModel != null) playerFlashlightModel.SetActive(true);
        
        // 2. Opcional: Ocultar el mensaje UI (Si lo haces desde aquí)
        // if (pickupPromptUI != null) pickupPromptUI.SetActive(false); 
        
        // 3. Eliminar el objeto de la linterna que estaba en el mundo
        Destroy(gameObject);
        
        Debug.Log("Linterna recogida y lista para usar.");
    }
    
    // **ELIMINAR:** Las funciones OnTriggerEnter, OnTriggerExit, y la variable playerIsNear.
}
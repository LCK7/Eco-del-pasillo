using UnityEngine;

public class FlashlightInteractor : MonoBehaviour
{
    // Asegúrate de que este valor es suficiente para alcanzar la linterna elevada
    public float DistanceOpen = 6f; // Lo subimos a 6f por seguridad
    
    // Conecta el objeto de texto UI específico para la linterna
    public GameObject textUIFlashlight; 

    void Update()
    {
        RaycastHit hit;
        
        // 1. Ocultar UI si no hay nada que ver (se apaga siempre al inicio)
        if (textUIFlashlight != null)
            textUIFlashlight.SetActive(false);

        // 2. Lanzamos el Raycast
        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            // Si el Raycast golpea ALGO, verificamos si es la Linterna
            if (hit.transform.CompareTag("Flashlight"))
            {
                // *************** DEBUGGING LINE 1 ***************
                // Este mensaje aparecerá constantemente si miras la linterna.
                Debug.Log("¡DETECTANDO LINTERNA!"); 
                // ************************************************

                // 3. Activamos el UI
                if (textUIFlashlight != null)
                    textUIFlashlight.SetActive(true);

                // 4. Si presionamos 'E', recogemos
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // *************** DEBUGGING LINE 2 ***************
                    Debug.Log("RECOGIENDO LINTERNA con la E!");
                    // ************************************************

                    FlashlightPickup flashlightPickup = hit.transform.GetComponent<FlashlightPickup>();
                    if (flashlightPickup != null)
                    {
                        flashlightPickup.Pickup();
                        // Ocultamos el mensaje permanentemente al recoger
                        if (textUIFlashlight != null)
                            textUIFlashlight.SetActive(false);
                    }
                }
            }
        }
    }
}
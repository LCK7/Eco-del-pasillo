using UnityEngine;
using DoorScript; // Necesario para acceder al script Door

namespace CameraDoorScript
{
    // Cambia el nombre de este script a PlayerInteractor.cs para mejor claridad
    public class PlayerInteractor : MonoBehaviour 
    {
        public float DistanceInteraction = 3f; // Distancia para interactuar
        public GameObject textUIMessage;      // El objeto UI que muestra "[E] Presionar"

        void Update () 
        {
            RaycastHit hit;
            bool hitObjectOfInterest = false;

            // Lanza el rayo hacia adelante desde la posición de la cámara
            if (Physics.Raycast (transform.position, transform.forward, out hit, DistanceInteraction)) 
            {
                // **1. Detección de Puerta**
                if (hit.transform.GetComponent<DoorScript.Door> ()) 
                {
                    hitObjectOfInterest = true;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                        hit.transform.GetComponent<DoorScript.Door> ().OpenDoor();
                }
                // **2. Detección de Linterna (Pickup)**
                else if (hit.transform.CompareTag("Flashlight")) 
                {
                    hitObjectOfInterest = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        var flashlightPickup = hit.transform.GetComponent<FlashlightPickup>();
                        if (flashlightPickup != null)
                        {
                            // Llamamos a la función pública Pickup() en el objeto linterna
                            flashlightPickup.Pickup(); 
                            
                            // Ocultamos el mensaje después de la recogida
                            textUIMessage.SetActive(false); 
                        }
                    }
                }

                // Mostrar UI si golpeamos un objeto de interés
                if (hitObjectOfInterest)
                {
                    textUIMessage.SetActive (true);
                }
                else
                {
                    textUIMessage.SetActive (false);
                }
            }
            else
            {
                // No golpeamos nada, ocultar UI
                textUIMessage.SetActive (false);
            }
        }
    }
}
using UnityEngine;
using DoorScript; // Necesario para acceder al script Door

namespace CameraDoorScript
{
    // PlayerInteractor: Interacción con puertas solamente
    public class PlayerInteractor : MonoBehaviour 
    {
        public float DistanceInteraction = 3f; // Distancia para interactuar
        public GameObject textUIMessage;      // El objeto UI que muestra "[E] Presionar"

        void Update () 
        {
            RaycastHit hit;
            bool hitObjectOfInterest = false;

            // Lanza el rayo hacia adelante desde la posición de la cámara
            if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceInteraction)) 
            {
                // **Detección de Puerta**
                Door door = hit.transform.GetComponent<Door>();
                if (door != null) 
                {
                    hitObjectOfInterest = true;

                    if (Input.GetKeyDown(KeyCode.E))
                        door.OpenDoor();
                }

                // Mostrar UI si golpeamos un objeto de interés
                textUIMessage.SetActive(hitObjectOfInterest);
            }
            else
            {
                // No golpeamos nada, ocultar UI
                textUIMessage.SetActive(false);
            }
        }
    }
}

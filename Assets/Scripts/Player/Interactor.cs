using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 2.5f;
    public LayerMask interactableLayer;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactDistance, interactableLayer.value))
        {
            var monos = hit.collider.GetComponents<MonoBehaviour>();
            foreach (var mb in monos)
            {
                if (mb is IInteractable interactable)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.Interact();
                    }
                    break;
                }
            }
        }
        else
        {
            // ocultar texto de interacción en UI si existe
        }
    }
}

using UnityEngine;

public class LoopTrigger : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            LoopManager.Instance.NextLoop();
            // opcional: reiniciar la escena o activar efectos; resetea triggered si quieres múltiples activaciones
        }
    }
}

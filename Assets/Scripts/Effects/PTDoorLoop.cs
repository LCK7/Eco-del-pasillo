using UnityEngine;
using System.Collections;

public class PTDoorLoopFixed : MonoBehaviour
{
    [Tooltip("Transform donde quieres que APAREZCA el punto que entra al trigger (la posición exacta)")]
    public Transform destino;

    [Tooltip("Pequeño ajuste manual si la aparición queda adelante/atrás (ej: destino.forward * -0.5f)")]
    public Vector3 extraOffset = Vector3.zero;

    [Tooltip("Tiempo de gracia para evitar retriggers/solapamientos")]
    public float cooldown = 0.6f;

    public bool debug = true;

    private bool puedeTeletransportar = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!puedeTeletransportar) return;
        if (!other.CompareTag("Player")) return;

        StartCoroutine(Teletransportar(other));
    }

    private IEnumerator Teletransportar(Collider otherCollider)
    {
        if (destino == null)
        {
            Debug.LogError($"[PTDoorLoopFixed] ERROR: 'destino' no asignado en {gameObject.name}");
            yield break;
        }

        puedeTeletransportar = false;

        // Root del jugador (por si el collider que entró es un child)
        Transform playerTriggerTransform = otherCollider.transform;
        Transform playerRoot = playerTriggerTransform.root;

        // Offset: queremos que el punto que activó el trigger quede exactamente en 'destino'
        Vector3 pointOffset = playerTriggerTransform.position - playerRoot.position;
        Vector3 targetRootPos = destino.position - pointOffset + extraOffset;

        // Detectar componentes físicos
        CharacterController cc = playerRoot.GetComponent<CharacterController>();
        Rigidbody rb = playerRoot.GetComponent<Rigidbody>();

        if (debug)
        {
            Debug.Log($"[PTDoorLoopFixed] Teleport {playerRoot.name} -> targetRootPos {targetRootPos} | destino {destino.position} | pointOffset {pointOffset}", this);
        }

        // Desactivar CharacterController para evitar que la colisión lo empuje
        if (cc != null) cc.enabled = false;

        // Si tiene Rigidbody, resetear velocidad y colocar con rb.position (teleport)
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Mover física directamente (teletransporte)
            rb.position = targetRootPos;
            rb.rotation = destino.rotation;
        }
        else
        {
            // Mover la raíz del jugador
            playerRoot.position = targetRootPos;
            playerRoot.rotation = destino.rotation;
        }

        // Esperar un frame para que física/cámaras se actualicen
        yield return null;

        if (cc != null) cc.enabled = true;

        // Pequeña espera para evitar retriggers inmediatos
        yield return new WaitForSeconds(cooldown);

        puedeTeletransportar = true;
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (destino == null)
            UnityEngine.Debug.LogWarning($"[PTDoorLoopFixed] 'destino' no asignado en {gameObject.name}", this);
#endif
    }
}

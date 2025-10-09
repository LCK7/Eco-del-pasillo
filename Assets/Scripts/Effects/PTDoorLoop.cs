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


        Transform playerTriggerTransform = otherCollider.transform;
        Transform playerRoot = playerTriggerTransform.root;

        Vector3 pointOffset = playerTriggerTransform.position - playerRoot.position;
        Vector3 targetRootPos = destino.position - pointOffset + extraOffset;

        CharacterController cc = playerRoot.GetComponent<CharacterController>();
        Rigidbody rb = playerRoot.GetComponent<Rigidbody>();

        if (debug)
        {
            Debug.Log($"[PTDoorLoopFixed] Teleport {playerRoot.name} -> targetRootPos {targetRootPos} | destino {destino.position} | pointOffset {pointOffset}", this);
        }

        if (cc != null) cc.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = targetRootPos;
            rb.rotation = destino.rotation;
        }
        else
        {
            playerRoot.position = targetRootPos;
            playerRoot.rotation = destino.rotation;
        }

        yield return null;

        if (cc != null) cc.enabled = true;

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

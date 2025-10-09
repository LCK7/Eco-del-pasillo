using UnityEngine;
using System.Collections;

public class SombraTrigger : MonoBehaviour
{
    public Transform player;             // Arrastra aquí el objeto del jugador
    public GameObject sombra;            // Arrastra aquí la sombra o figura
    public float rotacionRapida = 300f;  // Velocidad del giro
    public float duracionSombra = 0.5f;  // Tiempo visible de la sombra
    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;

        if (other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(EfectoSombra());
        }
    }

    IEnumerator EfectoSombra()
    {
        // 1️⃣ — Girar al jugador rápidamente hacia atrás (efecto sorpresa)
        Quaternion rotacionObjetivo = Quaternion.LookRotation(-player.forward);
        float tiempoGiro = 0f;

        while (tiempoGiro < 0.3f)
        {
            player.rotation = Quaternion.Slerp(player.rotation, rotacionObjetivo, Time.deltaTime * rotacionRapida);
            tiempoGiro += Time.deltaTime;
            yield return null;
        }

        // 2️⃣ — Mostrar sombra fugaz
        if (sombra != null)
        {
            sombra.SetActive(true);
            yield return new WaitForSeconds(duracionSombra);
            sombra.SetActive(false);
        }

        // 3️⃣ — Fin: ya no vuelve a pasar hasta que se reinicie la escena
    }
}

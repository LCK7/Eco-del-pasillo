using UnityEngine;
using System.Collections;

public class SombraTrigger : MonoBehaviour
{
    public Transform player;          
    public GameObject sombra;           
    public float rotacionRapida = 300f;  
    public float duracionSombra = 0.5f;  
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
        Quaternion rotacionObjetivo = Quaternion.LookRotation(-player.forward);
        float tiempoGiro = 0f;

        while (tiempoGiro < 0.3f)
        {
            player.rotation = Quaternion.Slerp(player.rotation, rotacionObjetivo, Time.deltaTime * rotacionRapida);
            tiempoGiro += Time.deltaTime;
            yield return null;
        }
        if (sombra != null)
        {
            sombra.SetActive(true);
            yield return new WaitForSeconds(duracionSombra);
            sombra.SetActive(false);
        }
    }
}

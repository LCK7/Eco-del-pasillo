using UnityEngine;

public class BathroomLightManager : MonoBehaviour
{
    // Arrastra el componente Light del techo del baño aquí
    public Light bathroomLight; 

    // Intensidad normal de la luz (ej: 1.0f)
    public float normalIntensity = 1.0f;

    // Intensidad atenuada/apagada (ej: 0.1f para tenue, 0.0f para apagada)
    public float dimmedIntensity = 0.1f;

    // Velocidad de transición
    public float transitionSpeed = 2.0f; 

    private float targetIntensity;

    void Start()
    {
        // Asegura que la luz está encendida al inicio
        if (bathroomLight != null)
        {
            bathroomLight.intensity = normalIntensity;
        }
        targetIntensity = normalIntensity;
    }

    void Update()
    {
        // Transición suave (Lerp) de la intensidad hacia el valor objetivo
        if (bathroomLight != null)
        {
            bathroomLight.intensity = Mathf.Lerp(
                bathroomLight.intensity, 
                targetIntensity, 
                Time.deltaTime * transitionSpeed
            );
        }
    }
    
    // ⬇️ FUNCIÓN CRÍTICA: Se dispara al ENTRAR al trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador entró, atenuar la luz
            targetIntensity = dimmedIntensity;
            
            // Opcional: Aquí podrías forzar a encender la linterna del jugador
            // Buscar el FlashlightController y llamar a una función para encenderla
        }
    }

    // ⬆️ FUNCIÓN CRÍTICA: Se dispara al SALIR del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador salió, restaurar la luz
            targetIntensity = normalIntensity;
            
            // Opcional: Aquí podrías forzar a apagar la linterna del jugador
        }
    }
}
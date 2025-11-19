using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float minIntensity = 0f;
    public float maxIntensity = 5f;

    public float flickerSpeed = 0.1f;      // Velocidad de cambios bruscos
    public float chaos = 2f;               // Cuánto caos agregar (mientras más, más brutal)
    public float blackoutChance = 0.1f;    // Probabilidad de apagones cortos (0.1 = 10%)

    private Light lt;
    private float timer;

    void Start()
    {
        lt = GetComponent<Light>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = flickerSpeed;

            // Apagón aleatorio breve
            if (Random.value < blackoutChance)
            {
                lt.intensity = 0f;
                return;
            }

            // Cambio de intensidad brusco
            float chaotic = Random.Range(minIntensity, maxIntensity)
                            + Random.Range(-chaos, chaos);

            lt.intensity = Mathf.Clamp(chaotic, 0f, maxIntensity);
        }
    }
}

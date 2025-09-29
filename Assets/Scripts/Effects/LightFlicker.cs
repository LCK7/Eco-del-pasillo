using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.2f;
    public float speed = 1f;
    private Light lt;

    void Start()
    {
        lt = GetComponent<Light>();
    }

    void Update()
    {
        lt.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * speed, 0f));
    }
}

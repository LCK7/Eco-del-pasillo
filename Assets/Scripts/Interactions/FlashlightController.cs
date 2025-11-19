using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;  
    public KeyCode toggleKey = KeyCode.F;

    void Start()
    {
        // Empieza apagada (puedes cambiarlo si quieres)
        if (flashlight != null)
            flashlight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}

using UnityEngine;
using System.Collections; // Necesario para usar Coroutines (para el fade out)

[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Transform cameraTransform;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    [Header("Footsteps (Loop)")]
    public AudioClip footstepsLoop;  // único archivo largo de pasos
    private AudioSource audioSource;
    
    // Control del Fade Out
    public float fadeOutTime = 0.2f; // Tiempo que tarda el sonido en desvanecerse (en segundos)
    private Coroutine fadeOutCoroutine; // Referencia a la corutina activa

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        // Configuración del AudioSource
        audioSource.clip = footstepsLoop;
        audioSource.loop = true;          // repetir en bucle
        audioSource.playOnAwake = false;  // no sonar al iniciar
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.7f;   
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
        HandleFootsteps();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (cameraTransform) cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 1. Calcular el movimiento horizontal
        Vector3 horizontalMove = transform.right * x + transform.forward * z;
        Vector3 finalHorizontalVelocity = horizontalMove * speed;
        
        // 2. Manejar la gravedad (movimiento vertical)
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f; 
        
        velocity.y += gravity * Time.deltaTime;

        // 3. COMBINAR movimiento horizontal y vertical en una sola llamada a controller.Move()
        // ¡Esta es la corrección crucial que soluciona el registro de velocidad!
        controller.Move((finalHorizontalVelocity + velocity) * Time.deltaTime); 
    }

    void HandleFootsteps()
    {
        // Usamos controller.velocity después de la corrección en HandleMovement()
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (horizontalVelocity.magnitude > 0.01f) // 0.01f es más seguro que 0f
        {
            if (!audioSource.isPlaying)
            {
                // Si estamos reproduciendo, cancelamos cualquier intento previo de FadeOut.
                if (fadeOutCoroutine != null)
                {
                    StopCoroutine(fadeOutCoroutine);
                    fadeOutCoroutine = null;
                }

                // Aseguramos que el volumen esté al máximo antes de reproducir.
                audioSource.volume = 0.7f; 
                Debug.Log("Reproduciendo pasos...");
                audioSource.Play();
            }
        }
        else
        {
            // Solo iniciamos el Fade Out si el sonido está sonando y si no hay un fade out ya activo
            if (audioSource.isPlaying && fadeOutCoroutine == null)
            {
                Debug.Log("Iniciando desvanecimiento (Fade Out)");
                fadeOutCoroutine = StartCoroutine(FadeOutStop(audioSource, fadeOutTime));
            }
        }
    }
    
    // Corutina para desvanecer el volumen y luego detener el sonido
    IEnumerator FadeOutStop(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        // Reduce el volumen gradualmente hasta 0
        while (audioSource.volume > 0)
        {
            float elapsed = Time.time - startTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            audioSource.volume = newVolume;
            yield return null; // Espera al siguiente frame
        }

        // Una vez que el volumen es 0, detenemos y restauramos el volumen
        audioSource.Stop();
        audioSource.volume = 0.7f; 
        
        fadeOutCoroutine = null; // Resetea la corutina para que se pueda iniciar de nuevo
        Debug.Log("Se detuvo el sonido después del Fade Out");
    }
}
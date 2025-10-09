using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform cameraTransform;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    [Header("Pisadas Naturales")]
    public AudioClip[] footstepClips; 
    private AudioSource audioSource;
    public float walkStepBase = 0.5f;    
    public float runStepBase = 0.35f;     
    private float nextStepTime;         
    private bool leftFoot = true;
    private int currentFootstepIndex = 0;    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f; 
        audioSource.volume = 0.8f;
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

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 horizontalMove = transform.right * x + transform.forward * z;
        Vector3 finalHorizontalVelocity = horizontalMove * speed;

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f; 

        velocity.y += gravity * Time.deltaTime;

        controller.Move((finalHorizontalVelocity + velocity) * Time.deltaTime);
    }

    void HandleFootsteps()
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (horizontalVelocity.magnitude > 0.2f && controller.isGrounded)
        {
            if (Time.time >= nextStepTime)
            {
                PlayNaturalFootstep();
                float baseInterval = (speed == runSpeed ? runStepBase : walkStepBase);
                nextStepTime = Time.time + baseInterval * Random.Range(0.9f, 1.1f); // ligera variación natural
            }
        }
    }
    void PlayNaturalFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0)
            return;

        AudioClip clip = footstepClips[currentFootstepIndex];
        currentFootstepIndex = (currentFootstepIndex + 1) % footstepClips.Length;

        audioSource.panStereo = leftFoot ? -0.15f : 0.15f;
        leftFoot = !leftFoot;

        audioSource.pitch = Random.Range(0.98f, 1.02f);
        audioSource.volume = Random.Range(0.5f, 0.6f);

        audioSource.PlayOneShot(clip);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollisionAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    
    //tarea 6
    public float volumeMultiplier = 0.1f;
    
    //tarea 8
    public AudioSource stepAudioSource;
    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource.Stop();
        stepAudioSource.Stop();
        
    }

    void Update()
    {
        // horizontal y vertical
        float h = 0f;
        float v = 0f;

        if (Keyboard.current.wKey.isPressed) v += 1f; 
        if (Keyboard.current.sKey.isPressed) v -= 1f;
        if (Keyboard.current.aKey.isPressed) h -= 1f;
        if (Keyboard.current.dKey.isPressed) h += 1f;

        // Calculate movement vector
        Vector3 move = new Vector3(h, 0f, v).normalized * moveSpeed;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        
        // Determine if player is walking
        isWalking = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude > 0.1f;

        // Step sound control
        if (stepAudioSource != null)
        {
            if (!stepAudioSource.isPlaying) stepAudioSource.Play(); // Play once
            stepAudioSource.volume = isWalking ? 1f : 0f;          // Mute if not walking
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        // para que cuando choca con el suelo no produce ningun sonido
        if (collision.gameObject.CompareTag("Ground")) return;
        // solo una vez
        if (audioSource != null)
        {
            // tarea 6 - calcular la velocidad y calcular el volumen según la velocidad
            float impactSpeed = collision.relativeVelocity.magnitude;
            float volume = Mathf.Clamp(impactSpeed * volumeMultiplier, 0.1f, 1f);
            Debug.Log("Speed: " + impactSpeed + ", Volume: " + volume);

            // producir sonido
            audioSource.PlayOneShot(audioSource.clip, volume);
        }
        
    }
}

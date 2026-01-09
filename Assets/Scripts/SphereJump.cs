using UnityEngine;
using UnityEngine.InputSystem;
public class SphereJump : MonoBehaviour
{
    public AudioSource _MyAudioSource;
    public float height = 2f;
    public float speed = 3f;

    private bool isJumping = false;
    private Vector3 startPos;

    void Start()
    {
        _MyAudioSource = GetComponent<AudioSource>();
        _MyAudioSource.loop = true;
        _MyAudioSource.Stop();
        startPos = transform.position;
    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            isJumping = true;

            if (!_MyAudioSource.isPlaying)
                _MyAudioSource.Play();
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            isJumping = false;
            _MyAudioSource.Stop();
            transform.position = startPos;
        }

        if (isJumping)
        {
            float yOffset = Mathf.Abs(Mathf.Sin(Time.time * speed)) * height;
            transform.position = new Vector3(
                startPos.x,
                startPos.y + yOffset,
                startPos.z
            );
        }
    }
}

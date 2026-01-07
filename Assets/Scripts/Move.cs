using UnityEngine;
using UnityEngine.InputSystem;

public class FastMover : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        if (Keyboard.current.mKey.isPressed)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}

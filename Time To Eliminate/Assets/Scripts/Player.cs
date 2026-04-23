using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1f;
    public float JumpForce = 1f;

    public GameObject head;

    private Rigidbody rb;
    private Vector2 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Look()
    {
        var mouse_x = Input.GetAxisRaw("Mouse X") * 50f * Time.fixedDeltaTime;
        var mouse_y = Input.GetAxisRaw("Mouse Y") * 50f * Time.fixedDeltaTime;

        Vector3 rot = head.transform.localRotation.eulerAngles;

        transform.rotation = transform.rotation * Quaternion.Euler(1, mouse_x, 1);
    }

    void GetInput()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
    }

    void Movement()
    {
        rb.AddForce(dir.normalized * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        Look();
    }
}

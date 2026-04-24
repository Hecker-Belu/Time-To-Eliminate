using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1f;
    public float JumpForce = 1f;

    public Transform head, camera, orientation;
    public float sensitivity = 1f;

    private Rigidbody rb;
    private Vector3 dir;

    private float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Look()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;


    }

    void GetInput()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
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

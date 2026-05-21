using UnityEngine;

public class Rocket : MonoBehaviour
{
    public string targetName;
    private GameObject target;
    public GameObject particles;
    public float speed = 15f;
    public float lifetime = 10f;

    // How fast the rocket turns (degrees per second)
    public float turnSpeed = 90f;

    void Start()
    {
        target = GameObject.Find(targetName);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;

        // Smooth rotation
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            turnSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

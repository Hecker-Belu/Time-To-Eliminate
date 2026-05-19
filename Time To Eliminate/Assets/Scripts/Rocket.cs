using UnityEngine;

public class Rocket : MonoBehaviour
{
    public string targetName;
    private GameObject target;
    public GameObject particles;
    public float speed = 15f;
    public float lifetime = 10f;

    void Start()
    {
        target = GameObject.Find(targetName);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.LookAt(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Spawn explosion at rocket position, NOT as a child
        Instantiate(particles, transform.position, Quaternion.identity);

        // Destroy rocket
        Destroy(gameObject);
    }
}

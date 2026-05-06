using UnityEngine;

public class EnemyGround : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Movement")]
    public float chaseForce = 1200f;
    public float turnSpeed = 8f;
    public float maxChaseDistance = 60f;

    [Header("Hover")]
    public float hoverHeight = 1.0f;
    public float hoverForce = 4000f;
    public float hoverDamp = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (!player) return;

        // DISTANCE-BASED DETECTION
        float dist = Vector3.Distance(transform.position, player.position);
        bool seen = dist <= maxChaseDistance;

        // ALWAYS flatten direction (fixes your Y problem)
        Vector3 toPlayer = player.position - transform.position;
        Vector3 flatDir = new Vector3(toPlayer.x, 0, toPlayer.z);

        Hover();

        if (seen)
        {
            RotateToward(flatDir);
            MoveToward(flatDir);
        }
    }

    void Hover()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, hoverHeight * 2f))
        {
            float heightError = hoverHeight - hit.distance;
            float upwardSpeed = rb.linearVelocity.y;
            float lift = (heightError * hoverForce) - (upwardSpeed * hoverDamp);

            rb.AddForce(Vector3.up * lift);
        }
    }

    void RotateToward(Vector3 flatDir)
    {
        if (flatDir.sqrMagnitude < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(flatDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * turnSpeed
        );
    }

    void MoveToward(Vector3 flatDir)
    {
        rb.AddForce(flatDir.normalized * chaseForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
}

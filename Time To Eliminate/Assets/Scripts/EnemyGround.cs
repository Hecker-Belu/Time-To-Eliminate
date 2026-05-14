using UnityEngine;
using UnityEngine.AI;

public class EnemyGround : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float turnSpeed = 8f;
    public float maxChaseDistance = 60f;

    NavMeshAgent agent;
    float verticalVelocity = 0f;
    float gravity = -20f; // stronger than Unity default

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!player) return;

        // --- GRAVITY ---
        verticalVelocity += gravity * Time.deltaTime;

        // Apply gravity to agent
        agent.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);

        // If grounded, reset vertical velocity
        if (agent.isOnNavMesh && agent.isOnOffMeshLink == false)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.2f))
            {
                if (hit.distance < 1.05f)
                    verticalVelocity = 0f;
            }
        }

        // --- CHASE LOGIC ---
        Vector3 toPlayer = player.position - transform.position;
        Vector3 flatDir = new Vector3(toPlayer.x, 0, toPlayer.z);

        bool seen = flatDir.magnitude <= maxChaseDistance;

        if (seen)
        {
            agent.SetDestination(player.position);
            RotateToward(flatDir);
        }
        else
        {
            agent.ResetPath();
        }

        if (player.GetComponent<Player>().isSetting)
        {
            agent.ResetPath();
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
}

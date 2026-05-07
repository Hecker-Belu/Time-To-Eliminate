using UnityEngine;
using UnityEngine.AI;

public class EnemyGround : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float turnSpeed = 8f;
    public float maxChaseDistance = 60f;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // We handle rotation manually
        agent.updateRotation = false;
        agent.updateUpAxis = false; // optional if using flat ground
    }

    void Update()
    {
        if (!player) return;

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

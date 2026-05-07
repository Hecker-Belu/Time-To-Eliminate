using UnityEngine;
using UnityEngine.AI;

public class RobotWalker : MonoBehaviour
{
    NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(0,0,0));
    }
}

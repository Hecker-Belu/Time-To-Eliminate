using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform follow;

    void Update()
    {
        transform.position = follow.position;
    }
}

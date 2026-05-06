using UnityEngine;

public class LegStepper : MonoBehaviour
{
    public Transform body;
    public Transform legTarget;
    public float stepDistance = 0.7f;
    public float stepHeight = 0.3f;
    public float stepSpeed = 6f;

    Vector3 defaultLocalPos;
    Vector3 currentTargetPos;
    bool isStepping = false;

    void Start()
    {
        defaultLocalPos = body.InverseTransformPoint(legTarget.position);
        currentTargetPos = legTarget.position;
    }

    void Update()
    {
        Vector3 idealWorldPos = body.TransformPoint(defaultLocalPos);
        float dist = Vector3.Distance(idealWorldPos, currentTargetPos);

        if (!isStepping && dist > stepDistance)
            StartCoroutine(Step(idealWorldPos));

        legTarget.position = currentTargetPos;
    }

    System.Collections.IEnumerator Step(Vector3 newPos)
    {
        isStepping = true;

        Vector3 start = currentTargetPos;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * stepSpeed;

            Vector3 mid = (start + newPos) * 0.5f + Vector3.up * stepHeight;
            currentTargetPos = Vector3.Lerp(
                Vector3.Lerp(start, mid, t),
                Vector3.Lerp(mid, newPos, t),
                t
            );

            yield return null;
        }

        currentTargetPos = newPos;
        isStepping = false;
    }
}

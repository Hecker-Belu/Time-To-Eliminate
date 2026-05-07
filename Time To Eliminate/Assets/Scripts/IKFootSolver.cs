using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IKFootSolver : MonoBehaviour
{
    public LayerMask GroundLayer;
    public Transform Body;
    public IKFootSolver OtherFoot;
    public float Speed;
    public float StepDistance;
    public float StepLength;
    public float StepHeight;
    public Vector3 FootOffset;

    private float footSpacing;
    private Vector3 oldPosition, currentPosition, newPosition;
    private Vector3 oldNormal, currentNormal, newNormal;

    private float lerp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPosition;
        transform.up = currentNormal;

        Ray ray = new Ray(Body.position + (Body.right * footSpacing), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 10, GroundLayer.value))
        {
            if (Vector3.Distance(newPosition, info.point) > StepDistance && !OtherFoot.IsMoving() && lerp >= 1)
            {
                lerp = 0;
                int direction = Body.InverseTransformPoint(info.point).z > Body.InverseTransformPoint(newPosition).z ? 1 : -1;
                newPosition = info.point + (Body.forward * StepLength * direction) + FootOffset;
                newNormal = info.normal;
            }
        }

        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * StepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * Speed;
        } else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }

    public bool IsMoving() {
        return lerp < 1;
    }
}

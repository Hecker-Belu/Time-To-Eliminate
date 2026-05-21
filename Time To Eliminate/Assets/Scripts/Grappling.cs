using UnityEngine;

public class Grappling : MonoBehaviour
{
    public AudioSource grappleSfx;

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    private bool pullingToEnemy = false;
    private Vector3 currentGrapplePosition;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }

    void FixedUpdate()
    {
        if (pullingToEnemy)
        {
            Vector3 dir = (grapplePoint - player.position).normalized;
            float pullSpeed = 40f; // adjust to taste

            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.linearVelocity = dir * pullSpeed;
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grappleSfx.Play();
            grapplePoint = hit.point;

            // ENEMY GRAPPLE � NO SPRINGJOINT
            if (hit.collider.CompareTag("Enemy"))
            {
                pullingToEnemy = true;

                lr.positionCount = 2;
                currentGrapplePosition = gunTip.position;
                return; // skip SpringJoint creation
            }

            // NORMAL GRAPPLE � USE SPRINGJOINT
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.25f;
            joint.minDistance = distanceFromPoint * 0.10f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    void StopGrapple()
    {
        grappleSfx.Stop();
        lr.positionCount = 0;

        pullingToEnemy = false;

        if (joint)
            Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint && !pullingToEnemy) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null || pullingToEnemy;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}

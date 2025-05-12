using UnityEngine;

public class SpringArmCamera : MonoBehaviour
{
    public Transform followTarget;
    public Transform focusTarget;
    public float distance = 5f;
    public float sensitivity = 3f;
    public float AimPointSensitivity = 5f;
    public LayerMask collisionMask;


    private Vector3 currentVelocity;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = followTarget.position - followTarget.forward * distance;
        if (Physics.Linecast(followTarget.position, desiredPosition, out RaycastHit hit, collisionMask)){
            desiredPosition = hit.point + hit.normal * 0.1f;
        }

        transform.position = desiredPosition;//Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1f/ sensitivity);

        Quaternion targetRotation = Quaternion.LookRotation(focusTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * AimPointSensitivity);

        //transform.LookAt(focusTarget);
    }
}

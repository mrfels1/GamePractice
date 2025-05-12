using UnityEngine;

public class AimRayCast : MonoBehaviour
{
    public Transform muzzle;
    public float maxDistance = 100f;
    public LayerMask collisionMask;

    void Update()
    {
        Vector3 origin = muzzle.position;
        Vector3 direction = muzzle.forward;

        if(Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, collisionMask)){
            transform.position = hit.point;
        }
        else{
            transform.position = origin + direction * maxDistance;
        }
    }
}

using UnityEngine;

public class ProjectileTracerRaycast : MonoBehaviour
{
    public float speed = 800f;
    public float damage = 10f;
    public float gravity = 9.81f;
    public float lifetime = 5f;
    public GameObject impactEffect;
    public LineRenderer tracer;

    private Vector3 velocity;
    private Vector3 lastPosition;

    public void Initialize(Vector3 direction, float speed, float gravity, float damage, GameObject impactFx)
    {
        this.velocity = direction.normalized * speed;
        this.gravity = gravity;
        this.damage = damage;
        this.impactEffect = impactFx;
        lastPosition = transform.position;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        Vector3 currentPosition = transform.position;

        // Баллистика
        velocity += Vector3.down * gravity * dt;
        Vector3 newPosition = currentPosition + velocity * dt;

        // Коллизия
        if (Physics.Raycast(currentPosition, newPosition - currentPosition, out RaycastHit hit, Vector3.Distance(currentPosition, newPosition)))
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            if (impactEffect)
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(gameObject);
            return;
        }

        transform.position = newPosition;
        transform.forward = velocity.normalized;

        // Обновление трассера
        if (tracer)
        {
            tracer.SetPosition(0, lastPosition);
            tracer.SetPosition(1, newPosition);
        }

        lastPosition = newPosition;
    }
}
